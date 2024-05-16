using GameData;
using GameData.Tower;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

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
        /// <summary>タワー強化・売却のUI</summary>
        private TowerActionsUI towerActionsUI;
        /// <summary>選択しているタワーの土台</summary>
        private TowerStand selectionTowerStand;
        /// <summary>建設したタワーのリスト</summary>
        private List<Tower> towerList = new List<Tower>();
        #endregion

        #region SerializeField
        /// <summary>生成場所の親オブジェクト</summary>
        [SerializeField] private Transform uiCanvas;
        /// <summary>タワー建設のUIオブジェクト</summary>
        [SerializeField] private GameObject towerBuildUIObj;
        /// <summary>タワー強化・売却のUIオブジェクト</summary>
        [SerializeField] private GameObject towerActionsUIObj;
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
            var obj = GameObject.FindWithTag("TowerStandList");

            foreach (Transform child in obj.transform)
            {
                var towerStand = child.GetComponent<TowerStand>();

                if (towerStand != null)
                {
                    towerStandList.Add(towerStand);
                    TowerStandInit(towerStand);
                }
            }
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// タワー土台をクリック選択する処理
        /// </summary>
        private void MouseDetectionTowerStand()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // マウスの位置からRayを発射
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // UIの貫通を防ぐ
                if (EventSystem.current.IsPointerOverGameObject()) 
                    return;

                int layerMask = ~(1 << LayerMask.NameToLayer("Tower"));

                // Rayがオブジェクトに当たったかどうかを確認
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    // Towerコンポーネントがアタッチされているかどうかを確認
                    TowerStand towerStand = hit.collider.gameObject.GetComponent<TowerStand>();

                    SelectTowerStand(towerStand);
                }
            }
        }

        /// <summary>
        /// タワーと代を選択する処理
        /// </summary>
        private void SelectTowerStand(TowerStand towerStand)
        {
            if (towerStand != null)
            {
                if (selectionTowerStand == towerStand)
                {
                    towerStand.OnTowerClicked();
                    DestroyTowerBuildUI();
                }
                else if (selectionTowerStand != null && towerStand.IsBulidedTower() != null)
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerActionsUI(selectionTowerStand);
                }
                else if (selectionTowerStand != null && towerStand.IsBulidedTower() == null)
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerBuildUI();
                }
                else if (selectionTowerStand == null && towerStand.IsBulidedTower() != null)
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerActionsUI(selectionTowerStand);
                }
                else
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerBuildUI();
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
        private void ShowTowerBuildUI()
        {
            // 現在表示しているUIがあれば破棄する
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI.gameObject);
            }

            // UIをインスタンス化して表示する
            towerBuildUI = Instantiate(towerBuildUIObj, new Vector3(960, 540, 0), Quaternion.identity, uiCanvas).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(towerType =>
            {
                IsCanBuild(towerType);
            }).AddTo(this);
        }

        /// <summary>
        /// 建設内を選択するUIを表示する
        /// </summary>
        private void ShowTowerActionsUI(TowerStand towerStand)
        {
            // 現在表示しているUIがあれば破棄する
            if (towerActionsUI != null)
            {
                Destroy(towerActionsUI.gameObject);
            }

            // UIをインスタンス化して表示する
            towerActionsUI = Instantiate(towerActionsUIObj, new Vector3(960, 540, 0), Quaternion.identity, uiCanvas).GetComponent<TowerActionsUI>();
            towerActionsUI.Init(towerStand);
        }

        /// <summary>
        /// 建設できるかを判定する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        private void IsCanBuild(TowerType towerType)
        {
            // タワーの情報を取得
            var towerData = GameDataManager.instance.GetTowerData(towerType);
            var towerStatus = towerData.towerStatusDataInfoList[0];
            // 所持金を取得
            var possessionMoney = GameDataManager.instance.GetGameDataInfo().possessionMoney;

            // 所持金が足りるかを判定
            if (towerStatus.towerCost <= possessionMoney)
            {
                TowerBuildSubject.OnNext(towerStatus.towerCost);
                selectionTowerStand.OnTowerClicked();
                selectionTowerStand.CreateTower(towerData);

                DestroyTowerBuildUI();
            }
            else
            {
                Debug.Log("所持金が足りません");
            }
        }

        /// <summary>
        /// 建設できるかを判定する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        private void IsCanUpGrade(TowerType towerType)
        {
            
        }

        /// <summary>
        /// 建設UIを削除する処理
        /// </summary>
        private void DestroyTowerBuildUI()
        {
            towerBuildUI.DeleteTowerDescription();

            Destroy(towerBuildUI.gameObject);
            towerBuildUI = null;
            selectionTowerStand = null;
        }
        #endregion
    }
}