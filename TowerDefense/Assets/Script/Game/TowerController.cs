using System.Collections.Generic;
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
        private TowerStand selectionTower;
        /// <summary>建設したタワーのリスト</summary>
        private List<Tower> towerList = new List<Tower>();
        #endregion

        #region SerializeField
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] private Transform uiCanvas;
        /// <summary>選択時に土台のハイライト</summary>
        [SerializeField] private GameObject uiPrefab;
        /// <summary>タワーの情報</summary>
        [SerializeField] private TowerDatabase towerDatabase;
        /// <summary>タワー土台のリスト</summary>
        [SerializeField] List<TowerStand> towerStandList = new List<TowerStand>();
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

                int layerMask = ~(1 << LayerMask.NameToLayer("Tower"));

                // Rayがオブジェクトに当たったかどうかを確認
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    // Towerコンポーネントがアタッチされているかどうかを確認
                    TowerStand towerStand = hit.collider.gameObject.GetComponent<TowerStand>();

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
            foreach (var towerStand in towerStandList)
            {
                TowerStandInit(towerStand);
            }
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// タワー土台の初期化
        /// </summary>
        public void TowerStandInit(TowerStand towerStand)
        {
            // タワーの建設時に通知を行う
            towerStand.TowerBuildSubject.Subscribe(tower =>
            {
                // 建設したタワーをリストに追加
                towerList.Add(tower);
            }).AddTo(this);
        }

        /// <summary>
        /// 建設内を選択するUIを表示する
        /// </summary>
        /// <param name="position">表示する座標</param>
        private void ShowTowerUI(Vector3 position)
        {
            // 現在表示しているUIがあれば破棄する
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI.gameObject);
            }

            // UIをインスタンス化して表示する
            towerBuildUI = Instantiate(uiPrefab, new Vector3(960,540,0), Quaternion.identity, uiCanvas).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(_ =>
            {
                selectionTower.CreateTower();
            }).AddTo(this);
        }
        #endregion
    }
}