using System.Collections.Generic;
using UnityEngine;

namespace GameData.Enemy
{
    /// <summary>
    /// 敵の情報を保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Create Enemy Database")]
    public class EnemyDatabase : ScriptableObject
    {
        #region PublicField
        /// <summary>通常の敵</summary>
        public List<EnemyDataInfo> enemyDataInfoList = new List<EnemyDataInfo>();
        /// <summary>ボス級の敵</summary>
        public List<EnemyDataInfo> bossEnemyDataInfoList = new List<EnemyDataInfo>();
        #endregion
    }
}