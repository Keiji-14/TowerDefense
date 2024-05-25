using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using GameData;

namespace NetWark
{
    public class APIClient : MonoBehaviour
    {
        #region PrivateField
        private string baseUrl = "http://www.keiji14.shop/api/";
        #endregion

        #region PublicMethod
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

                UserDataInfo userInfo = JsonUtility.FromJson<UserDataInfo>(request.downloadHandler.text);
                if (userInfo == null)
                {
                    Debug.LogError("Failed to parse user data");
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
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
}