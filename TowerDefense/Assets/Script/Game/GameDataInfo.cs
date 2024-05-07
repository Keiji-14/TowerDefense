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
        public int money;

        public GameDataInfo(int fortressLife, int money)
        {
            this.fortressLife = fortressLife;
            this.money = money;
        }
        #endregion
    }
}
