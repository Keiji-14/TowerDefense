using Scene;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    /// <summary>
    /// BGMの再生処理
    /// </summary>
    public class BGM : MonoBehaviour
    {
        #region PublicField
        public static BGM instance = null;
        #endregion

        #region PrivateField
        private Dictionary<string, AudioClip> sceneBGMMap;
        #endregion

        #region SerializeField
        [SerializeField] private AudioSource bgm;
        [SerializeField] private List<SceneBGM> sceneBGMList;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSceneBGMMap();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            PlayBGMForCurrentScene();
        }

        private void OnEnable()
        {
            SceneLoader.Instance().OnSceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            if (SceneLoader.Instance() != null)
            {
                SceneLoader.Instance().OnSceneLoaded -= OnSceneLoaded;
            }
        }
        #endregion

        #region PrivateMethod
        private void InitializeSceneBGMMap()
        {
            sceneBGMMap = new Dictionary<string, AudioClip>();
            foreach (var sceneBGM in sceneBGMList)
            {
                if (!sceneBGMMap.ContainsKey(sceneBGM.sceneName))
                {
                    sceneBGMMap.Add(sceneBGM.sceneName, sceneBGM.bgmClip);
                }
            }
        }

        private void OnSceneLoaded(string sceneName)
        {
            PlayBGMForScene(sceneName);
        }

        private void PlayBGMForCurrentScene()
        {
            PlayBGMForScene(SceneManager.GetActiveScene().name);
        }

        private void PlayBGMForScene(string sceneName)
        {
            if (sceneBGMMap.TryGetValue(sceneName, out var bgmClip))
            {
                if (bgm.clip != bgmClip)
                {
                    bgm.clip = bgmClip;
                    bgm.Play();
                }
            }
        }
        #endregion

        [System.Serializable]
        public struct SceneBGM
        {
            public string sceneName;
            public AudioClip bgmClip;
        }
    }
}