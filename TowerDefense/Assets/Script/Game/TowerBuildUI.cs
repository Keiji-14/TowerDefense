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
        /// <summary>�^���[���݃{�^�������������̏���</summary>
        private IObservable<Unit> OnClickTowerBuildButtonObserver => towerBuildBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>�^���[���݃{�^��</summary>
        [SerializeField] private Button towerBuildBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// ������
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