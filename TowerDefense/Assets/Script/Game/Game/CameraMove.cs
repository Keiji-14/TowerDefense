using UnityEngine;

namespace Game
{
    /// <summary>
    /// カメラの移動処理
    /// </summary>
    public class CameraMove : MonoBehaviour
    {
        #region SerializeField 
        /// <summary>カメラの移動速度</summary>
        [SerializeField] private float moveSpeed;
        /// <summary>カメラの移動範囲の最小値</summary>
        [SerializeField] private Vector3 minBounds;
        /// <summary>カメラの移動範囲の最大値</summary>
        [SerializeField] private Vector3 maxBounds;
        #endregion

        #region UnityEvent
        void Update()
        {
            Move();
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// カメラの動き
        /// </summary>
        private void Move()
        {
            // 入力を取得
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 入力値に応じて移動させる
            Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

            // カメラの方向に合わせて移動させる
            movement = transform.TransformDirection(movement);
            movement.y = 0f; // Y方向の移動は無視する

            // 現在の位置に移動分を加算
            Vector3 newPosition = transform.position + movement;

            // 新しい位置が境界内にあるかチェック
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

            // カメラの位置を更新
            transform.position = newPosition;
        }
        #endregion
    }
}