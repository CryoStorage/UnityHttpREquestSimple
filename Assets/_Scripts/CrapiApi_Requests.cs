using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Serialization;
using SimpleJSON;
public class CrapiApi_Requests : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
            DoRequest();
    }
    void DoRequest()
    {
        StopAllCoroutines();
        StartCoroutine(CorPostRequest("https://localhost:7114/api/todoitems"));
        
        // StartCoroutine(CorGetRequest("https://localhost:7114/api/todoitems"));
    }
    
    IEnumerator CorGetRequest(string Uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Uri))
        {
            //make request and wait    
            yield return webRequest.SendWebRequest();
            Debug.Log("Sending Request to " + Uri);
            string[] pages = Uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    JSONNode root = JSONNode.Parse(webRequest.downloadHandler.text);
                    Debug.Log(root.ToString());
                    break;
            }
        }
    }

    IEnumerator CorPostRequest(string Uri)
    {
        WWWForm form = new WWWForm();
        //form.headers["Content-Type"]= "application/json";
        var info = "{Name:lalo,IsComplete:true}";
        var json = System.Text.Encoding.UTF8.GetBytes(info);
        // foreach (var item in form.headers)
        // {
        //     Debug.Log(item);
        // }
        // form.headers"Content-Type", "application/json");
        // form.AddField("Id", 0);
        form.AddField("Name", "toDoItem0");
        form.AddField("Name", "false");
        
        using (UnityWebRequest webPost = UnityWebRequest.Post(Uri,form))
        {
            webPost.SetRequestHeader("Content-Type","application/json");
            yield return webPost.SendWebRequest();
            switch (webPost.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webPost.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webPost.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Post Success");
                    break;
            }
        }
        
    }
}
