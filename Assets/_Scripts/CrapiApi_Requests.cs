using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

[Serializable] public class ToDoTask
{
    public int id;
    public string name;
    public bool isComplete;
}
public class CrapiApi_Requests : MonoBehaviour
{
    [SerializeField] private int taskId;

    [SerializeField] private string taskName;

    [SerializeField] private bool taskComplete;
    private void Start()
    {
        ToDoTask myTask = new ToDoTask();
        myTask.id = taskId;
        myTask.name= taskName;
        myTask.isComplete = taskComplete;
        
        DoPost(myTask);
    }

    public void DoGet()
    {
        StopAllCoroutines();
        StartCoroutine(CorGetRequest("https://localhost:7114/api/Player"));
    }
    public void DoPost(ToDoTask aTask)
    {
        StopAllCoroutines();
        StartCoroutine(CorPostRequest("https://localhost:7114/api/Player", aTask));
        
    }  
    public void DoDelete(int aId)
    {
        StopAllCoroutines();
        StartCoroutine(CorDeleteRequest("https://localhost:7114/api/Player", aId));
        
    }
    
    private IEnumerator CorGetRequest(string aUri)
    {
        using (UnityWebRequest getRequest = UnityWebRequest.Get(aUri))
        {
            //make request and wait    
            yield return getRequest.SendWebRequest();
            Debug.Log("Sending Request to " + aUri);
            string[] pages = aUri.Split('/');
            int page = pages.Length - 1;
            switch (getRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + getRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + getRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    JSONNode root = JSONNode.Parse(getRequest.downloadHandler.text);
                    Debug.Log(root.ToString());
                    break;
            }
            getRequest.Dispose();
        }
    }

    private IEnumerator CorPostRequest(string aUri, ToDoTask aTask)
    {
        
        string json = JsonUtility.ToJson(aTask);
        Debug.Log(json);
        
        UnityWebRequest postRequest = UnityWebRequest.Put(aUri, json);
        postRequest.method = "POST";
        postRequest.SetRequestHeader("Content-Type", "Application/json");
        yield return postRequest.SendWebRequest();
        
        switch (postRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + postRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + postRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Post Success");
                break;
        }
        postRequest.Dispose();
    }

    private IEnumerator CorDeleteRequest(string aUri, int aId)
    {
        UnityWebRequest putRequest = UnityWebRequest.Delete(aUri + "/" + aId.ToString());
        yield return putRequest.SendWebRequest();
        switch (putRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + putRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + putRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Delete Success");
                break;
        }
        putRequest.Dispose();
    }

    private IEnumerator CorPutRequest(string aUri, int aId)
    {
        UnityWebRequest putRequest = UnityWebRequest.Put(aUri + "/" , aId.ToString());
        yield return putRequest.SendWebRequest();
        switch (putRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + putRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + putRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Delete Success");
                break;
        }
        putRequest.Dispose();
        
    }
}

