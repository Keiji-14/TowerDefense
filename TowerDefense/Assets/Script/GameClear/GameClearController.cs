using Scene;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameClear
{
    /// <summary>
    /// �Q�[���N���A��ʂ̏���
    /// </summary>
    public class GameClearController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>�V�[���J�ڑҋ@����</summary>
        private const float sceneLoaderWaitTime = 2f;
        /// <summary>�^�C�g����ʂ̖߂�{�^�������������̏���</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>�^�C�g����ʂ̖߂�{�^��</summary>
        [SerializeField] private Button titleBackBtn;
        /// <summary>�t�F�[�h�C���E�t�F�[�h�A�E�g�̏���</summary>
        [SerializeField] private FadeController fadeController;
        #endregion

        #region PublicMethod
        /// <summary>
        /// ������
        /// </summary>
        public void Init()
        {
            // �^�C�g����ʂɑJ�ڂ��鏈��
            OnClickTitleBackButtonObserver.Subscribe(_ =>
            {
                StartCoroutine(ChangeScene(SceneLoader.SceneName.Title));
            }).AddTo(this);
        }
        #endregion

        #region PrivateMethod

        /// <summary>
        /// �V�[���J�ڂ��s������
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