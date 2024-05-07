using UnityEngine;

namespace GameData.Tower
{
    /// <summary>
    /// 敵の情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class EnemyDataInfo : MonoBehaviour
    {
        #region PublicField
        /// <summary>敵のID</summary>
        public int enemyID;
        /// <summary>敵の耐久値</summary>
        public int life;
        /// <summary>撃破時の獲得する金額</summary>
        public int dropMoney;
        /// <summary>移動速度</summary>
        public float speed;
        /// <summary>敵のオブジェクト</summary>
        public GameObject enemyObj;
        #endregion
    }
}
