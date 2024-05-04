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
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
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
        }
        #endregion
    }
}