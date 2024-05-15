using GameData;
using GameData.Stage;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        #region PublicField
        /// <summary>出現した敵の保持</summary>
        public Subject<int> NextWaveSubject = new Subject<int>();
        /// <summary>敵を倒したお金を入手する処理</summary>
        public Subject<int> GetDropMoneySubject = new Subject<int>();
        /// <summary>終了したかどうかの処理</summary>
        public Subject<int> IsFinishSubject = new Subject<int>();
        #endregion

        #region PrivateField
        /// <summary>ウェーブを開始するかどうかの判定</summary>
        private bool isWaveStart = false;
        /// <summary>出現した敵の保持</summary>
        private List<Enemy> enemyList = new List<Enemy>();
        #endregion

        void Update()
        {
            if (isWaveStart)
            {
                isWaveStart = false;
                StartCoroutine(CreateEnemy());
            }
        }

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(StageDataInfo stageDataInfo)
        {
            isWaveStart = true;
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 敵を出現させる処理
        /// </summary>
        /// <param name="">攻撃対象</param>
        private IEnumerator CreateEnemy()
        {
            // ウェーブ数を取得
            var waveNum = GameDataManager.instance.GetGameDataInfo().waveNum;

            // 最終ウェーブではない場合は続行
            if (waveNum <= GameDataManager.instance.GetStageDataInfo().waveInfo.Count)
            {
                // 敵の出現数を取得
                var enemyNum = GameDataManager.instance.GetStageDataInfo().waveInfo[waveNum].enemyNum;
                // 敵の出現場所を取得
                var spawnPoint = GameDataManager.instance.GetStageDataInfo().waveInfo[waveNum].spawnPoint.position;
                // ウェーブのインターバル時間を取得
                var waveInterval = GameDataManager.instance.GetStageDataInfo().waveInterval;
                // 敵の情報を取得
                var enemyDataInfo = GameDataManager.instance.GetEnemyDataInfo(0);

                for (int i = 0; i < enemyNum; i++)
                {
                    Vector3 center = spawnPoint;
                    // ランダムな方向を選択
                    Vector3 randomDirection = Random.insideUnitSphere * 1f;
                    // 中心からランダムな方向へ1からランダムな距離の位置を計算
                    Vector3 randomPos = center + randomDirection;
                    // Y軸の位置を中心と同じにする（必要に応じて修正）
                    randomPos.y = center.y;

                    var enemy = Instantiate(enemyDataInfo.enemyObj, randomPos, Quaternion.identity).GetComponent<Enemy>();
                    
                    enemy.Init(enemyDataInfo);

                    enemy.EnemyDestroySubject.Subscribe(_ =>
                    {
                        GetDropMoneySubject.OnNext(enemy.enemyDataInfo.dropMoney);
                        DesteryEnemy(enemy);
                    }).AddTo(this);

                    // 生成した敵を追加
                    enemyList.Add(enemy);

                    yield return new WaitForSeconds(1f);
                }

                yield return new WaitForSeconds(waveInterval);

                NextWaveSubject.OnNext(waveNum);

                isWaveStart = true;
            }
        }

        /// <summary>
        /// 敵が消滅する時の処理
        /// </summary>
        private void DesteryEnemy(Enemy enemy)
        {
            enemyList.Remove(enemy);
            Instantiate(enemy.enemyDataInfo.destroyParticle, enemy.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);

            IsFinishSubject.OnNext(enemyList.Count);
        }
        #endregion
    }
}