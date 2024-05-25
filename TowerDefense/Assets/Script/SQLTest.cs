using GameData;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SQLTest : MonoBehaviour
{
    #region PrivateField
    private string baseUrl = "http://www.keiji14.shop/api/";
    #endregion

    IEnumerator Start()
    {
        // HTTPリクエストを送信してPHPスクリプトからデータを取得
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "get_data.php");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            string wrappedJson = "{\"userDataInfoList\":" + request.downloadHandler.text + "}";

            UserDataBase userDataBase = JsonUtility.FromJson<UserDataBase>(wrappedJson);

            Debug.Log("Response: " + userDataBase.userDataInfoList[0].name);

            if (userDataBase != null && userDataBase.userDataInfoList != null)
            {
                Debug.Log("List Count: " + userDataBase.userDataInfoList.Count);

                foreach (var data in userDataBase.userDataInfoList)
                {
                    Debug.Log("ID: " + data.id + ", Name: " + data.name + ", HighScore: " + data.highscore);
                }
            }
            else
            {
                Debug.LogError("Deserialization failed or list is null.");
            }
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
