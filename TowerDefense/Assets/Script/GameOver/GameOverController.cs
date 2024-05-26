using GameData;
using Scene;
using Audio;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameOver
{
    /// <summary>
    /// ゲームオーバー画面の処理
    /// </summary>
    public class GameOverController : MonoBehaviour
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
        /// <summary>スコアテキスト</summary>
        [SerializeField] private TextMeshProUGUI scoreText;
        /// <summary>フェードイン・フェードアウトの処理</summary>
        [SerializeField] private FadeController fadeController;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            ViewScoreText();

            // タイトル画面に遷移する処理
            OnClickTitleBackButtonObserver.Subscribe(_ =>
            {
                SE.instance.Play(SE.SEName.ButtonSE);

                StartCoroutine(ChangeScene(SceneLoader.SceneName.Title));
            }).AddTo(this);
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// スコアテキストを表示する処理
        /// </summary>
        private void ViewScoreText()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            // EXステージだった場合はスコアを表示する
            scoreText.gameObject.SetActive(gameDataInfo.stageType == StageType.EX);
            scoreText.text = $"Score:{gameDataInfo.score}";
        }

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