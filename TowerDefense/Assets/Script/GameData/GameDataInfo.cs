using System;

namespace GameData
{
    /// <summary>
    /// ゲーム中の情報
    /// </summary>
    [System.Serializable]
    public class GameDataInfo
    {
        #region PublicField
        /// <summary>砦の耐久値</summary>
        public int fortressLife;
        /// <summary>所持金</summary>
        public int possessionMoney;
        /// <summary>現在のウェーブ数</summary>
        public int waveNum;
        /// <summary>スコア（EXステージ）</summary>
        public int score;
        /// <summary>ゲームクリアかどうかの判定</summary>
        public bool isGameClear;
        /// <summary>ゲームオーバーかどうかの判定</summary>
        public bool isGameOver;
        /// <summary>ステージタイプ</summary>
        public StageType stageType;

        public GameDataInfo(int fortressLife, int possessionMoney, int waveNum, int score, bool isGameClear, bool isGameOver, StageType stageType)
        {
            this.fortressLife = fortressLife;
            this.possessionMoney = possessionMoney;
            this.waveNum = waveNum;
            this.score = score;
            this.isGameClear = isGameClear;
            this.isGameOver = isGameOver;
            this.stageType = stageType;
        }
        #endregion
    }

    /// <summary>
    /// ステージの種類
    /// </summary>
    public enum StageType
    {
        Default,
        Tutorial,
        EX,
    }
}
