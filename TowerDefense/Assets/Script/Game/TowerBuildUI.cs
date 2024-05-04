using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tower
{
    public class TowerBuildUI : MonoBehaviour
    {
        #region PublicField
        /// <summary></summary>
        public Subject<Unit> TowerBuildSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>タワー建設ボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickTowerBuildButtonObserver => towerBuildBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>タワー建設ボタン</summary>
        [SerializeField] private Button towerBuildBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            OnClickTowerBuildButtonObserver.Subscribe(_ =>
            {
                TowerBuildSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }
        #endregion
    }
}