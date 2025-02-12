﻿using Audio;
using GameData;
using GameData.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// タワーについての処理
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        /// <summary>マズルフラッシュの表示時間の値</summary>
        private const float flashTime = 0.1f;
        /// <summary>タワー回転のY軸の値</summary>
        private const float VerticalLockValue = 0f;
        /// <summary>攻撃可能かどうか</summary>
        private bool isShootInterval;
        /// <summary>タワーの回転速度</summary>
        private float rotationSpeed = 5f;
        /// <summary>現在の発射口を追跡する変数</summary>
        private Transform currentFirePoint;
        /// <summary>捕捉した敵のオブジェクト</summary>
        private GameObject targetEnemyObj;
        /// <summary>射程距離のコライダー</summary>
        private CapsuleCollider capsuleCollider;
        /// <summary>建設したタワーの情報</summary>
        private TowerDataInfo towerDataInfo;
        /// <summary>ジャミングした敵</summary>
        private HashSet<Enemy.Enemy> jammedEnemies = new HashSet<Enemy.Enemy>();
        #endregion

        #region SerializeField
        /// <summary>弾の発射口A</summary>
        [SerializeField] Transform firePointA;
        /// <summary>弾の発射口B</summary>
        [SerializeField] Transform firePointB;
        /// <summary>マズルフラッシュのオブジェクト</summary>
        [SerializeField] GameObject muzzleFlashObj;
        /// <summary>発射時の効果音</summary>
        [SerializeField] AudioClip shotSE;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="towerData">タワーの情報</param>
        public void Init(TowerDataInfo towerDataInfo)
        {
            // タワーの情報をディープコピーして保持させる
            this.towerDataInfo = (TowerDataInfo)towerDataInfo.Clone();

            capsuleCollider = GetComponent<CapsuleCollider>();
            // 射程距離を設定
            capsuleCollider.radius = towerDataInfo.towerStatusDataInfoList[0].firingRange;

            // 発射可能状態にする
            isShootInterval = true;

            currentFirePoint = firePointA;
        }

        /// <summary>
        /// タワーのレベルを上げる処理
        /// </summary>
        public void UpGradeTower()
        {
            // タワーのレベルを1上げる
            towerDataInfo.level++;
        }

        /// <summary>
        /// タワーの情報を返す処理
        /// </summary>
        public TowerDataInfo GetTowerDataInfo()
        {
            return towerDataInfo;
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

        private void OnTriggerExit(Collider other)
        {
            // 敵が圏外に出たかどうかを確認し、捕捉を解除する
            if (other.transform.CompareTag("Enemy") && other.gameObject == targetEnemyObj)
            {
                ReleaseTarget();
            }

            if (towerDataInfo.towerType == TowerType.Jamming && other.transform.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy.Enemy>();
                if (enemy != null && jammedEnemies.Contains(enemy))
                {
                    enemy.ResetSpeed();
                    jammedEnemies.Remove(enemy);
                }
            }
        }

        /// <summary>
        /// 行動パターンの判別
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void ActionType(GameObject enemyObj)
        {
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            if (gameDataInfo.isGameOver)
                return;

            switch (towerDataInfo.towerType)
            {
                case TowerType.MachineGun:
                    LookTarget(enemyObj);
                    MachineGun();
                    break;
                case TowerType.Cannon:
                    LookTarget(enemyObj);
                    Cannon();
                    break;
                case TowerType.Jamming:
                    Jamming(enemyObj);
                    break;
            }
        }

        /// <summary>
        /// ターゲットの方向を向く処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void LookTarget(GameObject enemyObj)
        {
            if (targetEnemyObj == null)
            {
                targetEnemyObj = enemyObj;
            }

            if (targetEnemyObj != null)
            {
                Vector3 targetDirection = targetEnemyObj.transform.position - transform.position;
                targetDirection.y = VerticalLockValue;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            capsuleCollider.radius = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1].firingRange;
        }

        /// <summary>
        /// ターゲットの捕捉を解除する
        /// </summary>
        private void ReleaseTarget()
        {
            targetEnemyObj = null;
        }

        /// <summary>
        /// 機関銃の処理
        /// </summary>
        private void MachineGun()
        {
            if (isShootInterval && targetEnemyObj != null)
            {
                isShootInterval = false;
                StartCoroutine(ShotMachineGun());
            }
        }

        /// <summary>
        /// 機関銃の弾を発射する処理
        /// </summary>
        private IEnumerator ShotMachineGun()
        {
            var towerStatusDataInfo = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1];
            // バースト数
            var burstNum = towerStatusDataInfo.uniqueStatus;
            // バーストの間隔
            float burstInterval = 0.05f;

            for (int i = 0; i < burstNum; i++)
            {
                // 発射口の選択
                Transform firePoint = (currentFirePoint == firePointA) ? firePointA : firePointB;

                // 弾を発射
                var bullet = Instantiate(towerDataInfo.bulletObj, firePoint.position, transform.rotation).GetComponent<Bullet>();
                SE.instance.Play(shotSE);
                
                var muzzleFlash = Instantiate(muzzleFlashObj, firePoint.position, transform.rotation);
                Destroy(muzzleFlash, flashTime);

                if (targetEnemyObj != null)
                {
                    bullet.Init(towerStatusDataInfo, towerDataInfo.towerType, targetEnemyObj);
                }

                // 現在の発射口を切り替え
                currentFirePoint = (currentFirePoint == firePointA) ? firePointB : firePointA;

                yield return new WaitForSeconds(burstInterval);
            }

            yield return new WaitForSeconds(towerStatusDataInfo.attackSpeed);

            isShootInterval = true;
        }

        /// <summary>
        /// 大砲の処理
        /// </summary>
        private void Cannon()
        {
            if (isShootInterval && targetEnemyObj != null)
            {
                isShootInterval = false;
                StartCoroutine(ShotCannon());
            }
        }

        /// <summary>
        /// 機関銃の弾を発射する処理
        /// </summary>
        private IEnumerator ShotCannon()
        {
            var towerStatusDataInfo = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1];

            var bullet = Instantiate(towerDataInfo.bulletObj, firePointA.position, firePointA.rotation).GetComponent<Bullet>();
            SE.instance.Play(shotSE);

            var muzzleFlash = Instantiate(muzzleFlashObj, firePointA.position, transform.rotation);
            Destroy(muzzleFlash, flashTime);

            if (targetEnemyObj != null)
            {
                bullet.Init(towerStatusDataInfo, towerDataInfo.towerType, targetEnemyObj);
            }

            yield return new WaitForSeconds(towerStatusDataInfo.attackSpeed);

            isShootInterval = true;
        }

        /// <summary>
        /// ジャミングの処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象</param>
        private void Jamming(GameObject enemyObj)
        {
            var towerStatusDataInfo = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1];

            var enemy = enemyObj.GetComponent<Enemy.Enemy>();
            if (enemy != null && !jammedEnemies.Contains(enemy))
            {
                enemy.ReduceSpeed(towerStatusDataInfo.uniqueStatus);
                jammedEnemies.Add(enemy);
            }
        }
        #endregion
    }
}