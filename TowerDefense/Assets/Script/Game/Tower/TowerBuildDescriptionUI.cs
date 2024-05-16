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
        /// <summary>タワーコストのテキスト</summary>
        [SerializeField] private TextMeshProUGUI towerCostText;
        /// <summary>タワー説明のテキスト</summary>
        [SerializeField] private TextMeshProUGUI towerDescriptionText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// タワーの情報を表示する
        /// </summary>
        public void ViewTowerText(TowerDescriptionInfo towerDescriptionInfo)
        {
            nameText.text = towerDescriptionInfo.name;
            attackText.text = towerDescriptionInfo.attack.ToString();
            attackSpeedText.text = towerDescriptionInfo.attackSpeed.ToString() + "s";
            towerCostText.text = towerDescriptionInfo.cose.ToString();
            towerDescriptionText.text = towerDescriptionInfo.description;
        }
        #endregion
    }

    /// <summary>
    /// タワーについての表示する情報
    /// </summary>
    public class TowerDescriptionInfo
    {
        #region PublicField
        /// <summary>名前</summary>
        public string name;
        /// <summary>攻撃力</summary>
        public int attack;
        /// <summary>攻撃速度</summary>
        public float attackSpeed;
        /// <summary>コスト</summary>
        public int cose;
        /// <summary>説明</summary>
        public string description;

        public TowerDescriptionInfo(string name, int attack, float attackSpeed, int cose, string description)
        {
            this.name = name;
            this.attack = attack;
            this.attackSpeed = attackSpeed;
            this.cose = cose;
            this.description = description;
        }
        #endregion
    }
}