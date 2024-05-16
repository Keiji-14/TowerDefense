using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// ステージ選択画面の処理
    /// </summary>
    public class StageSelect : MonoBehaviour
    {
        #region PublicField
        /// <summary>通常ステージの選択を決定時の処理</summary>
        public Subject<int> StageDecisionSubject = new Subject<int>();
        /// <summary>チュートリアルステージ決定時の処理</summary>
        public Subject<Unit> TutorialStageSubject = new Subject<Unit>();
        /// <summary>EXステージ決定時の処理</summary>
        public Subject<Unit> EXStageSubject = new Subject<Unit>();
        /// <summary>ステージ選択決定時の処理</summary>
        public Subject<Unit> MainTitleBackSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>チュートリアルステージのボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTutorialStageButtonObserver => tutorialStageBtn.OnClickAsObservable();
        /// <summary>EXステージのボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickEXStageButtonObserver => exStageBtn.OnClickAsObservable();
        /// <summary>メインタイトルに戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMainTitleBackButtonObserver => mainTitleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>通常ステージのボタンリスト</summary>
        [SerializeField] private List<Button> stageNumBtnList;
        /// <summary>チュートリアルステージのボタン</summary>
        [SerializeField] private Button tutorialStageBtn;
        /// <summary>EXステージのボタン</summary>
        [SerializeField] private Button exStageBtn;
        /// <summary>メインタイトルに戻るボタン</summary>
        [SerializeField] private Button mainTitleBackBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            int stageNum = 1; // ステージ番号を初期化

            foreach (var button in stageNumBtnList)
            {
                // ステージ番号を設定
                int capturedStageNum = stageNum; 

                button.OnClickAsObservable().Subscribe(_ =>
                {
                    StageDecisionSubject.OnNext(capturedStageNum);
                }).AddTo(this);

                stageNum++;
            }

            OnClickTutorialStageButtonObserver.Subscribe(_ =>
            {
                TutorialStageSubject.OnNext(Unit.Default);
            }).AddTo(this);

            OnClickEXStageButtonObserver.Subscribe(_ =>
            {
                EXStageSubject.OnNext(Unit.Default);
            }).AddTo(this);

            OnClickMainTitleBackButtonObserver.Subscribe(_ =>
            {
                MainTitleBackSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }
        #endregion
    }
}