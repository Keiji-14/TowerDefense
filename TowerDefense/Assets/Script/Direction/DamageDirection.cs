using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Direction
{
    public class DamageDirection : MonoBehaviour
    {
        #region PrivateField
        /// <summary>RGBAの値</summary>
		private float red, green, blue, alfa;
        /// <summary>ダメージ表記の画面</summary>
        private Image damageImg;
        #endregion

        #region UnityEvent
        void Start()
        {
            damageImg = GetComponent<Image>();
            red = damageImg.color.r;
            green = damageImg.color.g;
            blue = damageImg.color.b;
            alfa = 0;
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ダメージ演出
        /// </summary>
        public IEnumerator Damage()
        {
            damageImg.enabled = true;

            alfa = 1.0f;
            damageImg.color = new Color(red, green, blue, alfa);

            while (alfa > 0)
            {
                // アルファ値を少しずつ減らす
                alfa -= 0.01f;
                alfa = Mathf.Clamp01(alfa);
                damageImg.color = new Color(red, green, blue, alfa);

                yield return new WaitForSeconds(0.01f);
            }

            damageImg.enabled = false;
        }
        #endregion
    }
}