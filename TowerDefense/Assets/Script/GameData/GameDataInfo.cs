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

        public GameDataInfo(int fortressLife, int money, int waveNum)
        {
            this.fortressLife = fortressLife;
            this.possessionMoney = money;
            this.waveNum = waveNum;
    }
        #endregion
    }
}
