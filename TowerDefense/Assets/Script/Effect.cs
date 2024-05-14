using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
	#region SerializeField
	/// <summary>�G�t�F�N�g�̕\������</summary>
	[SerializeField] private float effectTime;
	#endregion

	#region UnityEvent
	void Start()
	{
		StartCoroutine(DestroyEffect());
	}
	#endregion

	#region PrivateMethod
	private IEnumerator DestroyEffect()
	{
		yield return new WaitForSeconds(effectTime);
		GameObject.Destroy(this.gameObject);
	}
	#endregion
}
