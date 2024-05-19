using GameData;
using GameData.Stage;
using Game.Fortress;
using Game.Tower;
using Game.Enemy;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// ゲーム画面の処理
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region PublicField
        /// <summary>ゲームクリア時の処理</summary>
        public Subject<Unit> GameClearSubject = new Subject<Unit>();
        /// <summary>ゲームオーバー時の処理</summary>
        public Subject<Unit> GameOverSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>ウェーブ数の初期化</summary>
        private const int waveInitNum = 0;
        /// <summary>スコアの初期化</summary>
        private const int scoreInitNum = 0;
        /// <summary>ゲーム開始ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickGameStartButtonObserver => gameStartBtn.OnClickAsObservable();
        #endregion

        #region SerializeField 
        /// <summary>ゲーム開始ボタン</summary>
        [SerializeField] private Button gameStartBtn;
        /// <summary砦の処理</summary>
        [SerializeField] private FortressController fortressController;
        /// <summary>タワーの処理</summary>
        [SerializeField] private TowerController towerController;
        /// <summary>敵の処理</summary>
        [SerializeField] private EnemyController enemyController;
        /// <summary>ゲームで表示するUI情報</summary>
        [SerializeField] private GameViewUI gameViewUI;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            if (gameDataInfo.isEXStage)
            {
                // EXステージ情報を取得
                var stageDataInfo = GameDataManager.instance.GetEXStageDataInfo();

                Instantiate(stageDataInfo.stageObj, Vector3.zero, Quaternion.identity);
                fortressController = GameObject.FindWithTag("Fortress").GetComponent<FortressController>();
            }
            else
            {
                // 通常ステージ情報を取得
                var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

                Instantiate(stageDataInfo.stageObj, Vector3.zero, Quaternion.identity);
                fortressController = GameObject.FindWithTag("Fortress").GetComponent<FortressController>();
            }

            // 初期化
            fortressController.FortressDamageSubject.Subscribe(_ =>
            {
                FortressTakeDamage();
            }).AddTo(this);

            towerController.Init();

            towerController.TowerBuildSubject.Subscribe(towerCost =>
            {
                PossessionMoneyUpdate(-towerCost);
            }).AddTo(this);

            towerController.TowerSaleSubject.Subscribe(towerCost =>
            {
                PossessionMoneyUpdate(towerCost);
            }).AddTo(this);

            gameViewUI.Init();

            OnClickGameStartButtonObserver.Subscribe(_ =>
            {
                GameStart();
            }).AddTo(this);
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// ゲームを開始する処理
        /// </summary>
        private void GameStart()
        {
            // 開始と同時にボタンを非表示にする
            gameStartBtn.gameObject.SetActive(false);

            enemyController.Init();

            enemyController.NextWaveSubject.Subscribe(waveNum =>
            {
                WaveUpdate(waveNum);
            }).AddTo(this);

            enemyController.GetDropMoneySubject.Subscribe(dropMoney =>
            {
                PossessionMoneyUpdate(dropMoney);
            }).AddTo(this);

            enemyController.AddScoreSubject.Subscribe(score =>
            {
                ScoreUpdate(score);
            }).AddTo(this);

            enemyController.IsFinishSubject.Subscribe(isFinish =>
            {
                if (isFinish)
                {
                    IsGameClear();
                }
            }).AddTo(this);
        }

        /// <summary>
        /// 砦にダメージを与える処理
        /// </summary>
        private void FortressTakeDamage()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            var fortressLife = gameDataInfo.fortressLife;
            fortressLife--;

            // ゲームの情報を更新する
            var setGameDataInfo = new GameDataInfo(
                    fortressLife,
                    gameDataInfo.possessionMoney,
                    gameDataInfo.waveNum,
                    gameDataInfo.score,
                    gameDataInfo.isEXStage,
                    gameDataInfo.isGameClear,
                    gameDataInfo.isGameOver);
            GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

            gameViewUI.UpdateViewUI();

            IsGameOver();
        }

        /// <summary>
        /// 所持金を更新する処理
        /// </summary>
        /// <param name="towerCost">タワーの価値</param>
        private void PossessionMoneyUpdate(int variableValue)
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            var possessionMoney = gameDataInfo.possessionMoney;
            possessionMoney += variableValue;

            // ゲームの情報を更新する
            var setGameDataInfo = new GameDataInfo(
                    gameDataInfo.fortressLife,
                    possessionMoney,
                    gameDataInfo.waveNum,
                    gameDataInfo.score,
                    gameDataInfo.isEXStage,
                    gameDataInfo.isGameClear,
                    gameDataInfo.isGameOver);
            
            GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

            gameViewUI.UpdateViewUI();

        }

        /// <summary>
        /// ゲーム情報のウェーブを更新する処理
        /// </summary>
        /// <param name="waveNum">ウェーブ数</param>
        private void WaveUpdate(int waveNum)
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            // ウェーブ数を加算する
            waveNum++;

            // ゲームの情報を更新する
            var setGameDataInfo = new GameDataInfo(
                    gameDataInfo.fortressLife,
                    gameDataInfo.possessionMoney,
                    waveNum,
                    gameDataInfo.score,
                    gameDataInfo.isEXStage,
                    gameDataInfo.isGameClear,
                    gameDataInfo.isGameOver);
            GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

            gameViewUI.UpdateViewUI();
        }

        /// <summary>
        /// ゲーム情報のスコアを更新する処理
        /// </summary>
        /// <param name="score">スコア</param>
        private void ScoreUpdate(int getScore)
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            // スコアを加算する
            var score = gameDataInfo.score;
            score += getScore;

            // ゲームの情報を更新する
            var setGameDataInfo = new GameDataInfo(
                    gameDataInfo.fortressLife,
                    gameDataInfo.possessionMoney,
                    gameDataInfo.waveNum,
                    score,
                    gameDataInfo.isEXStage,
                    gameDataInfo.isGameClear,
                    gameDataInfo.isGameOver);
            GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

            gameViewUI.UpdateViewUI();
        }

        /// <summary>
        /// ゲームが終了したかどうかを確認する処理
        /// </summary>
        private void IsGameClear()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            if (IsEnemyZero() && IsWaveFinish() && !gameDataInfo.isGameClear)
            {
                // ゲームの情報を更新する
                var setGameDataInfo = new GameDataInfo(
                        gameDataInfo.fortressLife,
                        gameDataInfo.possessionMoney,
                        gameDataInfo.waveNum,
                        gameDataInfo.score,
                        gameDataInfo.isEXStage,
                        true,
                        gameDataInfo.isGameOver);
                GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

                GameClearSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// ゲームオーバーかどうかを確認する処理
        /// </summary>
        private void IsGameOver()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            var fortressLife = gameDataInfo.fortressLife;

            // 砦の耐久値が0以下の場合
            if (fortressLife <= 0 && !gameDataInfo.isGameOver)
            {
                // ゲームの情報を更新する
                var setGameDataInfo = new GameDataInfo(
                        gameDataInfo.fortressLife,
                        gameDataInfo.possessionMoney,
                        gameDataInfo.waveNum,
                        gameDataInfo.score,
                        gameDataInfo.isEXStage,
                        gameDataInfo.isGameClear,
                        true);
                GameDataManager.instance.SetGameDataInfo(setGameDataInfo);

                GameOverSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 敵の数が0かどうかを判定する処理
        /// </summary>
        private bool IsEnemyZero()
        {
            return enemyController.GetEnemyCount() == 0;
        }

        /// <summary>
        /// 全ウェーブが終了したかどうかを判定する処理
        /// </summary>
        private bool IsWaveFinish()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

            Debug.Log($"waveNum{gameDataInfo.waveNum}");
            Debug.Log($"waveInfo.Count{stageDataInfo.waveInfo.Count}");

            return gameDataInfo.waveNum == stageDataInfo.waveInfo.Count;
        }
        
        #endregion
    }
}