using GameData.Tower;
using System.Collections;
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
        /// <summary>攻撃可能かどうか</summary>
        private bool isShootInterval;
        /// <summary>タワーの回転速度</summary>
        private float rotationSpeed = 5f;
        /// <summary></summary>
        private float lastAttackTime;
        /// <summary>現在の発射口を追跡する変数</summary>
        private Transform currentFirePoint;
        /// <summary>建設したタワーの情報</summary>
        private TowerDataInfo towerData;
        #endregion

        #region SerializeField
        /// <summary>弾の発射口A</summary>
        [SerializeField] Transform firePointA;
        /// <summary>弾の発射口B</summary>
        [SerializeField] Transform firePointB;
        /// <summary>マズルフラッシュのパーティクル</summary>
        [SerializeField] ParticleSystem muzzleFlash;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="towerData">タワーの情報</param>
        public void Init(TowerDataInfo towerData)
        {
            // タワーの情報を保持させる
            this.towerData = towerData;
            // 発射可能状態にする
            isShootInterval = true;

            currentFirePoint = firePointA;
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
        /// 機関銃の処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void MachineGun(GameObject enemyObj)
        {
            if (isShootInterval)
            {
                isShootInterval = false;
                StartCoroutine(ShotMachineGun(enemyObj));
            }
        }

        /// <summary>
        /// 機関銃の弾を発射する処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private IEnumerator ShotMachineGun(GameObject enemyObj)
        {
            // バーストの間隔
            float burstInterval = 0.05f;

            // 機関銃は4点バーストさせる
            for (int i = 0; i < 4; i++)
            {
                // 発射口の選択
                Transform firePoint = (currentFirePoint == firePointA) ? firePointA : firePointB;

                // 弾を発射
                var bullet = Instantiate(towerData.bulletObj, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
                bullet.Init(towerData.attack, towerData.bulletSpeed, enemyObj);

                // 現在の発射口を切り替え
                currentFirePoint = (currentFirePoint == firePointA) ? firePointB : firePointA;

                yield return new WaitForSeconds(burstInterval);
            }

            yield return new WaitForSeconds(towerData.attackSpeed);

            isShootInterval = true;
        }
        #endregion
    }
}