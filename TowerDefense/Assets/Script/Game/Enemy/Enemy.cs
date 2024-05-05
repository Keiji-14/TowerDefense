using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region PrivateField
        private float HP = 5;

        /// <summary>Rigidbody�R���|�[�l���g</summary>
        private Rigidbody rb;
        /// <summary>NavMeshAgent�R���|�[�l���g</summary>
        private NavMeshAgent agent;
        #endregion

        #region SerializeField
        [SerializeField] private Transform target;
        #endregion

        #region UnityEvent
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();

            agent.destination = target.position;
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// �_���[�W��^���鏈��
        /// </summary>
        /// <param name="damage">�_���[�W��</param>
        public void TakeDamage(float damage)
        {
            HP -= damage;

            LifeCheck();
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// �e�𔭎˂���
        /// </summary>
        private void LifeCheck()
        {
            // HP��0������������ǂ������m�F
            if (IsLifeZero())
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// �̗͂�0������������ǂ����𔻒�
        /// </summary>
        private bool IsLifeZero()
        {
            return HP <= 0;
        }
        #endregion
    }
}