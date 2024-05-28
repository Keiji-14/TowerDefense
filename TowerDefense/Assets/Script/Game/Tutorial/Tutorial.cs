using System.Collections;
using UnityEngine;
using TMPro;

namespace Game
{
    public class Tutorial : MonoBehaviour
    {
        #region PrivateField
        /// <summary>説明テキストを表示するコルーチン</summary>
        private Coroutine displayCoroutine;
        #endregion

        #region SerializeField 
        /// <summary>説明テキスト</summary>
        [SerializeField] private TextMeshProUGUI descriptionText;
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void ViewDescriptionText(string descriptionStr)
        {
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }
            displayCoroutine = StartCoroutine(DisplayTextCoroutine(descriptionStr));
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 説明テキストを表示する非同期処理
        /// </summary>
        private IEnumerator DisplayTextCoroutine(string text)
        {
            descriptionText.text = "";
            foreach (char letter in text.ToCharArray())
            {
                descriptionText.text += letter;
                // 一文字ごとの表示間隔
                yield return new WaitForSeconds(0.02f);
            }
        }
        #endregion
    }
}