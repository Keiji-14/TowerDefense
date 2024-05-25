using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    public class Tutorial : MonoBehaviour
    {
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
            descriptionText.text = descriptionStr;
        }
        #endregion
    }
}