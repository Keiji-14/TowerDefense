using UnityEngine;
using TMPro;

namespace Game.Tower
{
    public class TowerActionsDescriptionUI : MonoBehaviour
    {
        #region SerializeField
        [Header("Name")]
        /// <summary>タワー名のテキスト</summary>
        [SerializeField] private TextMeshProUGUI nameText;
        [Header("Attack")]
        /// <summary>攻撃のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackText;
        /// <summary>強化後の攻撃のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackUpGradeText;
        [Header("AttackSpeed")]
        /// <summary>攻撃速度のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        /// <summary>強化後の攻撃速度のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackSpeedUpGradeText;
        [Header("FiringRange")]
        /// <summary>射程距離のテキスト</summary>
        [SerializeField] private TextMeshProUGUI firingRangeText;
        /// <summary>強化後の射程距離のテキスト</summary>
        [SerializeField] private TextMeshProUGUI firingRangeUpGradeText;
        [Header("Unique")]
        /// <summary>固有の名前テキスト</summary>
        [SerializeField] private TextMeshProUGUI uniqueNameText;
        /// <summary>固有値のテキスト</summary>
        [SerializeField] private TextMeshProUGUI uniqueStatusText;
        /// <summary>強化後の固有値のテキスト</summary>
        [SerializeField] private TextMeshProUGUI uniqueStatusUpGradeText;
        [Header("UpGradeCost")]
        /// <summary>強化コストのテキスト</summary>
        [SerializeField] private TextMeshProUGUI upGradeCostText;
        /// <summary>売却のテキスト</summary>
        [SerializeField] private TextMeshProUGUI saleText;
        [Header("UpGradeCost")]
        /// <summary>タワーのステータスを表示するUIオブジェクト</summary>
        [SerializeField] private GameObject towerStatusUIObj;
        /// <summary>最大レベル時に表示するUIオブジェクト</summary>
        [SerializeField] private GameObject levelMaxUIObj;
        #endregion

        /// <summary>
        /// タワーの情報を表示する
        /// </summary>
        /// <param name="towerActionsDescriptionInfo">タワーの強化・売却についての表示する情報</param>
        public void ViewTowerText(TowerActionsDescriptionInfo towerActionsDescriptionInfo)
        {
            towerStatusUIObj.SetActive(true);
            levelMaxUIObj.SetActive(false);

            nameText.text = $"{towerActionsDescriptionInfo.name}";
            attackText.text = $"{towerActionsDescriptionInfo.attack}";
            attackUpGradeText.text = $"{towerActionsDescriptionInfo.attackUpgrade}";
            attackSpeedText.text = $"{towerActionsDescriptionInfo.attackSpeed}s";
            attackSpeedUpGradeText.text = $"{towerActionsDescriptionInfo.attackSpeedUpgrade}s";
            firingRangeText.text = $"{towerActionsDescriptionInfo.firingRange}";
            firingRangeUpGradeText.text = $"{towerActionsDescriptionInfo.firingRangeUpgrade}";
            uniqueNameText.text = $"{towerActionsDescriptionInfo.uniqueName}";
            uniqueStatusText.text = $"{towerActionsDescriptionInfo.uniqueStatus}";
            uniqueStatusUpGradeText.text = $"{towerActionsDescriptionInfo.uniqueStatusUpgrade}";
            upGradeCostText.text = $"{towerActionsDescriptionInfo.upGradeCost}";
            saleText.text = $"{towerActionsDescriptionInfo.sale}";
        }

        /// <summary>
        /// 最大レベル時のタワーの情報を表示する
        /// </summary>
        /// <param name="sale">売却金</param>
        public void ViewLevelMaxTowerText(int sale)
        {
            towerStatusUIObj.SetActive(false);
            levelMaxUIObj.SetActive(true);

            saleText.text = $"{sale}";
        }
    }

    /// <summary>
    /// タワーの強化・売却についての表示する情報
    /// </summary>
    public class TowerActionsDescriptionInfo
    {
        #region PublicField
        /// <summary>名前</summary>
        public string name;
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>強化後の攻撃力</summary>
        public int attackUpgrade;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>強化後の攻撃速度</summary>
        public float attackSpeedUpgrade;
        /// <summary>射程距離</summary>
        public float firingRange;
        /// <summary>強化後の射程距離</summary>
        public float firingRangeUpgrade;
        /// <summary>固有値の名前</summary>
        public string uniqueName;
        /// <summary>固有値</summary>
        public float uniqueStatus;
        /// <summary>強化後の固有値</summary>
        public float uniqueStatusUpgrade;
        /// <summary>強化コスト</summary>
        public int upGradeCost;
        /// <summary>説明</summary>
        public int sale;

        public TowerActionsDescriptionInfo(
            string name, 
            int attack, 
            int attackUpgrade, 
            float attackSpeed, 
            float attackSpeedUpgrade, 
            float firingRange, 
            float firingRangeUpgrade, 
            string uniqueName, 
            float uniqueStatus, 
            float uniqueStatusUpgrade, 
            int upGradeCost, 
            int sale)
        {
            this.name = name;
            this.attack = attack;
            this.attackUpgrade = attackUpgrade;
            this.attackSpeed = attackSpeed;
            this.attackSpeedUpgrade = attackSpeedUpgrade;
            this.firingRange = firingRange;
            this.firingRangeUpgrade = firingRangeUpgrade;
            this.uniqueName = uniqueName;
            this.uniqueStatus = uniqueStatus;
            this.uniqueStatusUpgrade = uniqueStatusUpgrade;
            this.upGradeCost = upGradeCost;
            this.sale = sale;
        }
        #endregion
    }
}