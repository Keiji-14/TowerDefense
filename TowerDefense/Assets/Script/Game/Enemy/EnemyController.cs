using GameData;
using GameData.Stage;
using GameData.Enemy;
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
        #endregion

        #region PrivateField
        /// <summary>ウェーブを開始するかどうかの判定</summary>
        private bool isWaveStart = false;
        /// <summary>出現した敵の保持</summary>
        private List<Enemy> enemyList = new List<Enemy>(); 
        #endregion

        #region SerializeField
        [SerializeField] private EnemyDataInfo enemyDataInfo;
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

                for (int i = 0; i < enemyNum; i++)
                {
                    var enemy = Instantiate(enemyDataInfo.enemyObj, spawnPoint, Quaternion.identity).GetComponent<Enemy>();

                    enemy.Init(enemyDataInfo);

                    enemyList.Add(enemy);

                    yield return new WaitForSeconds(1f);
                }

                yield return new WaitForSeconds(waveInterval);

                NextWaveSubject.OnNext(waveNum);

                isWaveStart = true;
            }
        }
        #endregion
    }
}