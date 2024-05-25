using System.Collections.Generic;
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
        /// <summary>タワーの名前</summary>
        public string name;
        /// <summary>タワーの種類</summary>
        public TowerType towerType;
        /// <summary>タワーの説明</summary>
        [Multiline(3)]
        public string description;
        /// <summary>タワーのレベル</summary>
        public int level;
        /// <summary>タワーのステータス</summary>
        public List<TowerStatusDataInfo> towerStatusDataInfoList = new List<TowerStatusDataInfo>();
        /// <summary>タワーのオブジェクト</summary>
        public GameObject towerObj;
        /// <summary>弾のオブジェクト</summary>
        public GameObject bulletObj;

        /// <summary>タワーの情報をディープコピーさせる処理</summary>
        public object Clone()
        {
            TowerDataInfo clone = (TowerDataInfo)MemberwiseClone();
            clone.towerStatusDataInfoList = new List<TowerStatusDataInfo>();
            foreach (var status in towerStatusDataInfoList)
            {
                clone.towerStatusDataInfoList.Add(new TowerStatusDataInfo
                {
                    attack = status.attack,
                    attackSpeed = status.attackSpeed,
                    bulletSpeed = status.bulletSpeed,
                    firingRange = status.firingRange,
                    uniqueName = status.uniqueName,
                    uniqueStatus = status.uniqueStatus,
                    towerCost = status.towerCost,
                    towerIncome = status.towerIncome,
                });
            }
            return clone;
        }
        #endregion
    }

    /// <summary>
    /// 強化したタワーの情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class TowerStatusDataInfo
    {
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>弾速</summary>
        public float bulletSpeed;
        /// <summary>射程距離</summary>
        public float firingRange;
        /// <summary>タワー固有値の名前</summary>
        public string uniqueName;
        /// <summary>タワー固有値</summary>
        public float uniqueStatus;
        /// <summary>建設費用</summary>
        public int towerCost;
        /// <summary>売却時の返金額</summary>
        public int towerIncome;
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