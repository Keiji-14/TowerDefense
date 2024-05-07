using GameData;
using GameData.Enemy;
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
        private float life = 10;
        /// <summary>Rigidbodyコンポーネント</summary>
        private Rigidbody rb;
        /// <summary>NavMeshAgentコンポーネント</summary>
        private NavMeshAgent agent;
        #endregion

        #region SerializeField
        [SerializeField] private Transform target;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(EnemyDataInfo enemyDataInfo)
        {
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();

            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

            target = stageDataInfo.fortressTransform;

            life = enemyDataInfo.life;
            agent.speed = enemyDataInfo.speed;

            agent.destination = target.position;
        }

        /// <summary>
        /// ダメージを与える処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public void TakeDamage(float damage)
        {
            life -= damage;

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
            return life <= 0;
        }
        #endregion
    }
}