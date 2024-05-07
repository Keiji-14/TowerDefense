using GameData.Enemy;
using GameData.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /// <summary>ステージの情報</summary>
        [SerializeField] private StageDataInfo stageDataInfo;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
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
    }
}