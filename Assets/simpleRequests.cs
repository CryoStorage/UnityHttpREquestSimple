using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class simpleRequests : MonoBehaviour
{
    [SerializeField] TMP_Text tmp_pName;
    [SerializeField] TMP_Text tmp_pIndex;
    [SerializeField] TMP_Text tmp_pType1;
    [SerializeField] TMP_Text tmp_pType2;
    [SerializeField] TMP_Text tmp_pHeight;
    [SerializeField] TMP_Text tmp_pWeight;
    [SerializeField] RawImage pSprite;
    Texture pTexture;
    [SerializeField] TMP_InputField searchInput;
    [SerializeField] List<Persona> personas;

    int randomId;

    private string toSearch;
    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetRequest("https://script.google.com/macros/s/AKfycbxQEfUWoUu0gNcGUyJVkmCPRaqxWz30doEldNUCX6FMgkRqNRmX-XUuBIu2WE2VN3MN/exec"));
        randomId = Random.Range(1,905);
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + randomId.ToString()));

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Return))
        {
            DoRequest();
        }
    }

    public void DoRequest()
    {
        
        if (searchInput.text == "")
        {
          RandomRequest();  
          return;
        }

        StopAllCoroutines();
        toSearch = searchInput.text;
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + toSearch));

    }

    public void RandomRequest()
    {
        //stopping coroutine before starting it again
        StopAllCoroutines();
        //A correct website page.
        // StartCoroutine(GetRequest("https://script.google.com/macros/s/AKfycbxQEfUWoUu0gNcGUyJVkmCPRaqxWz30doEldNUCX6FMgkRqNRmX-XUuBIu2WE2VN3MN/exec"));
        randomId = Random.Range(1,905);
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + randomId));


    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("Sending Request " + randomId);
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
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    JSONNode root = JSONNode.Parse(webRequest.downloadHandler.text);


                    tmp_pName.text = root["name"];
                    tmp_pHeight.text = root["height"];
                    tmp_pWeight.text = root["weight"];
                    tmp_pType1.text = root["types"][0]["type"]["name"];
                    tmp_pType1.text = root["types"][1]["type"]["name"];
                    tmp_pIndex.text = root["id"];
                    string imageURL = root["sprites"]["other"]["official-artwork"]["front_default"];

                    UnityWebRequest imgRequest = UnityWebRequestTexture.GetTexture(imageURL);
                    yield return imgRequest.SendWebRequest();
                    
                    if (imgRequest.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("Request error: " );
                    }else
                    {
                        pTexture = ((DownloadHandlerTexture)imgRequest.downloadHandler).texture;
                        pSprite.texture = pTexture;
                    }

                    // UnityWebRequest www = UnityWebRequestTexture.GetTexture(pokeImage);
                    // yield return www.SendWebRequest();

                    // if (www.result != UnityWebRequest.Result.Success)
                    // {
                    //     Debug.Log(www.error);
                    // }
                    // else
                    // {
                    //     Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    //     Debug.Log("Si se pudo");
                    //     Image1.texture = myTexture;
                    // }
 
                    break;
            }
        }
    }
}
