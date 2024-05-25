using Scene;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Help
{
    /// <summary>
    /// ヘルプ画面の処理
    /// </summary>
    public class HelpController : SceneBase
    {
        #region PrivateField
        /// <summary>ゲームについてのヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickGameHelpButtonObserver => gameHelpBtn.OnClickAsObservable();
        /// <summary>操作についてのヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickOperationHelpButtonObserver => operationHelpBtn.OnClickAsObservable();
        /// <summary>ステージについてのヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickStageHelpButtonObserver => stageHelpBtn.OnClickAsObservable();
        /// <summary>タワーについてヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTowerHelpButtonObserver => towerHelpBtn.OnClickAsObservable();
        /// <summary>敵についてのヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickEnemyHelpButtonObserver => enemyHelpBtn.OnClickAsObservable();
        /// <summary>ランキングについてのヘルプボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickRankingHelpButtonObserver => rankingHelpBtn.OnClickAsObservable();
        /// <summary>タイトル画面に戻る押した時の処理</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField 
        [SerializeField] private List<HelpInfo> helpInfoList;
        /// <summary>ゲームについてのヘルプのボタン</summary>
        [SerializeField] private Button gameHelpBtn;
        /// <summary>操作についてのヘルプのボタン</summary>
        [SerializeField] private Button operationHelpBtn;
        /// <summary>ステージについてのヘルプのボタン</summary>
        [SerializeField] private Button stageHelpBtn;
        /// <summary>タワーについてのヘルプのボタン</summary>
        [SerializeField] private Button towerHelpBtn;
        /// <summary>敵についてのヘルプのボタン</summary>
        [SerializeField] private Button enemyHelpBtn;
        /// <summary>ランキングについてのヘルプのボタン</summary>
        [SerializeField] private Button rankingHelpBtn;
        /// <summary>タイトル画面に戻るボタン</summary>
        [SerializeField] private Button titleBackBtn;
        /// <summary>ヘルプ選択画面のUIオブジェクト</summary>
        [SerializeField] private GameObject selectHelpUIObj;
        /// <summary>説明画面のUIオブジェクト</summary>
        [SerializeField] private GameObject descriptionHelpUIObj;
        /// <summary>ヘルプの内容を表示</summary>
        [SerializeField] private Help help;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            OnClickGameHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Game]);
            }).AddTo(this);

            OnClickOperationHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Operation]);
            }).AddTo(this);

            OnClickStageHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Stage]);
            }).AddTo(this);

            OnClickTowerHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Tower]);
            }).AddTo(this);

            OnClickEnemyHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Enemy]);
            }).AddTo(this);

            OnClickRankingHelpButtonObserver.Subscribe(_ =>
            {
                SwitchHelpUI(true);
                help.ViewHelp(helpInfoList[(int)HelpType.Ranking]);
            }).AddTo(this);

            OnClickTitleBackButtonObserver.Subscribe(_ =>
            {
                SceneLoader.Instance().UnLoad(SceneLoader.SceneName.Help);
            }).AddTo(this);

            help.Init();

            help.HelpSelectBackSubject.Subscribe(_ =>
            {
                SwitchHelpUI(false);
            }).AddTo(this);
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// UI表示を切り替える
        /// </summary>
        /// <param name="isView">表示判定</param>
        public void SwitchHelpUI(bool isView)
        {
            selectHelpUIObj.SetActive(!isView);
            descriptionHelpUIObj.SetActive(isView);
        }
        #endregion
    }

    [System.Serializable]
    public class HelpInfo
    {
        /// <summary>ヘルプについて</summary>
        public string aboutStr;
        [Multiline(10)]
        /// <summary>ヘルプの内容</summary>
        public string descriptionStr;
    }

    /// <summary>
    /// ヘルプの種類
    /// </summary>
    public enum HelpType
    {
        Game,
        Operation,
        Stage,
        Tower,
        Enemy,
        Ranking
    }
}