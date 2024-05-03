using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒ^ƒ[‚Ìî•ñ‚ğ•Û‚·‚éScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "TowerDatabase", menuName = "Create Tower Database")]
public class TowerDatabase : ScriptableObject
{
    public List<TowerData> towerDataList = new List<TowerData>();
}
