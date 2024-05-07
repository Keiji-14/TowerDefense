﻿using Scene;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameOver
{
    /// <summary>
    /// ゲームオーバー画面の処理
    /// </summary>
    public class GameOverController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>リトライボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickRetryButtonObserver => retryBtn.OnClickAsObservable();
        /// <summary>タイトル画面の戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>リトライボタン</summary>
        [SerializeField] private Button retryBtn;
        /// <summary>タイトル画面の戻るボタン</summary>
        [SerializeField] private Button titleBackBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // ゲーム画面に遷移する処理
            OnClickRetryButtonObserver.Subscribe(_ =>
            {
                SceneLoader.Instance().Load(SceneLoader.SceneName.Game);
            }).AddTo(this);

            // タイトル画面に遷移する処理
            OnClickTitleBackButtonObserver.Subscribe(_ =>
            {
                SceneLoader.Instance().Load(SceneLoader.SceneName.Title);
            }).AddTo(this);
        }
        #endregion
    }
}