using GameData.Tower;
using GameData.Stage;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        #region PrivateField
        private bool isWaveStart = false;
        /// <summary>出現した敵の保持</summary>
        private StageDataInfo stageDataInfo;
        /// <summary>出現した敵の保持</summary>
        private List<Enemy> enemyList = new List<Enemy>(); 
        #endregion

        #region SerializeField
        [SerializeField] private EnemyDataInfo enemyDataInfo;
        #endregion

        void Update()
        {
            if (isWaveStart)
            {
                isWaveStart = false;
                StartCoroutine(CreateEnemy());
            }
        }

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(StageDataInfo stageDataInfo)
        {
            this.stageDataInfo = stageDataInfo;
            isWaveStart = true;
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 敵を出現させる処理
        /// </summary>
        /// <param name="">攻撃対象</param>
        private IEnumerator CreateEnemy()
        {
            // 機関銃は4点バーストさせる
            for (int i = 0; i < stageDataInfo.waveInfo[0].enemyNum; i++)
            {
                var enemy = Instantiate(enemyDataInfo.enemyObj, stageDataInfo.waveInfo[0].spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
                enemyList.Add(enemy);

                yield return new WaitForSeconds(1f);
            }


            yield return new WaitForSeconds(stageDataInfo.waveInterval);

             isWaveStart= true;
        }
        #endregion
    }
}