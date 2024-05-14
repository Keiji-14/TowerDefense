using GameData;
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
        /// <summary>砦に時の処理</summary>
        public Subject<Unit> FortressDamageSubject = new Subject<Unit>();
        #endregion

        #region PrivateMethod
        // 敵が弾に当たった時の処理
        private void OnTriggerEnter(Collider other)
        {
            if (GameDataManager.instance.GetGameDataInfo().isGameClear ||
                GameDataManager.instance.GetGameDataInfo().isGameOver)
                return;

            if (other.transform.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy.Enemy>();
                enemy.EnemyDestroySubject.OnNext(Unit.Default);

                Destroy(other.gameObject);

                FortressDamageSubject.OnNext(Unit.Default);
            }
        }
        #endregion
    }
}