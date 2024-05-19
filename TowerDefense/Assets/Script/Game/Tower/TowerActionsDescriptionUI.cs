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
        //[SerializeField] private TextMeshProUGUI saleText;
        #endregion

        /// <summary>
        /// タワーの情報を表示する
        /// </summary>
        public void ViewTowerText(TowerBuildDescriptionInfo towerDescriptionInfo)
        {
            nameText.text = towerDescriptionInfo.name;
            attackText.text = towerDescriptionInfo.attack.ToString();
            attackSpeedText.text = towerDescriptionInfo.attackSpeed.ToString();
            //upGradeCostText.text = towerDescriptionInfo
        }
    }
}