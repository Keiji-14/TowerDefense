using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// チュートリアルステージの処理
    /// </summary>
    public class TutorialController : MonoBehaviour
    {
        #region PublicField
        /// <summary>次の説明へ移行する処理</summary>
		public Subject<Unit> NextDescriptionSubject = new Subject<Unit>();
        /// <summary>説明が全て終了した時の処理</summary>
		public Subject<Unit> FinishDescriptionSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>ステップカウント</summary>
		private int stepCount;
        /// <summary>画像カウント</summary>
		private int spriteCount;
        /// <summary>次の説明へのボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickNextDescriptionButtonObserver => nextDescriptionBtn.OnClickAsObservable();
        #endregion

        #region SerializeField 
        [Multiline(3)]
        [SerializeField] private List<string> descriptionStrList = new List<string>();
        /// <summary>説明テキスト</summary>
        [SerializeField] private GameObject descriptionObj;
        /// <summary>クリックを制御するオブジェクト</summary>
        [SerializeField] private GameObject clickControlObj;
        /// <summary>クリックを制御するオブジェクト</summary>
        /// <summary>背景イメージ</summary>
        [SerializeField] private Image fadeBackImg;
        /// <summary>背景イメージの画像リスト</summary>
        [SerializeField] private List<Sprite> fadeBackSprige;
        /// <summary>次の説明へのボタン</summary>
        [SerializeField] private Button nextDescriptionBtn;
        /// <summary>チュートリアル</summary>
        [SerializeField] private Tutorial tutorial;
        /// <summary>カメラ動作のコンポーネント</summary>
        [SerializeField] private CameraMove cameraMove;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            stepCount = 0;
            spriteCount = 0;

            descriptionObj.SetActive(true);
            fadeBackImg.sprite = fadeBackSprige[spriteCount];
            
            tutorial.ViewDescriptionText(descriptionStrList[stepCount]);

            OnClickNextDescriptionButtonObserver.Subscribe(_ =>
            {
                NextDescriptionSubject.OnNext(Unit.Default);
            }).AddTo(this);

            // チュートリアルはじめはカメラを動作させない。
            cameraMove.enabled = false;
        }

        /// <summary>
        /// 次の説明に移行処理
        /// </summary>
        public void NextDescription()
        {
            if (stepCount >= 18)
                return;

            stepCount++;

            if (stepCount == 3)
            {
                NextBackSprite();
            }
            if (stepCount == 5)
            {
                NextBackSprite();
            }
            if (stepCount == 6)
            {
                NextBackSprite();
            }

            if (stepCount == 9)
            {
                NextBackSprite();
                nextDescriptionBtn.gameObject.SetActive(false);
                fadeBackImg.raycastTarget = false;
            }
            if (stepCount == 10)
            {
                NextBackSprite();
                nextDescriptionBtn.gameObject.SetActive(true);
                fadeBackImg.raycastTarget = true;
            }
            if (stepCount == 11)
            {
                NextBackSprite();
                nextDescriptionBtn.gameObject.SetActive(false);
                fadeBackImg.raycastTarget = false;

                clickControlObj.SetActive(true);
            }
            if (stepCount == 12)
            {
                NextBackSprite();
                nextDescriptionBtn.gameObject.SetActive(true);
                fadeBackImg.raycastTarget = true;

                clickControlObj.SetActive(false);
            }
            if (stepCount == 13)
            {
                NextBackSprite();
            }
            if (stepCount == 14)
            {
                cameraMove.enabled = true;
                NextBackSprite();
            }
            if (stepCount == 15)
            {
                NextBackSprite();
            }
            if (stepCount == 16)
            {
                NextBackSprite();
            }

            if (stepCount >= 18)
            {
                tutorial.gameObject.SetActive(false);
                FinishDescriptionSubject.OnNext(Unit.Default);
            }
            tutorial.ViewDescriptionText(descriptionStrList[stepCount]);
        }

        /// <summary>
        /// 背景を更新
        /// </summary>
        public void NextBackSprite()
        {
            spriteCount++;
            fadeBackImg.sprite = fadeBackSprige[spriteCount];
        }
        #endregion
    }


}