using GameData.Enemy;
using GameData.Stage;
using GameData.Tower;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameData
{
    public class GameDataManager : MonoBehaviour
    {
        #region PublicField
        public static GameDataManager instance = null;
        #endregion

        #region PrivateField
        /// <summary>ゲームの情報</summary>
        private GameDataInfo gameDataInfo;
        /// <summary></summary>
        private TowerDatabase towerDatabase;
        /// <summary>ステージの情報</summary>
        [SerializeField] private StageDataInfo stageDataInfo;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DataInit();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ゲームの情報を設定する
        /// </summary>
        public void SetGameDataInfo(GameDataInfo gameDataInfo)
        {
            this.gameDataInfo = gameDataInfo;
        }

        /// <summary>
        /// ゲームの情報を返す
        /// </summary>
        public GameDataInfo GetGameDataInfo()
        {
            return gameDataInfo;
        }


        /// <summary>
        /// タワーの情報を取得する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        public TowerDataInfo GetTowerData(TowerType towerType)
        {
            return towerDatabase.towerDataList.FirstOrDefault
                (data => data.towerType == towerType);
        }

        /// <summary>
        /// ステージの情報を設定する
        /// </summary>
        public void SetStageDataInfo(StageDataInfo stageDataInfo)
        {
            this.stageDataInfo = stageDataInfo;
        }

        /// <summary>
        /// ステージの情報を返す
        /// </summary>
        public StageDataInfo GetStageDataInfo()
        {
            return stageDataInfo;
        }
        #endregion

        private void DataInit()
        {
             Addressables.LoadAssetAsync<TowerDatabase>("TowerDatabase.asset").Completed += handle =>
             {
                 if (handle.Result == null)
                 {
                     Debug.Log("Load Error");
                     return;
                 }
                 towerDatabase = handle.Result;
             };
        }
    }
}