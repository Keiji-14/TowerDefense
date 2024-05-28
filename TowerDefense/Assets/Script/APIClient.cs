using GameData;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

namespace NetWark
{
    public class APIClient : MonoBehaviour
    {
        #region PrivateField
        private static APIClient instance = null;

        private string baseUrl = "http://www.keiji14.shop/api/";
        #endregion

        #region PublicMethod
        /// <summary>
        /// インスタンス化
        /// </summary>
        /// <returns></returns>
        public static APIClient Instance()
        {
            // オブジェクトを生成し、自身をAddCompleteして、DontDestroyに置く
            if (instance == null)
            {
                var obj = new GameObject("APIClient");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<APIClient>();
            }

            return instance;
        }

        /// <summary>
        /// ユーザー登録を行う処理
        /// </summary>
        public IEnumerator RegisterUserAndGetID(string name)
        {
            WWWForm form = new WWWForm();
            form.AddField("name", name);

            UnityWebRequest request = UnityWebRequest.Post(baseUrl + "register_user.php", form);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User registered: " + request.downloadHandler.text);

                string jsonResponse = request.downloadHandler.text;
                UserIDResponse response = JsonUtility.FromJson<UserIDResponse>(jsonResponse);

                if (response.error == null)
                {
                    int userID = response.id;
                    PlayerPrefs.SetInt("UserID", userID);
                }
                else
                {
                    Debug.LogError("Error: " + response.error);
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }

        /// <summary>
        /// 保存されたUserIDを使用してユーザー情報を取得する処理
        /// </summary>
        public IEnumerator GetUserData()
        {
            int userID = PlayerPrefs.GetInt("UserID", -1);

            if (userID == -1)
            {
                Debug.LogError("No UserID found in PlayerPrefs");
                yield break;
            }

            UnityWebRequest request = UnityWebRequest.Get(baseUrl + "get_user_info.php?id=" + userID);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User data: " + request.downloadHandler.text);

                UserDataInfo userDataInfo = JsonUtility.FromJson<UserDataInfo>(request.downloadHandler.text);

                if (userDataInfo == null)
                {
                    Debug.LogError("Failed to parse user data");
                }
                else
                {
                    GameDataManager.instance.SetUserDataInfo(userDataInfo);
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }

        /// <summary>
        /// 保存されたUserIDを使用してユーザー情報を取得する処理
        /// </summary>
        public IEnumerator GetRankingData(System.Action<List<UserDataInfo>> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(baseUrl + "get_ranking.php");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                RankingResponse rankingResponse = JsonUtility.FromJson<RankingResponse>("{\"ranking\":" + jsonResponse + "}");

                if (rankingResponse != null)
                {
                    callback(rankingResponse.ranking);
                }
                else
                {
                    Debug.LogError("Failed to parse ranking data");
                    callback(new List<UserDataInfo>());
                }
            }
            else
            {
                Debug.LogError("Error getting ranking data: " + request.error);
                callback(new List<UserDataInfo>());
            }
        }

        /// <summary>
        /// ユーザーのハイスコアを更新する処理
        /// </summary>
        public IEnumerator UpdateUserHighScore(int userID, int newHighScore)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", userID);
            form.AddField("highscore", newHighScore);

            UnityWebRequest request = UnityWebRequest.Post(baseUrl + "update_highscore.php", form);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("High score updated: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error updating high score: " + request.error);
            }
        }
        #endregion
    }

    [System.Serializable]
    public class UserIDResponse
    {
        public int id;
        public string error; // エラーメッセージを格納するフィールド
    }

    [System.Serializable]
    public class RankingResponse
    {
        public List<UserDataInfo> ranking;
    }
}