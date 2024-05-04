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
        /// <summary>建設したタワーの情報</summary>
        private TowerData towerData;
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] GameObject selectionLightObj;

        [SerializeField] TowerData towerdata;
        #endregion

        #region PublicMethod
        public void OnTowerClicked()
        {
            isSelection = !isSelection;
            selectionLightObj.SetActive(isSelection);
        }

        public void CreateTower()
        {
            tower = Instantiate(towerdata.towerObj, transform.position, Quaternion.identity).GetComponent<Tower>();

            tower.transform.SetParent(this.transform);

            TowerBuildSubject.OnNext(tower);
        }
        #endregion
    }
}