using GameData;
using GameData.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    /// <summary>
    /// 敵の処理
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region PublicField
        /// <summary>敵が消滅する時の処理</summary>
        public Subject<int> EnemyDestroySubject = new Subject<int>();
        #endregion

        #region PrivateField
        /// <summary>敵の体力</summary>
        private int life;
        /// <summary>NavMeshAgentコンポーネント</summary>
        private NavMeshAgent agent;
        /// <summary>敵の情報</summary>
        private EnemyDataInfo enemyDataInfo;
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
            agent = GetComponent<NavMeshAgent>();

            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();
            target = stageDataInfo.fortressTransform;

            this.enemyDataInfo = enemyDataInfo;
            life = enemyDataInfo.life;
            agent.speed = enemyDataInfo.speed;

            agent.destination = target.position;
        }

        /// <summary>
        /// ダメージを与える処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public void TakeDamage(int damage)
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
                EnemyDestroySubject.OnNext(enemyDataInfo.dropMoney);
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