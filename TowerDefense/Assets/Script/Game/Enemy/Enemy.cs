using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    /// <summary>
    /// 敵の処理
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region PrivateField
        private float HP = 100;

        /// <summary>Rigidbodyコンポーネント</summary>
        private Rigidbody rb;
        /// <summary>NavMeshAgentコンポーネント</summary>
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

            agent.speed = 1.0f;
            agent.destination = target.position;
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ダメージを与える処理
        /// </summary>
        /// <param name="damage">ダメージ両</param>
        public void TakeDamage(float damage)
        {
            HP -= damage;

            LifeCheck();
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 弾を発射する
        /// </summary>
        private void LifeCheck()
        {
            // HPが0を下回ったかどうかを確認
            if (IsLifeZero())
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// 体力が0を下回ったかどうかを判定
        /// </summary>
        private bool IsLifeZero()
        {
            return HP <= 0;
        }
        #endregion
    }
}