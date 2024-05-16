using UnityEngine;
using TMPro;

namespace Game.Tower
{
    public class TowerActionsDescriptionUI : MonoBehaviour
    {
        #region SerializeField
        /// <summary>タワー名のテキスト</summary>
        [SerializeField] private TextMeshProUGUI nameText;
        /// <summary>攻撃のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackText;
        /// <summary>攻撃速度のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        /// <summary>攻撃のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackUpGradeText;
        /// <summary>攻撃速度のテキスト</summary>
        [SerializeField] private TextMeshProUGUI attackSpeedUpGradeText;
        /// <summary>強化コストのテキスト</summary>
        [SerializeField] private TextMeshProUGUI upGradeCostText;
        /// <summary>売却のテキスト</summary>
        [SerializeField] private TextMeshProUGUI saleText;
        #endregion

        /// <summary>
        /// タワーの情報を表示する
        /// </summary>
        public void ViewTowerText(TowerDescriptionInfo towerDescriptionInfo)
        {
            nameText.text = towerDescriptionInfo.name;
            attackText.text = towerDescriptionInfo.attack.ToString();
            attackSpeedText.text = towerDescriptionInfo.attackSpeed.ToString();
            //upGradeCostText.text = towerDescriptionInfo
        }
    }
}