using UnityEngine;

namespace Direction
{
    /// <summary>
    /// スカイボックスを回転させる処理
    /// </summary>
    public class RotationSky : MonoBehaviour
    {
        #region SerializeField 
        /// <summary>回転速度</summary>
        [SerializeField] float rotateSpeed;
        #endregion

        #region PrivateField
        /// <summary>回転速度の補正値値</summary>
        private const float rotationSpeedCorrection = 0.001f;
        /// <summary>回転した値</summary>
        private float rotationRepeatValue;
        /// <summary>スカイボックスのマテリアル</summary>
        private Material sky;
        #endregion

        #region UnityEvent
        void Start()
        {
            // Lightのスカイボックスを取得
            sky = RenderSettings.skybox;
        }

        void Update()
        {
            // スカイドームを回転させる処理
            rotationRepeatValue = Mathf.Repeat(sky.GetFloat("_Rotation") + rotateSpeed * rotationSpeedCorrection, 360f);
            sky.SetFloat("_Rotation", rotationRepeatValue);
        }
        #endregion
    }
}