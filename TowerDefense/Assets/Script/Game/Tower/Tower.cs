using UnityEngine;
using UniRx;

namespace Game.Tower
{
    /// <summary>
    /// タワーについての処理
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        /// <summary>タワー回転のY軸の値</summary>
        private const float VerticalLockValue = 0f;
        /// <summary>タワーの回転速度</summary>
        private float rotationSpeed = 5f; 
        #endregion

        #region PublicField
        /// <summary>敵を発見した時の処理</summary>
        public Subject<bool> EnemyFoundSubject = new Subject<bool>();
        #endregion

        #region PrivateMethod
        private void OnTriggerStay(Collider other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Attack(other.gameObject);
            }
        }

        private void Attack(GameObject enemy)
        {
            Vector3 targetDirection = enemy.transform.position - transform.position;
            targetDirection.y = VerticalLockValue;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Shot();
        }

        /// <summary>
        /// 弾を発射する
        /// </summary>
        private void Shot()
        {
            Debug.Log("攻撃");
        }
        #endregion
    }
}