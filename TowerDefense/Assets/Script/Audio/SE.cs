using System.Collections.Generic;
using UnityEngine;

namespace Audio
{    /// <summary>
     /// 効果音の再生処理
     /// </summary>
    public class SE : MonoBehaviour
    {
        #region PublicField
        public static SE instance = null;
        #endregion

        #region SerializeField
        [SerializeField] private AudioSource audioSource;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// SEを再生
        /// </summary>
        /// <param name="audioClip">効果音</param>
        public void Play(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
        #endregion
    }
}