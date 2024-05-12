using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
	/// <summary>
	/// �t�F�[�h�C���E�t�F�[�h�A�E�g�̏���
	/// </summary>
	public class FadeController : MonoBehaviour
	{
		#region PublicField
		/// <summary>�t�F�[�h�C�����s�����ǂ���</summary>
		public bool fadeIn = false;
		/// <summary>�t�F�[�h�A�E�g���s�����ǂ���</summary>
		public bool fadeOut = false;
		#endregion

		#region PrivateField
		/// <summary>RGBA�̒l</summary>
		private float red, green, blue, alfa;
		/// <summary>�t�F�[�h����摜</summary>
		private Image fadeImage;
		#endregion

		#region PublicField
		/// <summary>�t�F�[�h�C���̑��x</summary>
		[SerializeField] private float fadeInSpeed;
		/// <summary>�t�F�[�h�A�E�g�̑��x</summary>
		[SerializeField] private float fadeOutSpeed;
		#endregion

		#region UnityEvent
		void Start()
		{
			fadeImage = GetComponent<Image>();
			red = fadeImage.color.r;
			green = fadeImage.color.g;
			blue = fadeImage.color.b;
			alfa = fadeImage.color.a;

			fadeIn = true;
		}

		void Update()
		{
			if (fadeIn)
			{
				StartFadeIn();
			}

			if (fadeOut)
			{
				StartFadeOut();
			}
		}
		#endregion

		#region PrivateMethod
		// �t�F�[�h�C�����s������
		public void StartFadeIn()
		{
			fadeIn = true;
			alfa -= fadeInSpeed;
			SetAlpha();
			if (alfa <= 0)
			{
				fadeIn = false;
				fadeImage.enabled = false;
			}
		}

		// �t�F�[�h�A�E�g���s������
		public void StartFadeOut()
		{
			fadeOut = true;
			fadeImage.enabled = true;
			alfa += fadeOutSpeed;
			SetAlpha();
			if (alfa >= 1)
			{
				fadeOut = false;
			}
		}

		void SetAlpha()
		{
			fadeImage.color = new Color(red, green, blue, alfa);
		}
		#endregion
	}
}