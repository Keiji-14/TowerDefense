using UnityEngine;

namespace Game.Tower
{
    public class Bullet : MonoBehaviour
    {
        #region PrivateField
        /// <summary>UŒ‚—Í</summary>
        private int attack;
        /// <summary>’e‚Ì‘¬“x</summary>
        private float bulletSpeed;
        /// <summary>’Ç”ö‘ÎÛ‚Ì“G</summary>
        private GameObject targetEnemy;
        /// <summary>’Ç”ö‘ÎÛ‚ÌˆÊ’u</summary>
        private Vector3 targetPosition;
        #endregion

        #region UnityEvent
        void Update()
        {
            // ’Ç”ö‘ÎÛ‚ª‘¶İ‚·‚éê‡‚Í‚»‚Ì•ûŒü‚ÉˆÚ“®‚·‚é
            if (targetEnemy != null)
            {
                // ’Ç”ö‘ÎÛ‚ÌˆÊ’u‚ğXV
                targetPosition = targetEnemy.transform.position;
                // ’Ç”ö‘ÎÛ‚Ì•ûŒü‚ÉŒü‚©‚Á‚ÄˆÚ“®
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
            }
            else
            {
                // ’Ç”ö‘ÎÛ‚ª‘¶İ‚µ‚È‚¢ê‡‚Í’e‚ğ”jŠü‚·‚é
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ‰Šú‰»
        /// </summary>
        /// <param name="enemyObj">’Ç”ö‘ÎÛ</param>
        public void Init(int attack, float bulletSpeed, GameObject enemyObj)
        {
            this.attack = attack;
            this.bulletSpeed = bulletSpeed;
            targetEnemy = enemyObj;
            targetPosition = targetEnemy.transform.position;
        }
        #endregion

        #region PrivateMethod
        // “G‚ª’e‚É“–‚½‚Á‚½‚Ìˆ—
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy.Enemy>();

                enemy.TakeDamage(attack);

                Destroy(this.gameObject);
            }
        }
        #endregion
    }
}