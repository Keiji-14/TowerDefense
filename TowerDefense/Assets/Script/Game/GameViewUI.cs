using GameData;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GameViewUI : MonoBehaviour
    {
        #region SerializeField
        /// <summary>ウェーブ数表示の補正値</summary>
        private const int correctionWaveNum = 1;
        #endregion

        #region SerializeField
        /// <summary>砦の耐久値UI</summary>
        [SerializeField] private TextMeshProUGUI fortressLifeText;
        /// <summary>所持金のUI</summary>
        [SerializeField] private TextMeshProUGUI possessionMoneyText;
        /// <summary>ウェーブ数のUI</summary>
        [SerializeField] private TextMeshProUGUI waveText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// UI表示を更新
        /// </summary>
        public void UpdateViewUI()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

            fortressLifeText.text = gameDataInfo.fortressLife.ToString();
            possessionMoneyText.text = gameDataInfo.possessionMoney.ToString();
            waveText.text = $"{gameDataInfo.waveNum + correctionWaveNum}/{stageDataInfo.waveInfo.Count}";
        }
        #endregion
    }
}