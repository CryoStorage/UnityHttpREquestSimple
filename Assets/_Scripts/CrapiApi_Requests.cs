using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

[Serializable] public class Player
{
    public long Id;
    public int Jumps;
    public int Wins;
    public int Deaths;
    public int OrbsCollected;
    public int OrbsSpent;
    public int AdsWatched;
    public long PlayTime;
    public long AvgPlaySession;
    public long DistanceClimbed;
    public long DistanceFallen;

}
public class CrapiApi_Requests : MonoBehaviour
{
    [Header("host URI")] [SerializeField] private string uri = "https://localhost:7114/api/Player";
    [Header("Player")]
    [SerializeField] private long id;
    [SerializeField] private int jumps;
    [SerializeField] private int wins;
    [SerializeField] private int deaths;
    [SerializeField] private int orbsCollected;
    [SerializeField] private int orbsSpent;
    [SerializeField] private int adsWatched;
    [SerializeField] private long playTime;
    [SerializeField] private long avgPlaySession;
    [SerializeField] private long distanceClimbed;
    [SerializeField] private long distanceFallen;
    
    private Player _player = new Player();
    
    private void BuildPlayer()
    {
        _player = new Player();
        _player.Id = id;
        _player.Jumps = jumps;
        _player.Wins = wins;
        _player.Deaths = deaths;
        _player.OrbsCollected = orbsCollected;
        _player.OrbsSpent = orbsSpent;
        _player.AdsWatched = adsWatched;
        _player.PlayTime = playTime;
        _player.AvgPlaySession = avgPlaySession;
        _player.DistanceClimbed = distanceClimbed;
        _player.DistanceFallen = distanceFallen;
    }

    private void BuildJson(Player aPlayer)
    {
        
        
    }

    public void DoGet()
    {
        StopAllCoroutines();
        StartCoroutine(CorGetRequest(uri));
    }
    public void DoPost(Player aTask)
    {
        StopAllCoroutines();
        StartCoroutine(CorPostRequest(uri, aTask));
    }  
    public void DoDelete(int aId)
    {
        StopAllCoroutines();
        StartCoroutine(CorDeleteRequest(uri, aId));
    }
    public void DoPut()
    {
        StopAllCoroutines();
        // StartCoroutine(CorPutRequest(uri,));

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

    private IEnumerator CorPostRequest(string aUri, Player aTask)
    {
        string json = JsonUtility.ToJson(aTask);
        Debug.Log(json);
        
        UnityWebRequest postRequest = UnityWebRequest.Put(aUri, json);
        postRequest.method = "POST";
        postRequest.SetRequestHeader("Content-Type", "Application/json");
        //make request and wait    
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
        //make request and wait    
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
        //make request and wait    
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

