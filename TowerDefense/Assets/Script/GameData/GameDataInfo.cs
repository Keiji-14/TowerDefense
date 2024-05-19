namespace GameData
{
    /// <summary>
    /// ゲーム中の情報を管理するクラス
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
        /// <summary>ステージがEXモードかどうかの判定</summary>
        public bool isEXStage;
        /// <summary>ゲームクリアかどうかの判定</summary>
        public bool isGameClear;
        /// <summary>ゲームオーバーかどうかの判定</summary>
        public bool isGameOver;

        public GameDataInfo(int fortressLife, int possessionMoney, int waveNum, int score, bool isEXStage, bool isGameClear, bool isGameOver)
        {
            this.fortressLife = fortressLife;
            this.possessionMoney = possessionMoney;
            this.waveNum = waveNum;
            this.score = score;
            this.isEXStage = isEXStage;
            this.isGameClear = isGameClear;
            this.isGameOver = isGameOver;
        }
        #endregion
    }
}
