using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    /// <summary>
    /// ゲームの速度を変更する処理
    /// </summary>
    public class GameSpeed : MonoBehaviour
    {
        #region PrivateField
        /// <summary>レベルの初期値</summary>
        private const int initLevel = 0;
        /// <summary>現在のスピードレベル</summary>
        private int speedLevel = 0;
        /// <summary>速度切り替えボタンを押した時の処理</summary>
        private IObservable<Unit> OnClickChangeSpeedButtonObserver => changeSpeedBtn.OnClickAsObservable();
        #endregion

        #region SerializeField
        /// <summary>各スピードレベルの速度値</summary>
        [SerializeField] private List<float> timeScales = new List<float>();
        /// <summary>速度切り替えボタン</summary>
        [SerializeField] private Button changeSpeedBtn;
        /// <summary>速度テキスト</summary>
        [SerializeField] private TextMeshProUGUI speedText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            OnClickChangeSpeedButtonObserver.Subscribe(_ =>
            {
                ChangeGameSpeed();
            }).AddTo(this);

            InitGameSpeed();
        }

        /// <summary>
        /// ゲーム速度の初期化
        /// </summary>
        public void InitGameSpeed()
        {
            // 初期の時間スケールを設定
            Time.timeScale = timeScales[initLevel];
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// ゲームの速度を切り替える処理
        /// </summary>
        private void ChangeGameSpeed()
        {
            speedLevel = (speedLevel + 1) % timeScales.Count;
            // 新しい時間スケールを設定
            Time.timeScale = timeScales[speedLevel];

            speedText.text = $"x{timeScales[speedLevel]}";
        }
        #endregion
    }
}