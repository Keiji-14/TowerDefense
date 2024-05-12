using System.Collections.Generic;
using UnityEngine;

namespace GameData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Create Enemy Database")]
    public class EnemyDatabase : ScriptableObject
    {
        #region PublicField
        public List<EnemyDataInfo> enemyDataInfoList = new List<EnemyDataInfo>();
        #endregion
    }
}