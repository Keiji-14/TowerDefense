using System.Collections.Generic;
using UnityEngine;

namespace GameData.Tower
{
    /// <summary>
    /// タワーの情報を保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "TowerDatabase", menuName = "Create Tower Database")]
    public class TowerDatabase : ScriptableObject
    {
        #region PublicField
        public List<TowerData> towerDataList = new List<TowerData>();
        #endregion
    }
}