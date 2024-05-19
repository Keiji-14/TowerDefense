using GameData;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GameViewUI : MonoBehaviour
    {
        #region PrivateField
        /// <summary>ウェーブ数表示の補正値</summary>
        private const int correctionWaveNum = 1;
        #endregion

        #region SerializeField
        /// <summary>砦の耐久値UI</summary>
        [SerializeField] private TextMeshProUGUI fortressLifeText;
        /// <summary>所持金のUI</summary>
        [SerializeField] private TextMeshProUGUI possessionMoneyText;
        [Header("DefaultStage")]
        /// <summary>通常ステージ時のUI</summary>
        [SerializeField] private GameObject defaultStageUI;
        /// <summary>ウェーブ数のUI</summary>
        [SerializeField] private TextMeshProUGUI defaultStageWaveText;
        [Header("EXStage")]
        /// <summary>EXステージ時のUI</summary>
        [SerializeField] private GameObject exStageUI;
        /// <summary>現在のウェーブ数のUI</summary>
        [SerializeField] private TextMeshProUGUI exStageWaveText;
        /// <summary>スコアのUI</summary>
        [SerializeField] private TextMeshProUGUI scorePointText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            defaultStageUI.SetActive(!gameDataInfo.isEXStage);
            exStageUI.SetActive(gameDataInfo.isEXStage);

            UpdateViewUI();
        }

        /// <summary>
        /// UI表示を更新
        /// </summary>
        public void UpdateViewUI()
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            fortressLifeText.text = $"{gameDataInfo.fortressLife}";
            possessionMoneyText.text = $"{gameDataInfo.possessionMoney}";

            if (gameDataInfo.isEXStage)
            {
                exStageWaveText.text = $"{gameDataInfo.waveNum + correctionWaveNum}";
                scorePointText.text = $"{gameDataInfo.score}";
            }
            else
            {
                var stageDataInfo = GameDataManager.instance.GetStageDataInfo();
                defaultStageWaveText.text = $"{gameDataInfo.waveNum + correctionWaveNum}/{stageDataInfo.waveInfo.Count}";
            }
        }
        #endregion
    }
}