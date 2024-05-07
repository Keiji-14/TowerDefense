using UnityEngine;

namespace Game.Fortress
{
    /// <summary>
    /// 砦の処理の管理
    /// </summary>
    public class FortressController : MonoBehaviour
    {
        #region PrivateMethod
        // 敵が弾に当たった時の処理
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);

                Debug.Log("砦にダメージ");
            }
        }
        #endregion
    }
}