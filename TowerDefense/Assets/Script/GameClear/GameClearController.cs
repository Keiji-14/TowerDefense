using Scene;
using System;
using System.Collections;
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
        /// <summary>シーン遷移待機時間</summary>
        private const float sceneLoaderWaitTime = 2f;
        /// <summary>タイトル画面の戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>タイトル画面の戻るボタン</summary>
        [SerializeField] private Button titleBackBtn;
        /// <summary>フェードイン・フェードアウトの処理</summary>
        [SerializeField] private FadeController fadeController;
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
                StartCoroutine(ChangeScene(SceneLoader.SceneName.Title));
            }).AddTo(this);
        }
        #endregion

        #region PrivateMethod

        /// <summary>
        /// シーン遷移を行う処理
        /// </summary>
        private IEnumerator ChangeScene(SceneLoader.SceneName sceneName)
        {
            fadeController.fadeOut = true;

            yield return new WaitForSeconds(sceneLoaderWaitTime);

            SceneLoader.Instance().Load(sceneName);
        }
        #endregion
    }
}