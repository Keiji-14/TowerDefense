using UnityEngine;

namespace GameData.Tower
{
    /// <summary>
    /// タワーの情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class TowerData
    {
        #region PublicField
        public int towerID;
        public int attack;
        public float attackSpeed;
        public GameObject towerObj;
        public TowerType towerType;
        #endregion
    }

    /// <summary>
    /// タワーの種類
    /// </summary>
    public enum TowerType
    {
        MachineGun,
        Cannon,
        Jamming
    }
}