using Scene;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// タイトル画面の処理
    /// </summary>
    public class TitleController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>ステージ選択ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickStageSelectButtonObserver => stageSelectBtn.OnClickAsObservable();
        /// <summary>終了ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickExitButtonObserver => exitBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>ステージ選択ボタン</summary>
        [SerializeField] private Button stageSelectBtn;
        /// <summary>終了ボタン</summary>
        [SerializeField] private Button exitBtn;
        /// <summary>タイトル画面のUIオブジェクト</summary>
        [SerializeField] private GameObject mainTitleUIObj;
        /// <summary>ステージ選択画面のUIオブジェクト</summary>
        [SerializeField] private GameObject stageSelectUIObj;
        /// <summary>ステージ選択画面</summary>
        [SerializeField] private StageSelect stageSelect;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            stageSelect.Init();

            // ステージ選択画面を開く処理
            OnClickStageSelectButtonObserver.Subscribe(_ =>
            {
                mainTitleUIObj.SetActive(false);
                stageSelectUIObj.SetActive(true);
            }).AddTo(this);

            // ゲームを終了する処理
            OnClickExitButtonObserver.Subscribe(_ =>
            {
                Application.Quit();
            }).AddTo(this);

            // 選択したステージのゲームを開始する処理
            stageSelect.StageDecisionSubject.Subscribe(stageNum =>
            {
                SceneLoader.Instance().Load(SceneLoader.SceneName.Game);
            }).AddTo(this);

            // ステージ選択画面を閉じる処理
            stageSelect.MainTitleBackSubject.Subscribe(_ =>
            {
                mainTitleUIObj.SetActive(true);
                stageSelectUIObj.SetActive(false);
            }).AddTo(this);
        }
        #endregion
    }
}