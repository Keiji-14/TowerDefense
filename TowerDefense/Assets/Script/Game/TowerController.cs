using System.Collections.Generic;
using UnityEngine;

namespace Game.Tower
{
    public class TowerController : MonoBehaviour
    {
        #region PrivateField
        // 現在表示しているUI
        private GameObject currentUI; 
        /// <summary>選択しているタワー</summary>
        private Tower selectionTower;
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] GameObject uiPrefab;
        /// <summary>タワーの情報</summary>
        [SerializeField] TowerDatabase towerDatabase;
        #endregion

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

        void ShowTowerUI(Vector3 position)
        {
            // 現在表示しているUIがあれば破棄する
            if (currentUI != null)
            {
                Destroy(currentUI);
            }

            // UIをインスタンス化して表示する
            currentUI = Instantiate(uiPrefab, position, Quaternion.identity);
        }
    }
}