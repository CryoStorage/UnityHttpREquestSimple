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
    Color[] colors;
    [SerializeField] RawImage typeBg1;
    [SerializeField] RawImage typeBg2;
    [SerializeField] TMP_Text tmp_pHeight;
    [SerializeField] TMP_Text tmp_pWeight;
    [SerializeField] RawImage pSprite;
    Texture pTexture;
    [SerializeField] TMP_InputField searchInput;

    int randomId;

    private string toSearch;
    void Start()
    {
        Application.targetFrameRate = 60;
        // A correct website page.
        // StartCoroutine(GetRequest("https://script.google.com/macros/s/AKfycbxQEfUWoUu0gNcGUyJVkmCPRaqxWz30doEldNUCX6FMgkRqNRmX-XUuBIu2WE2VN3MN/exec"));
        //randomId = Random.Range(1,905);
        
        Color normal = new Color(168, 168, 120);
        Color fire = new Color(240, 128, 48);
        Color fighting = new Color(192, 48, 40);
        Color water = new Color(104, 144, 240);
        Color flying = new Color(168, 144, 240);
        Color grass = new Color(120, 200, 80);
        Color poison = new Color(160, 64, 160);
        Color electric = new Color(248, 208, 48);
        Color ground = new Color(224, 192, 104);
        Color psychic = new Color(248, 88, 136);
        Color rock = new Color(184, 160, 56);
        Color ice = new Color(152, 216, 216);
        Color bug = new Color(168, 184, 32);
        Color dragon = new Color(112, 56, 248);
        Color dark = new Color(112, 88, 72);
        Color ghost = new Color(112, 88, 152);
        Color steel = new Color(184, 184, 208);
        Color fairy = new Color(238, 153, 172);
        colors = new Color[]{normal,fire,fighting,water,flying,grass,poison,electric,ground,psychic,rock,ice,bug,dragon,ghost,dark,steel,fairy};

        randomId = Random.Range(1,895);
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
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/" + toSearch.ToLower()));

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
                    string stringType1 = root["types"][0]["type"]["name"];
                    tmp_pType1.text = stringType1;
                    Debug.Log (stringType1);
                    string stringType2 = root["types"][1]["type"]["name"];
                    tmp_pType2.text = stringType2;
                    
                    tmp_pIndex.text = root["id"];
                    string imageURL = root["sprites"]["other"]["official-artwork"]["front_default"];

                    RawImage[] bgRawImages = new RawImage[]{typeBg1,typeBg2};
                    string[] typeStrings = new string[]{stringType1,stringType2};

                    for (int i = 0; i < bgRawImages.Length ; i++)
                    {
                        switch(typeStrings[i])
                        {
                            case "normal":
                            bgRawImages[i].color = colors[0] ;
                            Debug.Log("typeisnormal");
                            
                            break;
                            case "fire":
                            bgRawImages[i].color = colors[1];

                            break;
                            case "fighting":
                            bgRawImages[i].color = colors[2];

                            break;
                            case "water":
                            bgRawImages[i].color = colors[3];

                            break;
                            case "flying":
                            bgRawImages[i].color = colors[4];

                            break;
                            case "grass":
                            bgRawImages[i].color = colors[5];

                            break;
                            case "poison":
                            bgRawImages[i].color = colors[6];

                            break;
                            case "electric":
                            bgRawImages[i].color = colors[7];

                            break;
                            case "ground":
                            bgRawImages[i].color = colors[8];

                            break;
                            case "psychic":
                            bgRawImages[i].color = colors[9];

                            break;
                            case "rock":
                            bgRawImages[i].color = colors[10];

                            break;
                            case "ice":
                            bgRawImages[i].color = colors[11];

                            break;
                            case "bug":
                            bgRawImages[i].color = colors[12];

                            break;
                            case "dragon":
                            bgRawImages[i].color = colors[13];
                            
                            break;
                            case "ghost":
                            bgRawImages[i].color = colors[14];

                            break;
                            case "dark":
                            bgRawImages[i].color = colors[15];

                            break;
                            case "steel":
                            bgRawImages[i].color = colors[16];

                            break;
                            case "fairy":
                            bgRawImages[i].color = colors[17];
                            break;

                            default:
                            Color transparent = new Color(1,1,1,0);
                            bgRawImages[i].color = transparent;

                            break;   
                        }
                    }
                    
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
                    break;
            }
        }
    }
}
