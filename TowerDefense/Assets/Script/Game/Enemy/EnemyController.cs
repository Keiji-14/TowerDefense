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
        /// <summary>スコアを加算する処理</summary>
        public Subject<int> AddScoreSubject = new Subject<int>();
        /// <summary>終了したかどうかの処理</summary>
        public Subject<bool> IsFinishSubject = new Subject<bool>();
        #endregion

        #region PrivateField
        /// <summary>ウェーブを開始するかどうかの判定</summary>
        private bool isWaveStart = false;
        /// <summary>出現した敵の保持</summary>
        private List<Enemy> enemyList = new List<Enemy>();
        #endregion

        #region SerializeField
        /// <summary>管理する親オブジェクト</summary>
        [SerializeField] private Transform createParent;
        #endregion

        void Update()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            if (gameDataInfo.isGameOver || gameDataInfo.isGameClear)
                return;

            if (isWaveStart)
            {
                isWaveStart = false;
                if (gameDataInfo.stageType == StageType.EX)
                {
                    StartCoroutine(EXCreateEnemy());
                }
                else
                {
                    StartCoroutine(DefaultCreateEnemy());
                }
            }
        }

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            isWaveStart = true;
        }

        public int GetEnemyCount()
        {
            return enemyList.Count;
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 通常ステージの敵を出現させる処理
        /// </summary>
        private IEnumerator DefaultCreateEnemy()
        {
            // ウェーブ数を取得
            var waveNum = GameDataManager.instance.GetGameDataInfo().waveNum;

            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

            // ウェーブが全て終了していた場合ば処理を止める
            if (waveNum >= stageDataInfo.waveInfo.Count)
            {
                IsFinishSubject.OnNext(true);
                yield break; // ウェーブ数が無効な場合は処理を中断
            }

            // ウェーブの情報を取得
            var waveInfo = stageDataInfo.waveInfo[waveNum];
            // 敵の出現場所を取得
            var spawnPoint = GameDataManager.instance.GetStageDataInfo().waveInfo[waveNum].spawnPoint.position;
            // ウェーブのインターバル時間を取得 
            var waveInterval = GameDataManager.instance.GetStageDataInfo().waveInterval;
            // 敵の情報を取得
            foreach (var enemySpawnInfo in waveInfo.enemySpawnInfoList)
            {
                // 敵の情報を取得
                var enemyDataInfo = GameDataManager.instance.GetEnemyDataInfo(enemySpawnInfo.enemyID);
                var enemyNum = enemySpawnInfo.enemyNum;

                for (int i = 0; i < enemyNum; i++)
                {
                    Vector3 center = spawnPoint;
                    // ランダムな方向を選択
                    Vector3 randomDirection = Random.insideUnitSphere * 0.5f;
                    // 中心からランダムな方向へ0.5からランダムな距離の位置を計算
                    Vector3 randomPos = center + randomDirection;
                    // Y軸の位置を中心と同じにする（必要に応じて修正）
                    randomPos.y = center.y;

                    var enemy = Instantiate(enemyDataInfo.enemyObj, randomPos, Quaternion.identity).GetComponent<Enemy>();

                    enemy.transform.SetParent(createParent);

                    enemy.Init(enemyDataInfo);

                    enemy.EnemyDestroySubject.Subscribe(_ =>
                    {
                        DestroyEnemy(enemy);
                    }).AddTo(this);

                    enemy.EnemyDefeatSubject.Subscribe(_ =>
                    {
                        GetDropMoneySubject.OnNext(enemy.enemyDataInfo.dropMoney);
                        DestroyEnemy(enemy);
                    }).AddTo(this);

                    // 生成した敵を追加
                    enemyList.Add(enemy);

                    yield return new WaitForSeconds(1f);
                }
            }

            yield return new WaitForSeconds(waveInterval);

            NextWaveSubject.OnNext(waveNum);

            isWaveStart = true;
        }

        /// <summary>
        /// EXステージの敵を出現させる処理
        /// </summary>
        private IEnumerator EXCreateEnemy()
        {
            // ウェーブ数を取得
            var waveNum = GameDataManager.instance.GetGameDataInfo().waveNum;
            // EXステージのデータを取得
            var stageDataInfo = GameDataManager.instance.GetEXStageDataInfo();
            // 敵の移動ルートを取得
            var randomRouteInfo = GetRandomRouteInfo(stageDataInfo.routeInfoList);
            // 敵の出現場所を取得
            var spawnPoint = randomRouteInfo.spawnPoint;
            // 敵の生成するンターバル時間を設定
            var createInterval = Mathf.Max(0.5f, 1f - 0.02f * waveNum);
            // ウェーブのインターバル時間を取得 
            var waveInterval = GameDataManager.instance.GetEXStageDataInfo().waveInterval;
            // 敵の数を取得
            var enemyNum = stageDataInfo.enemyNum + (waveNum - 1) * 2;
            // 敵の情報を取得
            var enemyDataInfoList = GameDataManager.instance.GetEnemyDataInfoList();
            // ボスの情報を取得
            var bossEnemyDataInfoList = GameDataManager.instance.GetBossEnemyDataInfoList();

            for (int i = 0; i < enemyNum; i++)
            {
                // ランダムに敵情報を選択
                var randomIndex = Random.Range(0, enemyDataInfoList.Count);
                var enemyDataInfo = enemyDataInfoList[randomIndex];

                Vector3 center = spawnPoint.position;
                // ランダムな方向を選択
                Vector3 randomDirection = Random.insideUnitSphere * 0.5f;
                // 中心からランダムな方向へ0.5からランダムな距離の位置を計算
                Vector3 randomPos = center + randomDirection;
                // Y軸の位置を中心と同じにする（必要に応じて修正）
                randomPos.y = center.y;

                var enemy = Instantiate(enemyDataInfo.enemyObj, randomPos, Quaternion.identity).GetComponent<Enemy>();

                enemy.transform.SetParent(createParent);

                enemy.Init(enemyDataInfo, randomRouteInfo);

                enemy.EnemyDestroySubject.Subscribe(_ =>
                {
                    DestroyEnemy(enemy);
                }).AddTo(this);

                enemy.EnemyDefeatSubject.Subscribe(_ =>
                {
                    GetDropMoneySubject.OnNext(enemy.enemyDataInfo.dropMoney);
                    AddScoreSubject.OnNext(enemy.enemyDataInfo.scorePoint);
                    DestroyEnemy(enemy);
                }).AddTo(this);

                // 生成した敵を追加
                enemyList.Add(enemy);

                yield return new WaitForSeconds(createInterval);
            }

            if (waveNum > 0 && waveNum % 3 == 0)
            {
                // ボス級の敵情報をランダムに選択
                var randomBossIndex = Random.Range(0, bossEnemyDataInfoList.Count);
                var bossEnemyDataInfo = bossEnemyDataInfoList[randomBossIndex];

                Vector3 center = spawnPoint.position;
                // ランダムな方向を選択
                Vector3 randomDirection = Random.insideUnitSphere * 0.5f;
                // 中心からランダムな方向へ0.5からランダムな距離の位置を計算
                Vector3 randomPos = center + randomDirection;
                // Y軸の位置を中心と同じにする（必要に応じて修正）
                randomPos.y = center.y;

                var bossEnemy = Instantiate(bossEnemyDataInfo.enemyObj, randomPos, Quaternion.identity).GetComponent<Enemy>();

                bossEnemy.transform.SetParent(createParent);

                bossEnemy.Init(bossEnemyDataInfo, randomRouteInfo);

                bossEnemy.EnemyDestroySubject.Subscribe(_ =>
                {
                    DestroyEnemy(bossEnemy);
                }).AddTo(this);

                bossEnemy.EnemyDefeatSubject.Subscribe(_ =>
                {
                    GetDropMoneySubject.OnNext(bossEnemy.enemyDataInfo.dropMoney);
                    AddScoreSubject.OnNext(bossEnemy.enemyDataInfo.scorePoint);
                    DestroyEnemy(bossEnemy);
                }).AddTo(this);

                // 生成したボス級の敵を追加
                enemyList.Add(bossEnemy);
            }

            yield return new WaitForSeconds(waveInterval);

            NextWaveSubject.OnNext(waveNum);

            isWaveStart = true;
        }

        /// <summary>
        /// ルート情報を返す処理
        /// </summary>
        private RouteInfo GetRandomRouteInfo(List<RouteInfo> routeInfoList)
        {
            if (routeInfoList == null || routeInfoList.Count == 0)
            {
                return null; // リストが空の場合はnullを返す
            }

            int randomIndex = Random.Range(0, routeInfoList.Count);
            return routeInfoList[randomIndex];
        }

        /// <summary>
        /// 敵が消滅する時の処理
        /// </summary>
        private void DestroyEnemy(Enemy enemy)
        {
            enemyList.Remove(enemy);

            enemy.Destroy();

            // すべての敵が倒された後にクリア判定を行う
            if (enemyList.Count == 0)
            {
                IsFinishSubject.OnNext(true);
            }
            else
            {
                IsFinishSubject.OnNext(false);
            }
        }
        #endregion
    }
}