using Scene;
using System;
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
        /// <summary>�^�C�g����ʂ̖߂�{�^�������������̏���</summary>
        private IObservable<Unit> OnClickTitleBackButtonObserver => titleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>�^�C�g����ʂ̖߂�{�^��</summary>
        [SerializeField] private Button titleBackBtn;
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
                SceneLoader.Instance().Load(SceneLoader.SceneName.Title);
            }).AddTo(this);
        }
        #endregion
    }
}