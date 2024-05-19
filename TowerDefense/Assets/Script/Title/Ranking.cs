using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Title
{
    /// <summary>
    /// ランキング画面の処理
    /// </summary>
    public class Ranking : MonoBehaviour
    {
        #region PublicField
        /// <summary>ステージ選択決定時の処理</summary>
        public Subject<Unit> MainTitleBackSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>メインタイトルに戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMainTitleBackButtonObserver => mainTitleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>メインタイトルに戻るボタン</summary>
        [SerializeField] private Button mainTitleBackBtn;

        [SerializeField] private List<TextMeshProUGUI> rankingTextList = new List<TextMeshProUGUI>();
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            OnClickMainTitleBackButtonObserver.Subscribe(_ =>
            {
                MainTitleBackSubject.OnNext(Unit.Default);
            }).AddTo(this);


        }

        /// <summary>
        /// ランキングを表示する
        /// </summary>
        public void ViewRanking()
        {
            var index = 0;
            foreach (var rankingText in rankingTextList)
            {
                rankingText.text = $"{PlayerPrefs.GetInt($"RANKINGSCORE{index + 1}", 0)}";
                index++;
            }
        }
        #endregion
    }
}