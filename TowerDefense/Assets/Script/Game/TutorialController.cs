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
        [SerializeField] private GameObject clickControlObjA;
        /// <summary>クリックを制御するオブジェクト</summary>
        [SerializeField] private GameObject clickControlObjB;
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
            tutorial.gameObject.SetActive(true);
            clickControlObjA.SetActive(true);
            clickControlObjB.SetActive(true);

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
            if (stepCount >= 23)
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
                SwicthDescription(false);

                clickControlObjA.SetActive(false);
            }
            if (stepCount == 10)
            {
                NextBackSprite();
                SwicthDescription(true);
            }
            if (stepCount == 11)
            {
                NextBackSprite();
                SwicthDescription(false);

                clickControlObjA.SetActive(true);
            }
            // タワーの建設が完了
            if (stepCount == 12)
            {
                NextBackSprite();
                SwicthDescription(true);
            }
            // タワー再度選択
            if (stepCount == 13)
            {
                SwicthDescription(false);

                clickControlObjA.SetActive(false);
            }

            if (stepCount == 14)
            {
                SwicthDescription(true);
                NextBackSprite();
            }
            if (stepCount == 17)
            {
                SwicthDescription(false);
                NextBackSprite();
            }
            if (stepCount == 18)
            {
                SwicthDescription(true);
                NextBackSprite();
            }
            // カメラの確認
            if (stepCount == 19)
            {
                cameraMove.enabled = true;
                NextBackSprite();
            }
            // カメラの確認完了
            if (stepCount == 20)
            {
                NextBackSprite();
            }
            // ゲーム開始ボタンの説明
            if (stepCount == 21)
            {
                NextBackSprite();
            }

            if (stepCount >= 23)
            {
                tutorial.gameObject.SetActive(false);
                FinishDescriptionSubject.OnNext(Unit.Default);
            }
            tutorial.ViewDescriptionText(descriptionStrList[stepCount]);
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 背景を更新
        /// </summary>
        private void NextBackSprite()
        {
            spriteCount++;
            fadeBackImg.sprite = fadeBackSprige[spriteCount];
        }

        /// <summary>
        /// 説明方法を切り替える処理
        /// </summary>
        private void SwicthDescription(bool isActive)
        {
            nextDescriptionBtn.gameObject.SetActive(isActive);
            fadeBackImg.raycastTarget = isActive;
        }
        #endregion
    }
}