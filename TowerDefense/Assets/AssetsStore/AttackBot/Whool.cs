using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whool : MonoBehaviour
{
    #region SerializeField
    /// <summary>回転速度</summary>
    [SerializeField] float rotateSpeed;
    /// <summary>ジャミングタワーの外周オブジェクト</summary>
    [SerializeField] private GameObject whoolObj;
    #endregion

    #region UnityEvent
    void Update()
    {
        RotateHalo();
    }
    #endregion

    #region PrivateMethod
    /// <summary>
    /// 回転させる処理速度
    /// </summary>
    private void RotateHalo()
    {
        if (whoolObj != null)
        {
            whoolObj.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
    #endregion
}
