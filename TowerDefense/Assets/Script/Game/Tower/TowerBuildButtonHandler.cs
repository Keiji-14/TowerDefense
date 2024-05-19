using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

namespace Game.Tower
{
    /// <summary>
    /// タワーについての説明UIの表示・非表示を行う処理
    /// </summary>
    public class TowerBuildButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region PublicField
        /// <summary>タワー説明を表示する処理</summary>
        public Subject<bool> TowerDescriptionSubject = new Subject<bool>();
        #endregion

        #region PublicMethod
        // マウスオーバー時に呼び出したいメソッド
        public void OnPointerEnter(PointerEventData eventData)
        {
            TowerDescriptionSubject.OnNext(true);
        }

        // マウスがUI要素から出た時に呼び出すメソッド
        public void OnPointerExit(PointerEventData eventData)
        {
            TowerDescriptionSubject.OnNext(false);
        }
        #endregion
    }
}