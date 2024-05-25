using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Direction
{
    public class DamageDirection : MonoBehaviour
    {
        #region PrivateField
        /// <summary>RGBA�̒l</summary>
		private float red, green, blue, alfa;
        /// <summary>�_���[�W�\�L�̉��</summary>
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
        public IEnumerator Damage()
        {
            damageImg.enabled = true;

            alfa = 1.0f;
            damageImg.color = new Color(red, green, blue, alfa);

            while (alfa > 0)
            {
                // �A���t�@�l�����������炷
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