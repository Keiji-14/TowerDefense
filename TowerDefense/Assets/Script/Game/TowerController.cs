using UniRx;
using UnityEngine;

namespace Game.Tower
{
    public class TowerController : MonoBehaviour
    {
        #region PrivateField
        /// <summary>タワー建設のUI</summary>
        private TowerBuildUI towerBuildUI; 
        /// <summary>選択しているタワー</summary>
        private Tower selectionTower;
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] private Transform uiCanvas;
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] private GameObject uiPrefab;
        /// <summary>タワーの情報</summary>
        [SerializeField] private TowerDatabase towerDatabase;
        #endregion

        #region UnityEvent
        void Update()
        {
            // マウスがクリックされたかどうかを確認
            if (Input.GetMouseButtonDown(0))
            {
                // マウスの位置からRayを発射
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Rayがオブジェクトに当たったかどうかを確認
                if (Physics.Raycast(ray, out hit))
                {
                    // Towerコンポーネントがアタッチされているかどうかを確認
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
        /// 初期化
        /// </summary>
        public void Init()
        {

        }
        #endregion

        #region PrivateMethod
        private void ShowTowerUI(Vector3 position)
        {
            // 現在表示しているUIがあれば破棄する
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI);
            }

            // UIをインスタンス化して表示する
            towerBuildUI = Instantiate(uiPrefab, new Vector3(0,0,0), Quaternion.identity, uiCanvas).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(_ =>
            {
                selectionTower.CreateTower();
            }).AddTo(this);
        }
        #endregion
    }
}