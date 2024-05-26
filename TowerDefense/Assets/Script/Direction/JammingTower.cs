using UnityEngine;

namespace Direction
{
    /// <summary>
    /// ジャミングタワーの演出
    /// </summary>
    public class JammingTower : MonoBehaviour
    {
        #region SerializeField
        /// <summary>回転速度</summary>
        [SerializeField] float rotateSpeed;
        /// <summary>ジャミングタワーの外周オブジェクト</summary>
        [SerializeField] private GameObject haloObj;
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
            if (haloObj != null)
            {
                haloObj.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }
        }
        #endregion
    }
}