using Scene;
using UnityEngine;

namespace GameClear
{
    /// <summary>
    /// ゲームクリア画面の管理
    /// </summary>
    public class GameClearScene : SceneBase
    {
        #region SerializeField 
        /// <summary>ゲームクリア画面の処理</summary>
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