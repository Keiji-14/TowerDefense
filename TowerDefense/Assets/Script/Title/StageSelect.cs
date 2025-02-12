﻿using Audio;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// ステージ選択画面の処理
    /// </summary>
    public class StageSelect : MonoBehaviour
    {
        #region PublicField
        /// <summary>通常ステージの選択を決定時の処理</summary>
        public Subject<int> StageDecisionSubject = new Subject<int>();
        /// <summary>チュートリアルステージ決定時の処理</summary>
        public Subject<Unit> TutorialStageSubject = new Subject<Unit>();
        /// <summary>EXステージ決定時の処理</summary>
        public Subject<Unit> EXStageSubject = new Subject<Unit>();
        /// <summary>ステージ選択決定時の処理</summary>
        public Subject<Unit> MainTitleBackSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>EXステージのボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickEXStageButtonObserver => exStageBtn.OnClickAsObservable();
        /// <summary>メインタイトルに戻るボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickMainTitleBackButtonObserver => mainTitleBackBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>通常ステージのボタンリスト</summary>
        [SerializeField] private List<Button> stageNumBtnList;
        /// <summary>EXステージのボタン</summary>
        [SerializeField] private Button exStageBtn;
        /// <summary>メインタイトルに戻るボタン</summary>
        [SerializeField] private Button mainTitleBackBtn;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // ステージ番号を初期化
            int stageNum = 1;

            // 通常ステージのボタンに処理を追加
            foreach (var button in stageNumBtnList)
            {
                // ステージ番号を設定
                int capturedStageNum = stageNum;

                // 通常ステージのボタンを押した時の処理
                button.OnClickAsObservable().Subscribe(_ =>
                {
                    SE.instance.Play(SE.SEName.ButtonSE);
                    StageDecisionSubject.OnNext(capturedStageNum);
                }).AddTo(this);

                stageNum++;
            }

            // EXステージのボタンを押した時の処理
            OnClickEXStageButtonObserver.Subscribe(_ =>
            {
                SE.instance.Play(SE.SEName.ButtonSE);
                EXStageSubject.OnNext(Unit.Default);
            }).AddTo(this);

            // メインタイトルに戻るボタンを押した時の処理
            OnClickMainTitleBackButtonObserver.Subscribe(_ =>
            {
                SE.instance.Play(SE.SEName.ButtonSE);
                MainTitleBackSubject.OnNext(Unit.Default);
            }).AddTo(this);
        }
        #endregion
    }
}