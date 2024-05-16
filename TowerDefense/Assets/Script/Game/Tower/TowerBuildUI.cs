using GameData;
using GameData.Tower;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tower
{
    /// <summary>
    /// タワー建設のUI
    /// </summary>
    public class TowerBuildUI : MonoBehaviour
    {
        #region PublicField
        public Subject<TowerType> TowerBuildSubject = new Subject<TowerType>();
        #endregion

        #region PrivateField
        /// <summary>説明UIの生成するX座標の補正値</summary>
        private const int correctionDescriptionUIPosX = 270;
        /// <summary>生成場所の親オブジェクト</summary>
        private Transform uiCanvas;
        /// <summary>表示しているタワーの説明UI</summary>
        private TowerBuildDescriptionUI towerDescriptionUI;
        /// <summary>タワー建設ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMachineGunTowerBuildButtonObserver => machineGunTowerBuildBtn.OnClickAsObservable();
        /// <summary>タワー建設ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickCannonTowerBuildButtonObserver => cannonTowerBuildBtn.OnClickAsObservable();
        /// <summary>タワー建設ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickJammingTowerBuildButtonObserver => jammingTowerBuildBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>機関銃タワー建設ボタン</summary>
        [SerializeField] private Button machineGunTowerBuildBtn;
        /// <summary>大砲タワー建設ボタン</summary>
        [SerializeField] private Button cannonTowerBuildBtn;
        /// <summary>ジャミングタワー建設ボタン</summary>
        [SerializeField] private Button jammingTowerBuildBtn;
        /// <summary>タワー説明UIオブジェクト</summary>
        [SerializeField] private GameObject towerDescriptionUIObj;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            uiCanvas = GameObject.FindWithTag("Canvas").transform;

            OnClickMachineGunTowerBuildButtonObserver.Subscribe(_ =>
            {
                TowerBuildSubject.OnNext(TowerType.MachineGun);
            }).AddTo(this);
            
            OnClickCannonTowerBuildButtonObserver.Subscribe(_ =>
            {
                TowerBuildSubject.OnNext(TowerType.Cannon);
            }).AddTo(this);

            OnClickJammingTowerBuildButtonObserver.Subscribe(_ =>
            {
                TowerBuildSubject.OnNext(TowerType.Jamming);
            }).AddTo(this);

            var machineGunTowerUIHandler = machineGunTowerBuildBtn.GetComponent<TowerBuildButtonHandler>();
            machineGunTowerUIHandler.TowerDescriptionSubject.Subscribe(isView =>
            {
                var createPos = new Vector3(
                        machineGunTowerBuildBtn.transform.position.x + correctionDescriptionUIPosX,
                        machineGunTowerBuildBtn.transform.position.y,
                        machineGunTowerBuildBtn.transform.position.z);

                IsViewTowerDescription(isView, createPos, TowerType.MachineGun);
            }).AddTo(this);

            var cannonTowerUIHandler = cannonTowerBuildBtn.GetComponent<TowerBuildButtonHandler>();
            cannonTowerUIHandler.TowerDescriptionSubject.Subscribe(isView =>
            {
                var createPos = new Vector3(
                        cannonTowerBuildBtn.transform.position.x + correctionDescriptionUIPosX,
                        cannonTowerBuildBtn.transform.position.y,
                        cannonTowerBuildBtn.transform.position.z);

                IsViewTowerDescription(isView, createPos, TowerType.Cannon);
            }).AddTo(this);

            var jammingTowerUIHandler = jammingTowerBuildBtn.GetComponent<TowerBuildButtonHandler>();
            jammingTowerUIHandler.TowerDescriptionSubject.Subscribe(isView =>
            {
                var createPos = new Vector3(
                        jammingTowerBuildBtn.transform.position.x + correctionDescriptionUIPosX,
                        jammingTowerBuildBtn.transform.position.y,
                        jammingTowerBuildBtn.transform.position.z);

                IsViewTowerDescription(isView, createPos, TowerType.Jamming);
            }).AddTo(this);
        }

        /// <summary>
        /// タワーの説明を表示するかどうか
        /// </summary>
        public void IsViewTowerDescription(bool isView, Vector3 createPos,TowerType towerType)
        {
            if (isView)
            {
                towerDescriptionUI = Instantiate(towerDescriptionUIObj, createPos, Quaternion.identity, uiCanvas).GetComponent<TowerBuildDescriptionUI>();
                var towerData = GameDataManager.instance.GetTowerData(towerType);
                var towerStatus = towerData.towerStatusDataInfoList[0];

                var towerDescriptionInfo = new TowerDescriptionInfo(towerData.name, towerStatus.attack, towerStatus.attackSpeed, towerStatus.towerCost, towerData.description);
                towerDescriptionUI.ViewTowerText(towerDescriptionInfo);
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