using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^���[�̏���ێ�����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "TowerDatabase", menuName = "Create Tower Database")]
public class TowerDatabase : ScriptableObject
{
    public List<TowerData> towerDataList = new List<TowerData>();
}
