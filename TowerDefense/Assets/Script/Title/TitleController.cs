using GameData;
using GameData.Stage;
using Scene;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using TMPro;

namespace Title
{
    /// <summary>
    /// タイトル画面の処理
    /// </summary>
    public class TitleController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>ウェーブ数の初期化</summary>
        private const int waveInitNum = 0;
        /// <summary>スコアの初期化</summary>
        private const int scoreInitNum = 0;
        /// <summary>シーン遷移待機時間</summary>
        private const float sceneLoaderWaitTime = 2f;
        /// <summary>ステージ選択ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickStageSelectButtonObserver => stageSelectBtn.OnClickAsObservable();
        /// <summary>チュートリアルステージのボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTutorialStageButtonObserver => tutorialStageBtn.OnClickAsObservable();
        /// <summary>ヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickHelpButtonObserver => helpBtn.OnClickAsObservable();
        /// <summary>ランキングボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickRankingButtonObserver => rankingBtn.OnClickAsObservable();
        /// <summary>終了ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickExitButtonObserver => exitBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>ステージ選択ボタン</summary>
        [SerializeField] private Button stageSelectBtn;
        /// <summary>チュートリアルステージのボタン</summary>
        [SerializeField] private Button tutorialStageBtn;
        /// <summary>ヘルプボタン</summary>
        [SerializeField] private Button helpBtn;
        /// <summary>ランキングのボタン</summary>
        [SerializeField] private Button rankingBtn;
        /// <summary>終了ボタン</summary>
        [SerializeField] private Button exitBtn;
        /// <summary>タイトル画面のUIオブジェクト</summary>
        [SerializeField] private GameObject mainTitleUIObj;
        /// <summary>ステージ選択画面のUIオブジェクト</summary>
        [SerializeField] private GameObject stageSelectUIObj;
        /// <summary>ランキング画面のUIオブジェクト</summary>
        [SerializeField] private GameObject rankingUIObj;
        /// <summary>ユーザー情報のテキスト</summary>
        [SerializeField] private TextMeshProUGUI userDataText;
        /// <summary>ステージ選択画面</summary>
        [SerializeField] private StageSelect stageSelect;
        /// <summary>ランキング画面</summary>
        [SerializeField] private Ranking ranking;
        /// <summary>初回起動時の処理</summary>
        [SerializeField] private FirstStartup firstStartup;
        /// <summary>フェードイン・フェードアウトの処理</summary>
        [SerializeField] private FadeController fadeController;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            FirstStartupInit();

            stageSelect.Init();
            ranking.Init();

            // ステージ選択画面を開く処理
            OnClickStageSelectButtonObserver.Subscribe(_ =>
            {
                mainTitleUIObj.SetActive(false);
                stageSelectUIObj.SetActive(true);
            }).AddTo(this);

            // チュートリアルステージの設定を行う処理
            OnClickTutorialStageButtonObserver.Subscribe(_ =>
            {
                SetTutorialStage();
            }).AddTo(this);

            // ヘルプ画面に遷移を行う処理
            OnClickHelpButtonObserver.Subscribe(_ =>
            {
                SceneLoader.Instance().Load(SceneLoader.SceneName.Help, true);
            }).AddTo(this);

            // ゲームを終了する処理
            OnClickExitButtonObserver.Subscribe(_ =>
            {
                Application.Quit();
            }).AddTo(this);

            // ランキング画面を開く処理
            OnClickRankingButtonObserver.Subscribe(_ =>
            {
                ranking.ViewRanking();
                mainTitleUIObj.SetActive(false);
                rankingUIObj.SetActive(true);
            }).AddTo(this);

            // 通常ステージの設定を行う処理
            stageSelect.StageDecisionSubject.Subscribe(stageNum =>
            {
                SetDefaultStage(stageNum);
            }).AddTo(this);

            // EXステージの設定を行う処理
            stageSelect.EXStageSubject.Subscribe(_ =>
            {
                SetEXStage();
            }).AddTo(this);

