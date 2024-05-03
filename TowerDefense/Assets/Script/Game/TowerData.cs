using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーの情報を管理するクラス
/// </summary>
[System.Serializable]
public class TowerData
{
    #region PublicField
    public int towerID;
    public int attack;
    public float attackSpeed;
    public GameObject towerObj;
    #endregion
}
