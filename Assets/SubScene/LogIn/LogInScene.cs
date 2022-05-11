using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;//UnityWebRequestを使うために必要
public class LogInScene : MonoBehaviour
{
    public GameObject sealed_load,Not,_new,_con;
    public Text conUser;
    public bool isWaiting;
    public InputField input_;
    public float cnt;
    public int i,result;
    public WebClient client;
    string user;


    void Start()
    {  
        
        sealed_load.active = false;
        Not.active = false;
        RuntimeTask.isLock = false;
        input_.Select();
    }

    string e;
    IEnumerator GetText(string g)
    {
        /*取得したいサイトURLを指定*/
        UnityWebRequest www = UnityWebRequest.Get(g);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            result = -1;
        }
        else
        {
            // 結果をテキストとして表示します
            e = (www.downloadHandler.text);
            Debug.Log("get :::" + e);
            result = 0;
        }
       
    }
    void Update()
        {
        if (isWaiting)
        {
            cnt += Time.deltaTime;
            if (cnt > 1)
            {
                cnt = 0;
                switch (i)
                {
                    case 0:
                        sealed_load.active = true;
                        client = new WebClient();
                        client.UseDefaultCredentials = true; 
                        client.Proxy.Credentials = CredentialCache.DefaultCredentials;
                        i++; 
                        
                        break;
                    case 1:
                        try
                        {
                            StartCoroutine(GetText("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/scl/" + input_.text + ".txt"));
                          //  UnityWebRequest www = UnityWebRequest.Get("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/scl/" + input_.text + ".txt");
                           // while (!www.isDone) ;
                           // var w = www.SendWebRequest();
                           if(result != -1)
                                UserData.userNum = int.Parse(e);

                            // if (www.isNetworkError || www.isHttpError)
                            //{
                            //    Debug.Log(www.error);
                            //    UserData.code = input_.text;
                            //    result = -1;
                            // }
                            // else
                            //  {
                            //    Debug.Log(www.downloadHandler.text);
                            //      UserData.userNum = int.Parse(www.downloadHandler.text);
                            //     //   result = "0" == client.DownloadString("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/server/userData/" + user + "/init.txt") ? 0 : 1;
                            //     Debug.Log(result);
                            result = 0; 
                           // }
                             
                            
                        } 
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                            result = -1;
                        }

                        i++;
                        break;

                    case 2:
                        i = result >= 0 ? 20 : 3; 
                        break;

                    case 3:  case 4: case 5: case 6: i++; break; //DELAY


                    default:
                        input_.text = "";
                        input_.interactable = true;
                        sealed_load.active = false; 
                        Not.active = true; 
                        isWaiting = false;
                        input_.Select();
                        RuntimeTask.isLock = false;
                        i = 0;
                        break;

                    case 20:
                        if (result == 1) conUser.text = "ようこそ、" + user + "様";
                        sealed_load.active = false;
                        Not.active = false;
                        bool _NEW = result == 0;
                        if (result == 0) _new.active = true;
                        else _con.active = true;
                        bool x = result == 0;
                        SceneLoader.Load(x ? "Avator" : "Load");
                        Destroy(this);
                        break; 

                }
            }
        }
    }

     

    public void InputChange()
    {
        input_.text = input_.text.ToUpper().Replace(" ","");

        if (input_.text.Length == 8)
        {
            input_.interactable = false;
            Debug.Log("Max_text");
            isWaiting = true; 
            RuntimeTask.isLock = true;
        }
    }



    public void NonSc()
    {
        Application.OpenURL("https://pictureanimationst9.wixsite.com/mysite/reqcode-c201");
        //Destroy(this);
    }



}
