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
        /// <summary>ゲームクリアかどうかの判定</summary>
        public bool isGameClear;
        /// <summary>ゲームオーバーかどうかの判定</summary>
        public bool isGameOver;

        public GameDataInfo(int fortressLife, int possessionMoney, int waveNum, bool isGameClear, bool isGameOver)
        {
            this.fortressLife = fortressLife;
            this.possessionMoney = possessionMoney;
            this.waveNum = waveNum;
            this.isGameClear = isGameClear;
            this.isGameOver = isGameOver;
        }
        #endregion
    }
}
