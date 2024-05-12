using System;
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
        /// <summary>ステージ選択決定時の処理</summary>
        public Subject<int> StageDecisionSubject = new Subject<int>();
        /// <summary>ステージ選択決定時の処理</summary>
        public Subject<Unit> MainTitleBackSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>ステージ選択ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickStageDecisionButtonObserver => stageDecisionBtn.OnClickAsObservable();
        /// <summary>メインタイトルに戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMainTitleBackButtonObserver => mainTitleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>ステージ選択ボタン</summary>
        [SerializeField] private Button stageDecisionBtn;
        /// <summary>メインタイトルに戻るボタン</summary>
        [SerializeField] private Button mainTitleBackBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // ステージ選択画面に遷移する処理
            OnClickStageDecisionButtonObserver.Subscribe(_ =>
            {
                StageDecisionSubject.OnNext(1);
            }).AddTo(this);

            OnClickMainTitleBackButtonObserver.Subscribe(_ =>
            {
                MainTitleBackSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }
        #endregion
    }
}