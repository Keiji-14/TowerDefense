using System.Collections.Generic;
using UnityEngine;

namespace GameData.Stage
{
    /// <summary>
    /// EXステージの情報を保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "EXStageData", menuName = "Create EX Stage Data")]
    public class EXStageDataInfo : ScriptableObject
    {
        #region PublicField
        /// <summary>ステージID</summary>
        public int stageID;
        /// <summary>開始時の砦の耐久値</summary>
        public int startFortressLife;
        /// <summary>開始時の所持金</summary>
        public int startMoney;
        /// <summary>開始時の敵の出現数</summary>
        public int enemyNum;
        /// <summary>ウェーブのインターバル</summary>
        public float waveInterval;
        /// <summary>ステージのオブジェクト</summary>
        public GameObject stageObj;
        /// <summary>砦の場所</summary>
        public Transform fortressTransform;
        /// <summary>EXステージのルートの情報</summary>
        public List<RouteInfo> routeInfoList = new List<RouteInfo>();
        #endregion
    }

    /// <summary>
    /// EXステージのルートの情報
    /// </summary>
    [System.Serializable]
    public class RouteInfo
    {
        #region PublicField
        /// <summary>敵の出現場所</summary>
        public Transform spawnPoint;
        /// <summary>ルートの中継地点</summary>
        public List<Transform> routeAnchor;
        #endregion
    }
}