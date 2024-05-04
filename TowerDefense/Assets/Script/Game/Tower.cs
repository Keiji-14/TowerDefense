using UnityEngine;
using UniRx;

namespace Game.Tower
{
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        /// <summary>ƒ^ƒ[‰ñ“]‚ÌY²‚Ì’l</summary>
        private const float VerticalLockValue = 0f;
        /// <summary>ƒ^ƒ[‚Ì‰ñ“]‘¬“x</summary>
        private float rotationSpeed = 5f; 
        #endregion

        #region PublicField
        /// <summary>“G‚ğ”­Œ©‚µ‚½‚Ìˆ—</summary>
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
            targetDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Shot();
        }

        /// <summary>
        /// ’e‚ğ”­Ë‚·‚é
        /// </summary>
        private void Shot()
        {
            Debug.Log("UŒ‚");
        }
        #endregion
    }
}