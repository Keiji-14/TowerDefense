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
        #region PrivateField
        /// <summary>ウェーブ数の初期化</summary>
        private const int waveInitNum = 1;
        /// <summary>ゲーム開始ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickGameStartButtonObserver => gameStartBtn.OnClickAsObservable();
        #endregion

        #region SerializeField 
        /// <summary>ゲーム開始ボタン</summary>
        [SerializeField] private Button gameStartBtn;
        /// <summary>ステージの情報</summary>
        [SerializeField] private StageDataInfo stageDataInfo;
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
            // ゲーム情報をを初期化
            var gameDataInfo = new GameDataInfo(stageDataInfo.startFortressLife, stageDataInfo.startMoney, waveInitNum);
            GameDataManager.instance.SetGameDataInfo(gameDataInfo);

            // 初期化
            fortressController.FortressDamageSubject.Subscribe(_ =>
            {
                FortressTakeDamage();
            }).AddTo(this);

            towerController.Init();

            towerController.TowerBuildSubject.Subscribe(towerCost =>
            {
                PossessionMoneyUpdate(towerCost);
                gameViewUI.UpdateViewUI();
            }).AddTo(this);

            gameViewUI.UpdateViewUI();

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

            enemyController.Init(stageDataInfo);

            enemyController.NextWaveSubject.Subscribe(waveNum =>
            {
                WaveUpdate(waveNum);
            }).AddTo(this);
        }

        /// <summary>
        /// 砦にダメージを与える処理
        /// </summary>
        private void FortressTakeDamage()
        {
            var fortressLife = GameDataManager.instance.GetGameDataInfo().fortressLife;
            fortressLife--;

            // ゲームの情報を更新する
            var gameDataInfo = new GameDataInfo(
                    fortressLife,
                    GameDataManager.instance.GetGameDataInfo().possessionMoney,
                    GameDataManager.instance.GetGameDataInfo().waveNum);
            GameDataManager.instance.SetGameDataInfo(gameDataInfo);

            gameViewUI.UpdateViewUI();
        }

        /// <summary>
        /// 所持金を更新する処理
        /// </summary>
        /// <param name="towerCost">タワーの価値</param>
        private void PossessionMoneyUpdate(int towerCost)
        {
            var possessionMoney = GameDataManager.instance.GetGameDataInfo().possessionMoney;
            possessionMoney -= towerCost;

            // ゲームの情報を更新する
            var gameDataInfo = new GameDataInfo(
                    GameDataManager.instance.GetGameDataInfo().fortressLife,
                    possessionMoney,
                    GameDataManager.instance.GetGameDataInfo().waveNum);
            GameDataManager.instance.SetGameDataInfo(gameDataInfo);
        }

        /// <summary>
        /// ゲーム情報のウェーブを更新する処理
        /// </summary>
        /// <param name="waveNum">ウェーブ数</param>
        private void WaveUpdate(int waveNum)
        {
            // ウェーブ数を加算する
            waveNum++;

            // ゲームの情報を更新する
            var gameDataInfo = new GameDataInfo(
                    GameDataManager.instance.GetGameDataInfo().fortressLife,
                    GameDataManager.instance.GetGameDataInfo().possessionMoney,
                    waveNum);
            GameDataManager.instance.SetGameDataInfo(gameDataInfo);
        }
        #endregion
    }
}