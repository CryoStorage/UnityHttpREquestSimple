using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;

public class simpleRequests : MonoBehaviour
{
    [SerializeField] TMP_Text texto;
    [SerializeField] TMP_InputField searchInput;
    [SerializeField] List<Persona> personas;

    private string toSearch;
    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetRequest("https://script.google.com/macros/s/AKfycbxQEfUWoUu0gNcGUyJVkmCPRaqxWz30doEldNUCX6FMgkRqNRmX-XUuBIu2WE2VN3MN/exec"));
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + toSearch));

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    public void DoRequest()
    {
        //stopping coroutine before starting it again
        StopAllCoroutines();
        //A correct website page.
        // StartCoroutine(GetRequest("https://script.google.com/macros/s/AKfycbxQEfUWoUu0gNcGUyJVkmCPRaqxWz30doEldNUCX6FMgkRqNRmX-XUuBIu2WE2VN3MN/exec"));
        toSearch = searchInput.text;
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + toSearch));

    }
   
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("Sending Request");
            string[] pages = uri.Split('/');
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
                    texto.text = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    JSONNode root = JSONNode.Parse(webRequest.downloadHandler.text);

                    foreach (var key in root.Keys)
                    {
                        int num = 0;
                        Debug.Log(num);
                        num++;
                    }
                    break;
            }
        }
    }
}
