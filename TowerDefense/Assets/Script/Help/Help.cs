using System;
using UniRx;
using UnityEngine;
using Audio;
using UnityEngine.UI;
using TMPro;

namespace Help
{
    /// <summary>
    /// ヘルプの表示
    /// </summary>
    public class Help : MonoBehaviour
    {
        #region PublicField
        /// <summary>ヘルプ選択画面に戻る時の処理</summary>
        public Subject<Unit> HelpSelectBackSubject = new Subject<Unit>();
        #endregion

        /// <summary>ヘルプ選択画面に戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickGameHelpSelectBackButtonObserver => helpSelectBackBtn.OnClickAsObservable();

        #region SerializeField 
        /// <summary>ヘルプ選択画面に戻るボタン</summary>
        [SerializeField] private Button helpSelectBackBtn;
        /// <summary>ヘルプについてテキスト</summary>
        [SerializeField] private TextMeshProUGUI aboutHelpText;
        /// <summary>ヘルプの内容テキスト</summary>
        [SerializeField] private TextMeshProUGUI descriptionHelpText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            OnClickGameHelpSelectBackButtonObserver.Subscribe(_ =>
            {
                SE.instance.Play(SE.SEName.ButtonSE);

                HelpSelectBackSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }

        /// <summary>
        /// ヘルプの表示
        /// </summary>
        public void ViewHelp(HelpInfo helpInfo)
        {
            aboutHelpText.text = helpInfo.aboutStr;
            descriptionHelpText.text = helpInfo.descriptionStr;
        }
        #endregion
    }
}