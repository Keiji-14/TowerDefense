using GameData;
using GameData.Tower;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// タワーの処理を管理
    /// </summary>
    public class TowerController : MonoBehaviour
    {
        #region PublicField
        /// <summary>タワーを建設した時の処理</summary>
        public Subject<int> TowerBuildSubject = new Subject<int>();
        #endregion

        #region PrivateField
        /// <summary>タワー建設のUI</summary>
        private TowerBuildUI towerBuildUI; 
        /// <summary>選択しているタワーの土台</summary>
        private TowerStand selectionTowerStand;
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
        [SerializeField] private List<TowerStand> towerStandList = new List<TowerStand>();
        #endregion

        #region UnityEvent
        void Update()
        {
            MouseDetectionTowerStand();
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
        private void MouseDetectionTowerStand()
        {
            // マウスの位置からRayを発射
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = ~(1 << LayerMask.NameToLayer("Tower"));

            // Rayがオブジェクトに当たったかどうかを確認
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // Towerコンポーネントがアタッチされているかどうかを確認
                TowerStand towerStand = hit.collider.gameObject.GetComponent<TowerStand>();

                if (towerStand != null)
                {
                    if (selectionTowerStand != null)
                    {
                        towerStand.OnTowerClicked();
                        selectionTowerStand.OnTowerClicked();
                        selectionTowerStand = towerStand;
                        ShowTowerUI(towerStand.transform.position);
                    }
                    else
                    {
                        towerStand.OnTowerClicked();
                        selectionTowerStand = towerStand;
                        ShowTowerUI(towerStand.transform.position);
                    }
                }
            }*/

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
                        if (selectionTowerStand != null)
                        {
                            towerStand.OnTowerClicked();
                            selectionTowerStand.OnTowerClicked();
                            selectionTowerStand = towerStand;
                            ShowTowerUI();
                        }
                        else
                        {
                            towerStand.OnTowerClicked();
                            selectionTowerStand = towerStand;
                            ShowTowerUI();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// タワー土台の初期化
        /// </summary>
        private void TowerStandInit(TowerStand towerStand)
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
        private void ShowTowerUI()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                // 現在表示しているUIがあれば破棄する
                if (towerBuildUI != null)
                {
                    Destroy(towerBuildUI.gameObject);
                }

                // UIをインスタンス化して表示する
                towerBuildUI = Instantiate(uiPrefab, new Vector3(960, 540, 0), Quaternion.identity, uiCanvas).GetComponent<TowerBuildUI>();

                towerBuildUI.Init();

                towerBuildUI.TowerBuildSubject.Subscribe(towerType =>
                {
                    var towerData = GetTowerData(towerType);
                    selectionTowerStand.CreateTower(towerData);
                }).AddTo(this);
            }*/
            // 現在表示しているUIがあれば破棄する
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI.gameObject);
            }

            // UIをインスタンス化して表示する
            towerBuildUI = Instantiate(uiPrefab, new Vector3(960, 540, 0), Quaternion.identity, uiCanvas).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(towerType =>
            {
                IsCanBuild(towerType);
            }).AddTo(this);
        }

        /// <summary>
        /// 建設できるかを判定する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        private void IsCanBuild(TowerType towerType)
        {
            // タワーの情報を取得
            var towerData = GetTowerData(towerType);
            // 所持金を取得
            var possessionMoney = GameDataManager.instance.GetGameDataInfo().possessionMoney;

            // 所持金が足りるかを判定
            if (towerData.towerCost <= possessionMoney)
            {
                TowerBuildSubject.OnNext(towerData.towerCost);
                selectionTowerStand.CreateTower(towerData);
            }
            else
            {
                Debug.Log("所持金が足りません");
            }
        }

        /// <summary>
        /// タワーの情報を取得する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        private TowerDataInfo GetTowerData(TowerType towerType)
        {
            return towerDatabase.towerDataList.FirstOrDefault
                (data => data.towerType == towerType);
        }
        #endregion
    }
}