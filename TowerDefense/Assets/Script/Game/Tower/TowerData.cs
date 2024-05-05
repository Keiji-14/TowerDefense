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
        /// <summary>タワーID</summary>
        public int towerID;
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>弾速</summary>
        public float bulletSpeed;
        /// <summary>タワーのオブジェクト</summary>
        public GameObject towerObj;
        /// <summary>弾のオブジェクト</summary>
        public GameObject bulletObj;
        /// <summary>タワーの種類</summary>
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