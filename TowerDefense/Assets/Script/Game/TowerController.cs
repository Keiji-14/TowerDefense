using UniRx;
using UnityEngine;

namespace Game.Tower
{
    public class TowerController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>�^���[���݂�UI</summary>
        private TowerBuildUI towerBuildUI; 
        /// <summary>�I�����Ă���^���[</summary>
        private Tower selectionTower;
        #endregion

        #region SerializeField
        /// <summary>�I�����ɓy��̃n�C���C�g</summary>
        [SerializeField] private GameObject uiPrefab;
        /// <summary>�^���[�̏��</summary>
        [SerializeField] private TowerDatabase towerDatabase;
        #endregion

        #region UnityEvent
        void Update()
        {
            // �}�E�X���N���b�N���ꂽ���ǂ������m�F
            if (Input.GetMouseButtonDown(0))
            {
                // �}�E�X�̈ʒu����Ray�𔭎�
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Ray���I�u�W�F�N�g�ɓ����������ǂ������m�F
                if (Physics.Raycast(ray, out hit))
                {
                    // Tower�R���|�[�l���g���A�^�b�`����Ă��邩�ǂ������m�F
                    Tower towerStand = hit.collider.gameObject.GetComponent<Tower>();

                    if (towerStand != null)
                    {
                        if (selectionTower != null)
                        {
                            towerStand.OnTowerClicked();
                            selectionTower.OnTowerClicked();
                            selectionTower = towerStand;
                            ShowTowerUI(towerStand.transform.position);
                        }
                        else
                        {
                            towerStand.OnTowerClicked();
                            selectionTower = towerStand;
                            ShowTowerUI(towerStand.transform.position);
                        }
                    }
                    
                }
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ������
        /// </summary>
        public void Init()
        {

        }
        #endregion

        #region PrivateMethod
        private void ShowTowerUI(Vector3 position)
        {
            // ���ݕ\�����Ă���UI������Δj������
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI);
            }

            // UI���C���X�^���X�����ĕ\������
            towerBuildUI = Instantiate(uiPrefab, position, Quaternion.identity).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(_ =>
            {
                selectionTower.CreateTower();
            }).AddTo(this);
        }
        #endregion
    }
}