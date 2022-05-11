using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class SetUp_System : MonoBehaviour
{
    public bool isCheck;
    public int cnt,dotCnt,pos;
    public float iw;
    [Range(0,0.3f)]
    public float dotWaitCnt;
    public InputField input_;
    public Text samples;
    public Button button;

    public GameObject checkRoot;
    public Text checkText_main, checkText_sub;
    public Slider checkParam;
    public WebClient client;

    // Start is called before the first frame update
    void Start()
    {
        client = new WebClient();
        input_ = GameObject.Find("Canvas_" + (OS_.isPC() ? "PC" : "SP")+ "/InputField").GetComponent<InputField>();
        button = GameObject.Find("Canvas_" + (OS_.isPC() ? "PC" : "SP") + "/Button").GetComponent<Button>();
        Destroy(GameObject.Find("Canvas_" + (!OS_.isPC() ? "PC" : "SP")));
        samples = input_.transform.Find("Placeholder").GetComponent<Text>();
        checkText_main = checkRoot.transform.Find("Text").GetComponent<Text>();
        checkText_sub = checkRoot.transform.Find("sub").GetComponent<Text>();
        checkParam = checkRoot.transform.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheck) return;
        dotWaitCnt += Time.deltaTime;
        if (dotWaitCnt > 0.3f)
        {
            dotCnt++;
            if (dotCnt == 4)
                dotCnt = 0;
        }


        iw += Time.deltaTime;

        switch (cnt)
        {
            case 0://文字数min
                if (iw > 1f)
                { 
                    if (input_.text.Length < 3)
                        REF("3文字以下の名前は利用できません");
                    else
                        SetParam("名前の文字数を確認中", 0.1f);
                } 
                break;


            case 1://文字数Max
                if (iw > 1f)
                {
                    if (input_.text.Length >10) 
                        REF("名前の文字数の上限を超えています。"); 
                    else 
                        SetParam("禁止ワードが含まれていないか検出中", 0.2f); 
                }
                break;


            case 2://禁止ワード
                if (iw > 1f)
                {
                    SetParam("同名の名前がないか確認しています。", 0.3f); 
                }
                break;


            case 3://同名確認
                int MAX = 1;
                try
                {
                    string x = client.DownloadString("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/server/userData/" + pos.ToString().PadLeft(6, '0') + "/name.txt");
                    if (x == input_.text || x.Replace(" ", "").Replace("　", "") == input_.text.Replace(" ", "").Replace("　", ""))
                    {
                        REF("その名前は既に利用されています");
                    }
                }
                catch (Exception e)
                {  

                    //後で追加
                }

                pos++;
                if (pos == MAX+1)
                {
                    SetParam("登録を開始します。", 0.9f);
                }
                else
                {
                    cnt--;
                    SetParam("登録を開始します。", 0.3f + (0.6f/ MAX * pos));
                }

                break;
            case 4:
                if (iw > 0.4f)
                {
                    try
                    {
                        UserData.name = input_.text;
                        client.Encoding = System.Text.Encoding.UTF8;
                        client.UploadString("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/server/userData/" + UserData.userNum.ToString().PadLeft(6, '0') + "/name.txt", input_.text);
                        client.Dispose();
                        SetParam("登録しました!!", 1f);
                        Destroy(input_.transform.root.gameObject);
                        Destroy(this);
                        SceneLoader.Load("Avator");
                    }
                    catch (Exception e)
                    {
                        REF("登録に失敗しました。再度入力してください");
                    }
                }
                break;
        }
        
    }

    public void OnClick() 
    {
        button.interactable = input_.interactable = false;
        checkRoot.active = true;
        isCheck = true;
        cnt = 0;
        dotCnt = 0;
        SetParam("名前の文字数を確認中", 0);
        cnt = 0;
    }
    
    public void OnTextBoxChanged() 
    {
        input_.text = input_.text.Replace("#", "").Replace("\n", "").Replace("\r", "").Replace("%", "");
    }

    void SetParam(string word, float sv)
    {
        cnt++;
        iw = 0;
        checkText_sub.text = word;
        checkParam.value = sv;
    
    }

    void REF(string w)
    {
        Debug.Log("REF");
        pos = 0;
        isCheck = false;
        cnt = 0;
        dotCnt = 0;
        iw = 0;
        dotWaitCnt = 0;
        input_.text = "";
        samples.fontSize = OS_.isPC() ? 10 : 14;
        samples.text = w;
        samples.color = Color.red;
        button.interactable = input_.interactable = true;
        checkRoot.active = false;
        checkText_main.text = checkText_sub.text = "";
        checkParam.value = 0;

    }
}
