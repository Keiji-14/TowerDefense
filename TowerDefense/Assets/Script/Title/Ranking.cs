using GameData;
using NetWark;
using Audio;
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
        /// <summary>メインタイトルに戻る時の処理</summary>
        public Subject<Unit> MainTitleBackSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>メインタイトルに戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMainTitleBackButtonObserver => mainTitleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>メインタイトルに戻るボタン</summary>
        [SerializeField] private Button mainTitleBackBtn;
        /// <summary>順位を表示するテキスト</summary>
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
                SE.instance.Play(SE.SEName.ButtonSE);
                MainTitleBackSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }

        /// <summary>
        /// ランキングを表示する
        /// </summary>
        public void ViewRanking()
        {
            StartCoroutine(APIClient.Instance().GetRankingData(UpdateRankingDisplay));
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// ランキングを表示を更新する処理
        /// </summary>
        /// <param name="rankingData">ランキングの情報</param>
        private void UpdateRankingDisplay(List<UserDataInfo> rankingData)
        {
            for (int i = 0; i < rankingTextList.Count; i++)
            {
                if (i < rankingData.Count)
                {
                    rankingTextList[i].text = $"{rankingData[i].name}: {rankingData[i].highscore}";
                }
                else
                {
                    rankingTextList[i].text = $"---";
                }
            }
        }
        #endregion
    }
}