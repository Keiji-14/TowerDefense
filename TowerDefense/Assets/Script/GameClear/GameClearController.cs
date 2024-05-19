using Scene;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameClear
{
    /// <summary>
    /// ゲームクリア画面の処理
    /// </summary>
    public class GameClearController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>タイトル画面の戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>タイトル画面の戻るボタン</summary>
        [SerializeField] private Button titleBackBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // タイトル画面に遷移する処理
            OnClickTitleBackButtonObserver.Subscribe(_ =>
            {
                SceneLoader.Instance().Load(SceneLoader.SceneName.Title);
            }).AddTo(this);
        }
        #endregion
    }
}