using UnityEngine;

namespace GameData.Tower
{
    /// <summary>
    /// タワーの情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class TowerDataInfo
    {
        #region PublicField
        /// <summary>タワーID</summary>
        public int towerID;
        /// <summary>タワーの種類</summary>
        public TowerType towerType;
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>建設費用</summary>
        public int towerCost;
        /// <summary>売却時の返金額</summary>
        public int towerIncome;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>弾速</summary>
        public float bulletSpeed;
        /// <summary>タワーのオブジェクト</summary>
        public GameObject towerObj;
        /// <summary>弾のオブジェクト</summary>
        public GameObject bulletObj;
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