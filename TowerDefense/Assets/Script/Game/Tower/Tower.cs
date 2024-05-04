using UnityEngine;
using UniRx;

namespace Game.Tower
{
    /// <summary>
    /// �^���[�ɂ��Ă̏���
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        /// <summary>�^���[��]��Y���̒l</summary>
        private const float VerticalLockValue = 0f;
        /// <summary>�^���[�̉�]���x</summary>
        private float rotationSpeed = 5f; 
        #endregion

        #region PublicField
        /// <summary>�G�𔭌��������̏���</summary>
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
        /// �e�𔭎˂���
        /// </summary>
        private void Shot()
        {
            Debug.Log("�U��");
        }
        #endregion
    }
}