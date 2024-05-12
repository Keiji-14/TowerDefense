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
        public List<EnemyDataInfo> enemyDataInfoList = new List<EnemyDataInfo>();
        #endregion
    }
}