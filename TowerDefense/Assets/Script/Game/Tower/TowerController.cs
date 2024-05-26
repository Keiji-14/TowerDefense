using GameData;
using GameData.Tower;
using Audio;
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
        /// <summary>タワーを売却した時の処理</summary>
        public Subject<int> TowerSaleSubject = new Subject<int>();
        /// <summary>次の説明へ移行する処理(チュートリアルに使用)</summary>
		public Subject<Unit> NextDescriptionSubject = new Subject<Unit>();
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
        [SerializeField] private Transform createParent;
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
        /// タワー土台を選択する処理
        /// </summary>
        private void SelectTowerStand(TowerStand towerStand)
        {
            if (towerStand != null)
            {
                if (selectionTowerStand == towerStand)
                {
                    towerStand.OnTowerClicked();
                    DestroyTowerUI();
                }
                else if (selectionTowerStand != null && towerStand.GetTower() != null)
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerActionsUI(selectionTowerStand);
                    DestroyTowerBuildUI();
                }
                else if (selectionTowerStand != null && towerStand.GetTower() == null)
                {
                    towerStand.OnTowerClicked();
                    selectionTowerStand.OnTowerClicked();
                    selectionTowerStand = towerStand;
                    ShowTowerBuildUI();
                    DestroyTowerActionsUI();
                }
                else if (selectionTowerStand == null && towerStand.GetTower() != null)
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

                SE.instance.Play(SE.SEName.ButtonSE);
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
        /// 建設するタワーを選択するUIを表示する
        /// </summary>
        private void ShowTowerBuildUI()
        {
            // 現在表示しているUIがあれば破棄する
            if (towerBuildUI != null)
            {
                Destroy(towerBuildUI.gameObject);
            }

            // UIをインスタンス化して表示する
            towerBuildUI = Instantiate(towerBuildUIObj, new Vector3(960, 540, 0), Quaternion.identity, createParent).GetComponent<TowerBuildUI>();

            towerBuildUI.Init();

            towerBuildUI.TowerBuildSubject.Subscribe(towerType =>
            {
                IsCanBuild(towerType);

                if (GameDataManager.instance.GetGameDataInfo().stageType == StageType.Tutorial)
                {
                    NextDescriptionSubject.OnNext(Unit.Default);
                }
            }).AddTo(this);

            if (GameDataManager.instance.GetGameDataInfo().stageType == StageType.Tutorial)
            {
                NextDescriptionSubject.OnNext(Unit.Default);
            }
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
            towerActionsUI = Instantiate(towerActionsUIObj, new Vector3(960, 540, 0), Quaternion.identity, createParent).GetComponent<TowerActionsUI>();
            towerActionsUI.Init(towerStand);

            towerActionsUI.TowerUpgradeSubject.Subscribe(towerStand =>
            {
                IsCanUpGrade(towerStand);
            }).AddTo(this);

            towerActionsUI.TowerSaleSubject.Subscribe(towerStand =>
            {
                var towerDataInfo = towerStand.GetTower().GetTowerDataInfo();
                var towerIncome = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1].towerIncome;

                TowerSaleSubject.OnNext(towerIncome);

                selectionTowerStand.OnTowerClicked();
                towerStand.DestroyTower();
                DestroyTowerUI();
            }).AddTo(this);


            if (GameDataManager.instance.GetGameDataInfo().stageType == StageType.Tutorial)
            {
                NextDescriptionSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 建設できるかを判定する処理
        /// </summary>
        /// <param name="towerType">タワーの種類</param>
        private void IsCanBuild(TowerType towerType)
        {
            // タワーの情報を取得
            var towerData = GameDataManager.instance.GetTowerData(towerType);
            var buildCost = towerData.towerStatusDataInfoList[0].towerCost;
            // 所持金を取得
            var possessionMoney = GameDataManager.instance.GetGameDataInfo().possessionMoney;

            // 所持金が足りるかを判定
            if (buildCost <= possessionMoney)
            {
                TowerBuildSubject.OnNext(buildCost);
                selectionTowerStand.OnTowerClicked();
                selectionTowerStand.CreateTower(towerData);

                DestroyTowerUI();
            }
            else
            {
                Debug.Log("所持金が足りません");
            }
        }

        /// <summary>
        /// 強化できるかを判定する処理
        /// </summary>
        /// <param name="towerStand">タワーの土台</param>
        private void IsCanUpGrade(TowerStand towerStand)
        {
            var towerDataInfo = towerStand.GetTower().GetTowerDataInfo();
            var upGradeCost = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level].towerCost;
            var possessionMoney = GameDataManager.instance.GetGameDataInfo().possessionMoney;

            if (upGradeCost <= possessionMoney)
            {
                TowerBuildSubject.OnNext(upGradeCost);
                selectionTowerStand.OnTowerClicked();

                var tower = towerStand.GetTower();
                tower.UpGradeTower();

                DestroyTowerUI();
            }
            else
            {
                Debug.Log("所持金が足りません");
            }
        }

        /// <summary>
        /// タワーのUIを削除する処理
        /// </summary>
        private void DestroyTowerUI()
        {
            if (towerBuildUI != null)
            {
                DestroyTowerBuildUI();
            }

            if (towerActionsUI != null)
            {
                DestroyTowerActionsUI();
            }

            selectionTowerStand = null;

            if (GameDataManager.instance.GetGameDataInfo().stageType == StageType.Tutorial)
            {
                NextDescriptionSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// タワーの建設UIを削除する処理
        /// </summary>
        private void DestroyTowerBuildUI()
        {
            towerBuildUI.DeleteTowerDescription();
            Destroy(towerBuildUI.gameObject);
            towerBuildUI = null;
        }

        /// <summary>
        /// タワーの強化・売却UIを削除する処理
        /// </summary>
        private void DestroyTowerActionsUI()
        {
            towerActionsUI.DeleteTowerDescription();
            Destroy(towerActionsUI.gameObject);
            towerActionsUI = null;
        }
        #endregion
    }
}