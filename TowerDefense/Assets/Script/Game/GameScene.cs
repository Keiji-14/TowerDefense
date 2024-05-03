using Scene;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// �Q�[����ʂ̊Ǘ�
    /// </summary>
    public class GameScene : SceneBase
    {
        #region SerializeField 
        /// <summary>�Q�[����ʂ̏���</summary>
        [SerializeField] private GameController gameController;
        #endregion

        #region UnityEvent
        public override void Start()
        {
            base.Start();

            gameController.Init();
        }
        #endregion
    }
}