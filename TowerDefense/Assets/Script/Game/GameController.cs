using GameData;
using Game.Enemy;
using Game.Tower;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ゲーム画面の処理
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>ゲーム中の情報</summary>
        private GameDataInfo gameDataInfo;
        #endregion

        #region SerializeField 
        /// <summary>タワーの処理</summary>
        [SerializeField] private TowerController towerController;
        /// <summary>敵の処理</summary>
        [SerializeField] private EnemyController enemyController;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            gameDataInfo = new GameDataInfo(10, 100);

            towerController.Init();
            enemyController.Init();
        }
        #endregion
    }
}