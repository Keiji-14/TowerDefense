using UnityEngine;

namespace Game
{
    public class CameraMove : MonoBehaviour
    {
        #region SerializeField 
        /// <summary>カメラの移動速度</summary>
        [SerializeField] private float moveSpeed;
        #endregion

        #region UnityEvent
        void Update()
        {
            // 入力を取得
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            // 入力値に応じて移動させる
            Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
            
            // カメラの方向に合わせて移動させる
            movement = transform.TransformDirection(movement);
            movement.y = 0f; // Y方向の移動は無視する
            
            // カメラの水平移動
            transform.position += movement;
        }
        #endregion
    }
}