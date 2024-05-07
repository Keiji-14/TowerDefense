using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// 弾の処理
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region PrivateField
        /// <summary>攻撃力</summary>
        private int attack;
        /// <summary>弾の速度</summary>
        private float bulletSpeed;
        /// <summary>追尾対象の敵</summary>
        private GameObject targetEnemy;
        /// <summary>追尾対象の位置</summary>
        private Vector3 targetPosition;
        /// <summary>弾の初期方向</summary>
        private Vector3 bulletInitRot = new Vector3(90.0f, 0.0f, 0.0f);
        #endregion

        #region UnityEvent
        void Update()
        {
            // 追尾対象が存在する場合はその方向に移動する
            if (targetEnemy != null)
            {
                // 追尾対象の位置を更新
                targetPosition = targetEnemy.transform.position;
                // 追尾対象の方向に向かって移動
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
            }
            else
            {
                // 追尾対象が存在しない場合は弾を破棄する
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="enemyObj">追尾対象</param>
        public void Init(int attack, float bulletSpeed, GameObject enemyObj)
        {
            this.attack = attack;
            this.bulletSpeed = bulletSpeed;
            targetEnemy = enemyObj;
            targetPosition = targetEnemy.transform.position;

            transform.Rotate(bulletInitRot);
        }
        #endregion

        #region PrivateMethod
        // 敵が弾に当たった時の処理
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