using Scene;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ƒQ[ƒ€‰æ–Ê‚ÌŠÇ—
    /// </summary>
    public class GameScene : SceneBase
    {
        #region SerializeField 
        /// <summary>ƒQ[ƒ€‰æ–Ê‚Ìˆ—</summary>
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