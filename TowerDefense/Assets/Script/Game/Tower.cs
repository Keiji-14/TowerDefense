using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// タワーについての処理
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        private bool isSelection = false;
        private TowerData towerData;
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] GameObject selectionLightObj;

        [SerializeField] TowerData tower;
        #endregion

        #region PublicMethod
        public void OnTowerClicked()
        {
            isSelection = !isSelection;
            selectionLightObj.SetActive(isSelection);
        }

        public void CreateTower()
        {
            Instantiate(tower.towerObj, transform.position, Quaternion.identity);
        }
        #endregion
    }
}