using System.Collections.Generic;
using UnityEngine;

namespace GameData.Stage
{
    /// <summary>
    /// ステージの情報を保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "StageData", menuName = "Create Stage Data")]
    public class StageDataInfo : ScriptableObject
    {
        #region PublicField
        /// <summary>ステージID</summary>
        public int stageID;
        /// <summary>開始時の砦の耐久値</summary>
        public int startFortressLife;
        /// <summary>開始時の所持金</summary>
        public int startMoney;
        /// <summary>ウェーブのインターバル</summary>
        public float waveInterval;
        /// <summary>ステージのオブジェクト</summary>
        public GameObject stageObj;
        /// <summary>砦の場所</summary>
        public Transform fortressTransform;
        /// <summary>1Weveの情報</summary>
        public List<WaveInfo> waveInfo = new List<WaveInfo>();
        #endregion
    }

    /// <summary>
    /// 1Weveの情報
    /// </summary>
    [System.Serializable]
    public class WaveInfo
    {
        #region PublicField
        public int enemyNum;
        /// <summary>敵の出現数</summary>
        public List<EnemySpawnInfo> enemySpawnInfoList = new List<EnemySpawnInfo>();
        /// <summary>敵の出現場所</summary>
        public Transform spawnPoint;
        /// <summary>ルートの中継地点</summary>
        public List<Transform> routeAnchor;
        #endregion
    }

    /// <summary>
    /// 敵の出現情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class EnemySpawnInfo
    {
        #region PublicField
        /// <summary>敵の種類</summary>
        public int enemyID;
        /// <summary>敵の出現数</summary>
        public int enemyNum;
        #endregion
    }
}