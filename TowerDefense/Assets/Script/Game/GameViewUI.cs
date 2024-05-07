using GameData;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GameViewUI : MonoBehaviour
    {
        #region SerializeField
        /// <summary>砦の耐久値UI</summary>
        [SerializeField] private TextMeshProUGUI fortressLifeText;
        /// <summary>所持金のUI</summary>
        [SerializeField] private TextMeshProUGUI possessionMoneyText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// UI表示を更新
        /// </summary>
        public void UpdateViewUI()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            fortressLifeText.text = gameDataInfo.fortressLife.ToString();
            possessionMoneyText.text = gameDataInfo.possessionMoney.ToString();
        }
        #endregion
    }
}