using Scene;
using UnityEngine;

namespace Help
{
    /// <summary>
    /// ヘルプ画面の管理
    /// </summary>
    public class HelpScene : SceneBase
    {
        #region SerializeField 
        /// <summary>ヘルプ画面の処理</summary>
        [SerializeField] private HelpController helpController;
        #endregion

        #region UnityEvent
        public override void Start()
        {
            base.Start();

            helpController.Init();
        }
        #endregion
    }
}