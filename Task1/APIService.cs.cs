using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIService : MonoBehaviour
{
    private const string apiUrl = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public delegate void OnAPIDataReceived(string json);
    public static event OnAPIDataReceived APIDataReceived;

    public void FetchAPIData()
    {
        StartCoroutine(FetchDataCoroutine());
    }

    private IEnumerator FetchDataCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error fetching API data: " + webRequest.error);
            }
            else
            {
                string jsonData = webRequest.downloadHandler.text;
                APIDataReceived?.Invoke(jsonData);
            }
        }
    }
}
