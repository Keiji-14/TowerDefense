using Game.Enemy;
using Game.Tower;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// ゲーム画面の処理
    /// </summary>
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
        towerController.Init();
    }
    #endregion
}
