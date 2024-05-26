using System.Collections;
using UnityEngine;

namespace Direction
{
    /// <summary>
    /// エフェクトの処理
    /// </summary>
    public class Effect : MonoBehaviour
    {
        #region SerializeField
        /// <summary>エフェクトの表示時間</summary>
        [SerializeField] private float effectTime;
        #endregion

        #region UnityEvent
        void Start()
        {
            StartCoroutine(DestroyEffect());
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// エフェクトを消滅させる非同期処理
        /// </summary>
        private IEnumerator DestroyEffect()
        {
            yield return new WaitForSeconds(effectTime);
            GameObject.Destroy(this.gameObject);
        }
        #endregion
    }
}