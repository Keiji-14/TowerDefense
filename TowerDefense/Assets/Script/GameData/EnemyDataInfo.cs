using UnityEngine;

namespace GameData.Enemy
{
    /// <summary>
    /// 敵の情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class EnemyDataInfo
    {
        #region PublicField
        /// <summary>敵のID</summary>
        public int enemyID;
        /// <summary>敵の耐久値</summary>
        public int life;
        /// <summary>撃破時の獲得する金額</summary>
        public int dropMoney;
        /// <summary>撃破時の獲得するポイント(EXステージ用)</summary>
        public int scorePoint;
        /// <summary>移動速度</summary>
        public float speed;
        /// <summary>垂直変位</summary>
        public float baseOffset;
        /// <summary>敵のオブジェクト</summary>
        public GameObject enemyObj;
        #endregion
    }
}
