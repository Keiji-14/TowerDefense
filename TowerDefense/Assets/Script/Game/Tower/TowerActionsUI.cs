using Audio;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tower
{
    /// <summary>
    /// タワー強化・売却のUI
    /// </summary>
    public class TowerActionsUI : MonoBehaviour
    {
        #region PublicField
        /// <summary>タワーの強化ボタンを押した時の処理</summary>
        public Subject<TowerStand> TowerUpgradeSubject = new Subject<TowerStand>();
        /// <summary>タワーの売却ボタンを押した時の処理</summary>
        public Subject<TowerStand> TowerSaleSubject = new Subject<TowerStand>();
        #endregion

        #region PrivateField
        /// <summary>生成場所の親オブジェクト</summary>
        private Transform uiCanvas;
        /// <summary>表示しているタワーの説明UI</summary>
        private TowerBuildDescriptionUI towerDescriptionUI;
        /// <summary>タワーの強化ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTowerUpgradeButtonObserver => towerUpgradeBtn.OnClickAsObservable();
        /// <summary>タワーの売却ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTowerSaleButtonObserver => towerSaleBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>タワーの強化ボタン</summary>
        [SerializeField] private Button towerUpgradeBtn;
        /// <summary>タワーの売却ボタン</summary>
        [SerializeField] private Button towerSaleBtn;
        /// <summary>タワー強化・売却UIのコンポーネント</summary>
        [SerializeField] private TowerActionsDescriptionUI towerActionsDescriptionUI;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(TowerStand towerStand)
        {
            uiCanvas = GameObject.FindWithTag("Canvas").transform;

            OnClickTowerUpgradeButtonObserver.Subscribe(_ =>
            {
                TowerUpgradeSubject.OnNext(towerStand);
                SE.instance.Play(SE.SEName.ButtonSE);
            }).AddTo(this);

            OnClickTowerSaleButtonObserver.Subscribe(_ =>
            {
                TowerSaleSubject.OnNext(towerStand);
                SE.instance.Play(SE.SEName.ButtonSE);
            }).AddTo(this);

            var towerDataInfo = towerStand.GetTower().GetTowerDataInfo();

            // レベルが最大の場合
            if (towerDataInfo.level >= towerDataInfo.towerStatusDataInfoList.Count)
            {
                towerUpgradeBtn.interactable = false;
                var towerIncome = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1].towerIncome;

                towerActionsDescriptionUI.ViewLevelMaxTowerText(towerIncome);
            }
            else
            {
                var towerCurrentStatus = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level - 1];
                var towerUpGradeStatus = towerDataInfo.towerStatusDataInfoList[towerDataInfo.level];

                var towerActionsDescriptionInfo =
                    new TowerActionsDescriptionInfo(
                        towerDataInfo.name,
                        towerCurrentStatus.attack,
                        towerUpGradeStatus.attack,
                        towerCurrentStatus.attackSpeed,
                        towerUpGradeStatus.attackSpeed,
                        towerCurrentStatus.firingRange,
                        towerUpGradeStatus.firingRange,
                        towerCurrentStatus.uniqueName,
                        towerCurrentStatus.uniqueStatus,
                        towerUpGradeStatus.uniqueStatus,
                        towerUpGradeStatus.towerCost,
                        towerCurrentStatus.towerIncome
                        );

                towerActionsDescriptionUI.ViewTowerText(towerActionsDescriptionInfo);
            }
        }

        /// <summary>
        /// タワーの説明UIを削除する処理
        /// </summary>
        public void DeleteTowerDescription()
        {
            if (towerDescriptionUI != null)
            {
                Destroy(towerDescriptionUI.gameObject);
                towerDescriptionUI = null;
            }
        }
        #endregion
    }
}