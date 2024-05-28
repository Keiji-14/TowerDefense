﻿using GameData;
using UniRx;
using UnityEngine;

namespace Game.Fortress
{
    /// <summary>
    /// 砦の処理の管理
    /// </summary>
    public class FortressController : MonoBehaviour
    {
        #region PublicField
        /// <summary>砦にダメージを与える時の処理</summary>
        public Subject<int> FortressDamageSubject = new Subject<int>();
        #endregion

        #region PrivateMethod
        private void OnTriggerEnter(Collider other)
        {
            if (GameDataManager.instance.GetGameDataInfo().isGameClear ||
                GameDataManager.instance.GetGameDataInfo().isGameOver)
                return;

            // 砦に敵に当たった時の処理
            if (other.transform.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy.Enemy>();
                var enemyDataInfo = enemy.enemyDataInfo;
                
                enemy.EnemyDestroySubject.OnNext(Unit.Default);

                Destroy(other.gameObject);

                FortressDamageSubject.OnNext(enemyDataInfo.attack);
            }
        }
        #endregion
    }
}