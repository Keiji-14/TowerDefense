using GameData;
using Audio;
using System.Collections;
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

        #region SerializeField
        /// <summary>城が破壊された時のオブジェクト</summary>
        [SerializeField] private GameObject destroyObj;
        /// <summary>消滅時の効果音</summary>
        [SerializeField] private AudioClip destroySE;
        /// <summary>消滅時のパーティクル</summary>
        [SerializeField] public ParticleSystem destroyParticle;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 砦が破壊された時の処理
        /// </summary>
        public IEnumerator DestroyFortress()
        {
            SE.instance.Play(destroySE);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            Instantiate(destroyObj, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
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