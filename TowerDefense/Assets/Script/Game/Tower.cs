using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// �^���[�ɂ��Ă̏���
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        private bool isSelection = false;
        private TowerData towerData;
        #endregion

        #region SerializeField
        /// <summary>�I�����ɓy��̃n�C���C�g</summary>
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