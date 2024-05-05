using GameData.Tower;
using UnityEngine;

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
        /// <summary></summary>
        private float lastAttackTime;
        /// <summary>建設したタワーの情報</summary>
        private TowerData towerData;
        #endregion

        #region SerializeField
        /// <summary>弾の発射口</summary>
        [SerializeField] Transform firePoint;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="towerData">タワーの情報</param>
        public void Init(TowerData towerData)
        {
            // タワーの情報を保持させる
            this.towerData = towerData;
        }
        #endregion

        #region PrivateMethod
        private void OnTriggerStay(Collider other)
        {
            // 敵が圏内に入ったかどうか
            if (other.transform.CompareTag("Enemy"))
            {
                ActionType(other.gameObject);
            }
        }

        /// <summary>
        /// 行動パターンの判別
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void ActionType(GameObject enemyObj)
        {
            switch (towerData.towerType)
            {
                case TowerType.MachineGun:
                    LookTarget(enemyObj);
                    MachineGun(enemyObj);
                    break;
                case TowerType.Cannon:
                    LookTarget(enemyObj);
                    break;
                case TowerType.Jamming:
                    break;
            }
        }

        /// <summary>
        /// ターゲットの方向を向く処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void LookTarget(GameObject enemyObj)
        {
            Vector3 targetDirection = enemyObj.transform.position - transform.position;
            targetDirection.y = VerticalLockValue;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 弾を発射する
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void MachineGun(GameObject enemyObj)
        {
            // 1フレームあたりのダメージ量を算出（攻撃速度に応じて）
            if (Time.time - lastAttackTime > (1f / towerData.attackSpeed))
            {
                var bullet = Instantiate(towerData.bulletObj, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
                bullet.Init(towerData.attack, towerData.bulletSpeed, enemyObj);
                // 前回の攻撃時刻を更新
                lastAttackTime = Time.time;
            }
        }

        /// <summary>
        /// 弾を発射する
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void Shot(GameObject enemyObj)
        {
            var enemy = enemyObj.GetComponent<Enemy.Enemy>();

            // 1フレームあたりのダメージ量を算出（攻撃速度に応じて）
            float damage = towerData.attack * (1f / towerData.attackSpeed) * Time.deltaTime;

            enemy.TakeDamage(damage);

            Debug.Log("攻撃");
        }
        #endregion
    }
}