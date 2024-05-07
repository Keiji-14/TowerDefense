using GameData.Tower;
using UniRx;
using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// タワー土台についての処理
    /// </summary>
    public class TowerStand : MonoBehaviour
    {
        #region PublicField
        /// <summary>タワーを建設した時の処理</summary>
        public Subject<Tower> TowerBuildSubject = new Subject<Tower>();
        #endregion

        #region PrivateField
        /// <summary>選択中かどうか</summary>
        private bool isSelection = false;
        /// <summary>建設したタワー</summary>
        private Tower tower;
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] GameObject selectionLightObj;
        #endregion

        #region PublicMethod
        public void OnTowerClicked()
        {
            isSelection = !isSelection;
            selectionLightObj.SetActive(isSelection);
        }

        /// <summary>
        /// タワーを生成する処理
        /// </summary>
        /// <param name="towerData">タワーの情報</param>
        public void CreateTower(TowerDataInfo towerData)
        {
            tower = Instantiate(towerData.towerObj, transform.position, Quaternion.identity).GetComponent<Tower>();

            tower.transform.SetParent(this.transform);

            tower.Init(towerData);

            TowerBuildSubject.OnNext(tower);
        }
        #endregion
    }
}