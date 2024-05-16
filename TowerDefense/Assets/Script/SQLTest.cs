using GameData;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SQLTest : MonoBehaviour
{
    IEnumerator Start()
    {
        // PHPスクリプトのURL
        string url = "http://localhost:8080/towerdefense/Towers/settowers.json";

        // HTTPリクエストを送信してPHPスクリプトからデータを取得
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        /*if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("HTTP Error: " + www.error);
        }
        else
        {
            // レスポンスからデータを取得して表示
            string jsonString = www.downloadHandler.text;
            var inputData = JsonUtility.FromJson<JsonData>(jsonString);

            foreach (var tower in inputData.tower_list)
            {
                Debug.Log("tower: " + tower.towerIncome);
            }
        }*/
    }
}
