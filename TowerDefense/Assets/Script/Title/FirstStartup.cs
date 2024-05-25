﻿using NetWark;
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
        #region PrivateField
        /// <summary>プレイヤー名の初期名</summary>
        private const string initialText = "ユーザー";

        private bool isFirstTime;
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

        [SerializeField] private APIClient apiClient;
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
            isFirstTime = PlayerPrefs.GetInt("FirstTime", 0) == 0;

            if (isFirstTime)
            {
                firstStartupWindow.SetActive(true);

                nameInputField.text = initialText;
                enterBtn.interactable = false;

                nameInputField.onValueChanged.AddListener(OnInputFieldValueChanged);

                InputEnterObservable.Subscribe(_ =>
                {
                    StartCoroutine(RegisterUserIfNeeded(nameInputField.text));
                }).AddTo(this);
            }
            else
            {
                AlreadyStartUp();
            }
        }

        /// <summary>
        /// 2回目以降の起動時の場合
        /// </summary>
        public void AlreadyStartUp()
        {
            StartCoroutine(apiClient.GetUserData());
            firstStartupWindow.SetActive(false);
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
            yield return apiClient.RegisterUserAndGetID(name);

            // ユーザー登録が完了したら、フラグを設定して初回起動処理を終了する
            isFirstTime = false;
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.SetString("UserName", name);
            firstStartupWindow.SetActive(false);
        }
        #endregion
    }
}