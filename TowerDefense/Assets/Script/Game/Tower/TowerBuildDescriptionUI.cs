using UnityEngine;
using TMPro;

namespace Game.Tower
{
    /// <summary>
    /// タワーについての説明UIを表示する処理
    /// </summary>
    public class TowerBuildDescriptionUI : MonoBehaviour
    {
        #region SerializeField
        /// <summary>タワー名のテキスト</summary>
        [SerializeField] private TextMeshProUGUI nameText;
        /// <summary>攻撃のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackText;
        /// <summary>攻撃速度のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        /// <summary>射程距離のテキスト</summary>
        [SerializeField] private TextMeshProUGUI firingRangeText;
        /// <summary>タワー固有値の名前のテキスト</summary>
        [SerializeField] private TextMeshProUGUI uniqueNameText;
        /// <summary>タワーの固有値のテキスト</summary>
        [SerializeField] private TextMeshProUGUI uniqueStatusText;
        /// <summary>タワーコストのテキスト</summary>
        [SerializeField] private TextMeshProUGUI towerCostText;
        /// <summary>タワー説明のテキスト</summary>
        [SerializeField] private TextMeshProUGUI towerDescriptionText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// タワーの情報を表示する
        /// </summary>
        /// <param name="towerDescriptionInfo">タワーの建設についての表示する情報</param>
        public void ViewTowerText(TowerBuildDescriptionInfo towerDescriptionInfo)
        {
            nameText.text = $"{towerDescriptionInfo.name}";
            attackText.text = $"{towerDescriptionInfo.attack}";
            attackSpeedText.text = $"{towerDescriptionInfo.attackSpeed}s";
            firingRangeText.text = $"{towerDescriptionInfo.firingRange}";
            uniqueNameText.text = $"{towerDescriptionInfo.uniqueName}";
            uniqueStatusText.text = $"{towerDescriptionInfo.uniqueStatus}";
            towerCostText.text = $"{towerDescriptionInfo.cose}";
            towerDescriptionText.text = $"{towerDescriptionInfo.description}";
        }
        #endregion
    }

    /// <summary>
    /// タワーの建設についての表示する情報
    /// </summary>
    public class TowerBuildDescriptionInfo
    {
        #region PublicField
        /// <summary>名前</summary>
        public string name;
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>射程距離</summary>
        public float firingRange;
        /// <summary>固有値の名前</summary>
        public string uniqueName;
        /// <summary>固有値</summary>
        public float uniqueStatus;
        /// <summary>コスト</summary>
        public int cose;
        /// <summary>説明</summary>
        public string description;

        public TowerBuildDescriptionInfo(
            string name, 
            int attack, 
            float attackSpeed, 
            float firingRange, 
            string uniqueName, 
            float uniqueStatus, 
            int cose, 
            string description)
        {
            this.name = name;
            this.attack = attack;
            this.attackSpeed = attackSpeed;
            this.firingRange = firingRange;
            this.uniqueName = uniqueName;
            this.uniqueStatus = uniqueStatus;
            this.cose = cose;
            this.description = description;
        }
        #endregion
    }
}