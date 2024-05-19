﻿using GameData;
using GameData.Tower;
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
        /// <summary>説明UIの生成するX座標の補正値</summary>
        private const int correctionDescriptionUIPosX = 270;
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
        /// <summary>タワー説明UIオブジェクト</summary>
        [SerializeField] private GameObject towerDescriptionUIObj;
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
            }).AddTo(this);

            OnClickTowerSaleButtonObserver.Subscribe(_ =>
            {
                TowerSaleSubject.OnNext(towerStand);
            }).AddTo(this);
        }

        /// <summary>
        /// タワーの説明を表示するかどうか
        /// </summary>
        public void IsViewTowerDescription(bool isView, Vector3 createPos, TowerType towerType)
        {
            if (isView)
            {
                towerDescriptionUI = Instantiate(towerDescriptionUIObj, createPos, Quaternion.identity, uiCanvas).GetComponent<TowerBuildDescriptionUI>();

                //var towerData = //GameDataManager.instance.GetTowerData(towerType);
                //var towerDescriptionInfo = new TowerDescriptionInfo(towerData.name, towerData.attack, towerData.attackSpeed, towerData.towerCost, towerData.description);
                //towerDescriptionUI.ViewTowerText(towerDescriptionInfo);
            }
            else
            {
                Destroy(towerDescriptionUI.gameObject);
                towerDescriptionUI = null;
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