using Game.Tower;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// ƒQ[ƒ€‰æ–Ê‚Ìˆ—
    /// </summary>
    #region SerializeField 
    /// <summary>ƒ^ƒ[‚Ìˆ—</summary>
    [SerializeField] private TowerController towerController;
    #endregion

    #region PublicMethod
    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    public void Init()
    {
        towerController.Init();
    }
    #endregion
}
