using Scene;
using UnityEngine;

namespace GameClear
{
    /// <summary>
    /// �Q�[���N���A��ʂ̊Ǘ�
    /// </summary>
    public class GameClearScene : SceneBase
    {
        #region SerializeField 
        /// <summary>�Q�[���N���A��ʂ̏���</summary>
        [SerializeField] private GameClearController gameClearController;
        #endregion

        #region UnityEvent
        public override void Start()
        {
            base.Start();

            gameClearController.Init();
        }
        #endregion
    }
}