            // ステージ選択画面を閉じる処理
            stageSelect.MainTitleBackSubject.Subscribe(_ =>
            {
                mainTitleUIObj.SetActive(true);
                stageSelectUIObj.SetActive(false);
            }).AddTo(this);

            // ランキング画面を閉じる処理
            ranking.MainTitleBackSubject.Subscribe(_ =>
            {
                mainTitleUIObj.SetActive(true);
                rankingUIObj.SetActive(false);
            }).AddTo(this);
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 初回起動時の初期化処理
        /// </summary>
        private void FirstStartupInit()
        {
            var isFirstTime = PlayerPrefs.GetInt("FirstTime", 0) == 0;

            firstStartup.ViewUserDataSubject.Subscribe(_ =>
            {
                ViewUserData();
            }).AddTo(this);

            if (isFirstTime)
            {
                userDataText.gameObject.SetActive(false);
                firstStartup.Init();
            }
            else
            {
                firstStartup.AlreadyStartUp();
            }
        }


        public void ViewUserData()
        {
            var userDataInfo = GameDataManager.instance.GetUserDataInfo();
            userDataText.text = $"UID：{userDataInfo.id}　Name：{userDataInfo.name}";

            userDataText.gameObject.SetActive(true);
        }

        /// <summary>
        /// 通常ステージの設定を行う処理
        /// </summary>
        private void SetDefaultStage(int stageNum)
        {
            Addressables.LoadAssetAsync<StageDataInfo>($"StageData{stageNum}.asset").Completed += handle =>
            {
                if (handle.Result == null)
                {
                    Debug.Log("Load Error");
                    return;
                }
                var stageDataInfo = handle.Result;
                GameDataManager.instance.SetStageDataInfo(stageDataInfo);

                // ゲーム情報をを初期化
                var gameDataInfo = new GameDataInfo(stageDataInfo.startFortressLife, stageDataInfo.startMoney, waveInitNum, scoreInitNum, false, false, StageType.Default);
                GameDataManager.instance.SetGameDataInfo(gameDataInfo);
            };

            StartCoroutine(ChangeGameScene());
        }

        /// <summary>
        /// チュートリアルステージの設定を行う処理
        /// </summary>
        private void SetTutorialStage()
        {
            Addressables.LoadAssetAsync<StageDataInfo>("TutorialStageData.asset").Completed += handle =>
            {
                if (handle.Result == null)
                {
                    Debug.Log("Load Error");
                    return;
                }
                var stageDataInfo = handle.Result;
                GameDataManager.instance.SetStageDataInfo(stageDataInfo);

                // ゲーム情報をを初期化
                var gameDataInfo = new GameDataInfo(stageDataInfo.startFortressLife, stageDataInfo.startMoney, waveInitNum, scoreInitNum, false, false, StageType.Tutorial);
                GameDataManager.instance.SetGameDataInfo(gameDataInfo);
            };

            StartCoroutine(ChangeGameScene());
        }

        /// <summary>
        /// EXステージの設定を行う処理
        /// </summary>
        private void SetEXStage()
        {
            Addressables.LoadAssetAsync<EXStageDataInfo>("EXStageData.asset").Completed += handle =>
            {
                if (handle.Result == null)
                {
                    Debug.Log("Load Error");
                    return;
                }
                var stageDataInfo = handle.Result;
                GameDataManager.instance.SetEXStageDataInfo(stageDataInfo);

                // ゲーム情報をを初期化
                var gameDataInfo = new GameDataInfo(stageDataInfo.startFortressLife, stageDataInfo.startMoney, waveInitNum, scoreInitNum, false, false, StageType.EX);
                GameDataManager.instance.SetGameDataInfo(gameDataInfo);
            };

            StartCoroutine(ChangeGameScene());
        }

        /// <summary>
        /// 選択したステージのゲームシーンに遷移を行う処理
        /// </summary>
        private IEnumerator ChangeGameScene()
        {
            fadeController.fadeOut = true;

            yield return new WaitForSeconds(sceneLoaderWaitTime);
            
            SceneLoader.Instance().Load(SceneLoader.SceneName.Game);
        }
        #endregion
    }
}