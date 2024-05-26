using NetWark;
using Audio;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// 初回起動時の処理
    /// </summary>
    public class FirstStartup : MonoBehaviour
    {
        #region PublicField
        /// <summaryユーザー情報をタイトル画面に表示する処理</summary>
        public Subject<Unit> ViewUserDataSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>プレイヤー名の初期名</summary>
        private const string initialText = "ユーザー";
        /// <summary>決定ボタンを選択した時の処理</summary>
        private IObservable<Unit> InputEnterObservable =>
            enterBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>決定ボタン</summary>
        [SerializeField] private Button enterBtn;
        /// <summary>名前入力場所</summary>
        [SerializeField] private InputField nameInputField;
        /// <summary>マッチングウィンドウ</summary>
        [SerializeField] private GameObject firstStartupWindow;
        #endregion

        #region UnityEvent
        void Update()
        {
            if (PlayerPrefs.HasKey("FirstTime"))
                return;

            // テキストが含まれているかどうかでボタンを有効にする
            enterBtn.interactable = IsInputFieldValue();
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            firstStartupWindow.SetActive(true);

            nameInputField.text = initialText;
            enterBtn.interactable = false;

            nameInputField.onValueChanged.AddListener(OnInputFieldValueChanged);

            InputEnterObservable.Subscribe(_ =>
            {
                SE.instance.Play(SE.SEName.ButtonSE);

                StartCoroutine(RegisterUserIfNeeded(nameInputField.text));
            }).AddTo(this);
        }

        /// <summary>
        /// 2回目以降の起動時の場合
        /// </summary>
        public void AlreadyStartUp()
        {
            StartCoroutine(AlreadyStartUpCoroutine());
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// ひらがな、カタカナ、英語、一部の記号以外の文字を削除する処理
        /// </summary>
        private void OnInputFieldValueChanged(string value)
        {
            string filteredText = System.Text.RegularExpressions.Regex.Replace(value, "[^ぁ-んァ-ンa-zA-Z0-9!\"#$%&'()*+,./:;<=>?@[\\]^_`{|}ー~]+", "");

            // テキストを更新する
            nameInputField.text = filteredText;
        }

        /// <summary>
        /// テキストが含まれているかどうかの処理
        /// </summary>
        private bool IsInputFieldValue()
        {
            return nameInputField.text.Length > 0;
        }

        private IEnumerator RegisterUserIfNeeded(string name)
        {
            yield return APIClient.Instance().RegisterUserAndGetID(name);
            yield return StartCoroutine(APIClient.Instance().GetUserData());

            PlayerPrefs.SetInt("FirstTime", 1);
            firstStartupWindow.SetActive(false);

            ViewUserDataSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// 2回目以降の起動時の非同期処理
        /// </summary>
        private IEnumerator AlreadyStartUpCoroutine()
        {
            yield return StartCoroutine(APIClient.Instance().GetUserData());
            firstStartupWindow.SetActive(false);
            ViewUserDataSubject.OnNext(Unit.Default);
        }
        #endregion
    }
}