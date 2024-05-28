using Scene;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ゲーム画面の管理
    /// </summary>
    public class GameScene : SceneBase
    {
        #region PrivateField 
        /// <summary>シーン遷移待機時間</summary>
        private const float sceneLoaderWaitTime = 2f;
        #endregion

        #region SerializeField 
        /// <summary>ゲーム画面の処理</summary>
        [SerializeField] private GameController gameController;
        #endregion

        #region UnityEvent
        public override void Start()
        {
            base.Start();

            gameController.Init();

            gameController.GameClearSubject.Subscribe(_ =>
            {
                StartCoroutine(ChangeScene(SceneLoader.SceneName.GameClear)); 
            }).AddTo(this);

            gameController.GameOverSubject.Subscribe(_ =>
            {
                StartCoroutine(ChangeScene(SceneLoader.SceneName.GameOver));
            }).AddTo(this);
        }
        #endregion

        #region UnityEvent
        /// <summary>
        /// 選択したステージのゲームシーンに遷移を行う処理
        /// </summary>
        private IEnumerator ChangeScene(SceneLoader.SceneName sceneName)
        {
            yield return new WaitForSeconds(sceneLoaderWaitTime);

            SceneLoader.Instance().Load(sceneName, true);
        }
        #endregion
    }
}