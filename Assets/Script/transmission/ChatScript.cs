using MonobitEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
public class ChatScript : MonobitEngine.MonoBehaviour
{
    #region Garbage
    //プレイヤー名
    string _playerName;

    //プレイヤーID
    int _playerId = -1;

    //inputfieldの文字を消すためのボタン
    Button _ClearSendTextBtn;

    //文字を送るためのボタン
    Button _SendBtn;

    //inputfieldの文字を消すためのボタン
    Button _ClearReceiveTextBtn;

    //送信する文字を入れるフィールド
    Text _receiveText;

    //受信する文字を入れるフィールド
    InputField _sendField;

    [SerializeField]
    //部屋に入るためのボタン
    Button _enterButton;
     
     
    //MonobitViewコンポーネント
    [SerializeField]
    MonobitView _monobitView;

    //プレイヤー名入力欄GameObject
    [SerializeField]
    GameObject _nameObject;

    InputField _nameInputField;

    //送信する文字を入れるGameObject
    [SerializeField]
    GameObject _sendObject;

    //受信する文字を入れるGameObject
    [SerializeField]
    GameObject _receiveObject;
    #endregion


    #region test parameters
    private string[] _messages = new string[]
    {
        "こんにちは",
        "Hello",
        "ニィハオ"
    };

    private int[] _nums = new int[]
    {
        30000,
        54,
        5431,
        7894
    };

    #endregion

    public const string ServerName = "MetaAtelierServer";
    public const string RoomName = "MetaAtelier";

    public string SEND_DATA = "aaa";
    public string RECEIVE_DATA = "bbb";
    public bool SEND_BTN = false;

    public GameObject _chatP , _popP;
    public Image _receivePanel;
    public Text _sendBTNText , _sendFieldText;

    public float _fadeSpeed = 1f;

    public float _fadeTime = 5;
    public float _timer = 0f;

    public static bool _IsChat = false;

    #region Chat Methods
    public void SendChat(string text)
    {
        if (string.IsNullOrEmpty(text)) return;

        text = ForbiddenWords.WordFilter(text);

        //表現が規制された時
        if (string.IsNullOrEmpty(text))
        {
            //Debug.LogWarning("不適切な表現を含むため送信できません");
            _receiveText.text += "不適切な表現を含むため送信できません" + Environment.NewLine;
            _sendField.text = "";
            return;
        }

        _sendField.text = "";
        _IsChat = false;
        //Debug.Log("送信00");
        _monobitView.RPC("ReceiveChat", MonobitTargets.All, text,_playerId);
        if (EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);  //フォーカスを外す
    }

    [MunRPC]
    void ReceiveChat(string message,int id)
    {

        _receiveText.text += (_playerId == id) ?
             "自分 : " + message + Environment.NewLine :
              message + Environment.NewLine ;
        //Debug.Log("受信　：" + message);
    }


    public void SendChat()
    {
        string text = _sendField.text;
        if (string.IsNullOrEmpty(text)) return;

        text = ForbiddenWords.WordFilter(text);

        //表現が規制された時
        if (string.IsNullOrEmpty(text))
        {
            //ebug.LogWarning("不適切な表現を含むため送信できません");
            return;
        }
        _sendField.text = "";
        //Debug.Log("送信");
        _monobitView.RPC("ReceiveChat", MonobitTargets.All, text, _playerId);
    }

    #endregion
    #region Unity CallBacks

    // Start is called before the first frame update
    void Start()
    {
        
        //各種コンポーネントの取得------------------------------------

        //_nameInputField = _nameObject.GetComponent<InputField>();
        _sendField = _sendObject.GetComponent<InputField>();
        _sendFieldText = _sendObject.transform.GetChild(1).GetComponent<Text>();
        _receiveText = _receiveObject.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        //_enterButton = _nameObject.transform.GetChild(2).GetComponent<Button>();
        //_ClearSendTextBtn = _sendObject.transform.GetChild(3).GetComponent<Button>();
        _SendBtn = _sendObject.transform.GetChild(4).GetComponent<Button>();
        //_ClearReceiveTextBtn = _receiveObject.transform.GetChild(0).GetComponent<Button>();

        //---------------------------------------------------------------------------


        //Buttonのイベントを追加-----------------------------------------------------
        
        _SendBtn.onClick.AddListener(SendChat);
        _popP.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Popup);
        _sendField.onEndEdit.AddListener(SendChat);
        //_ClearReceiveTextBtn.onClick.AddListener(ClearReceive);
        //_ClearSendTextBtn.onClick.AddListener(ClearSend);
        //_enterButton.onClick.AddListener(EnterRoom);

        //---------------------------------------------------------------------------
        

        _chatP.SetActive(false);
        _popP.SetActive(true);
        //_sendObject.SetActive(false);
        //_receiveObject.SetActive(false);

        //_sendField.Select();
        //_receiveText.text += Environment.NewLine;


        if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            _monobitView = GetComponentInParent<MonobitEngine.MonobitView>();

        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)// 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
            _monobitView = GetComponentInChildren<MonobitEngine.MonobitView>();

        else// 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
            _monobitView = GetComponent<MonobitEngine.MonobitView>();

        if (_monobitView == null)
            _monobitView = this.gameObject.AddComponent<MonobitView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (MonobitNetwork.isConnect)
        { 
            if (!MonobitNetwork.inRoom)
            {
                //RoomNameの部屋があるなら入室　ないなら作る
                foreach(var room in MonobitNetwork.GetRoomData())
                {
                    if (room.name == RoomName)
                    {
                        MonobitNetwork.JoinRoom(RoomName);
                    }
                }
                MonobitNetwork.CreateRoom(RoomName);
            }
            else
            {
                fadeUI();
                if (!_sendField.isFocused && Input.GetKeyDown(KeyCode.T))
                {
                    Debug.Log("aaaa");
                    Popup();
                }
                if( Input.GetKeyDown(KeyCode.Escape))
                {
                    PopDown();
                }

                if (SEND_BTN)
                {
                    SendChat(SEND_DATA);
                    SEND_DATA = "";
                    SEND_BTN = false;
                }
            }
            _playerId = MonobitNetwork.player.ID;   
        } 


    }
    #endregion

    #region private Methods

   /* private void EnterRoom()
    {
        if (MonobitNetwork.isConnect)
            if (!MonobitNetwork.inRoom)
            {
                _playerName = _nameInputField.text;
                Debug.Log("プレイヤー名" + _playerName);

                //部屋が一個以上存在するとき

                if (MonobitNetwork.GetRoomData().Length >= 1)
                {
                    foreach (var room in MonobitNetwork.GetRoomData())
                    {
                        if (room.name == "ChatRoom")
                        {
                            MonobitNetwork.JoinRoom("ChatRoom");   //ChatRoomに入る
                            //名前入力欄を消してチャットUIを出す
                            _nameObject.SetActive(false);
                            _sendObject.SetActive(true);
                            _receiveObject.SetActive(true);
                            _receiveText.text = " 名前: " +_playerName + Environment.NewLine;
                            SendMessage(_playerName + "が入室しました");
                            Debug.Log(_playerName + "が入室しました");
                        }

                    }

                }
                else
                {
                    MonobitNetwork.CreateRoom("ChatRoom"); // "ChatRoom"という名前で部屋作成
                    Debug.Log("\"ChatRoom\"作成");
                    //名前入力欄を消してチャットUIを出す

                    MonobitNetwork.player.name = _nameInputField.text;
                    _receiveText.text = " 名前: "+ MonobitNetwork.player.name + Environment.NewLine;

                    _nameObject.SetActive(false);
                    _sendObject.SetActive(true);
                    _receiveObject.SetActive(true);

                    SendMessage(_playerName + "が入室しました");
                    Debug.Log(_playerName + "が入室しました");
                }
            }
    }*/

    #endregion


    #region BTN Methods
    /*
    public void ClearSend()
    {
        _sendField.text = string.Empty;

        Debug.Log("送信フィールドクリア");
    }

    public void ClearReceive()
    {
        _receiveText.text = "";
        Debug.Log("受信フィールドクリア");
    }
    */
    public void Popup()
    {
        Debug.Log("popUP");

        _popP.SetActive(false);

        _sendField.image.color = Color.white;
        _sendFieldText.color = Color.black;
        _receiveText.color = Color.black;
        _receivePanel.color = new Color(1f, 1f,1f, 0.4f);
        _SendBtn.image.color = Color.white;
        _sendBTNText.color = Color.black;

        _sendField.text = "";
        _timer = 0f;
        _chatP.SetActive(true);
        _sendField.Select();
        _IsChat = true;
    }

    public void PopDown()
    {
        _IsChat = false;
        _sendField.text = "";
        _popP.SetActive(true);
        _chatP.SetActive(false);
    }


    public void fadeUI()
    {
        _timer += Time.deltaTime;

        if (_sendField.text !="")
        {
            _timer = 0f;
        }

        if (_fadeTime  < _timer)
        {
            _sendField.image.color -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            _sendFieldText.color -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            _receiveText.color -= new Color(0, 0, 0, _fadeSpeed *2* Time.deltaTime);
            _receivePanel.color -= new Color(0, 0, 0, _fadeSpeed /2* Time.deltaTime);
            _SendBtn.image.color -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            _sendBTNText.color -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);

            if (_fadeTime * 1.3 < _timer)
                PopDown();
        }
    }


#endregion

}


//不適切な表現を規制するクラス
public class ForbiddenWords
{
#region Static Fields
    static readonly string[] Words = new string[]{
"青姦","あおかん",
"アメ公","あめこう",
"アルコール依存症",
"犬殺し","いぬごろし",
"淫売","いんばい","売春",
"うんこ","うんこ",
"うんち",
"穢多","えた",
"ガキ","がき",
"皮被り","かわかぶり","包茎",
"姦通","かんつう",
"キ印","きじるし","精神障害者",
"キチ","きち",
"気違い",
"屑屋","くず","クズ",
"くわえ込む","くわえこむ",
"クンニ","くんに",
"強姦","ごうかん",
"ゴミ","ごみ",
"千摺り","せんずり","オナニー",
"ちんこ","チンコ","ちんぽ","チンポ","ちんちん","チンチン","ソープ",
"非人","ひにん",
"ブス","ぶす",
"部落","ぶらく",
"マンコ","まんこ","女性器","ほーみ","べちょこ","おめこ",
"セックス","せっくす"
,"あなる","アナル",
"おっぱい","オッパイ",
"死ね",
"ガイジ","がいじ",
"エロ","インポ","いんぽ","陰毛","淫乱","えっち","エッチ","おしり","顔射","がんしゃ","射精","スケベ","スカトロ","絶倫","前立腺","ちぇりーぼーい","チェリーボーイ","童貞","どうてい","てまん","手マン","なかだし","膣","発情","フェラ","ホモ","ほも","マリファナ","レイプ","性交",
"シコシコ",
"fuck","Fuck",
"shit","Shit",
"Suck","suck",
"faggot","Faggot",
"Nigger","Negro","Nigga","nigger","negro","nigga",
"sex","Sex","SEX","piss","dick","Dick","cunt","Cunt","tits","Tits",
"ass","Ass","prick","1919","4545","0721"

    };

#endregion

    public static string WordFilter(string text)
    {
        foreach (string s in Words)
        {
            if (text.Contains(s))
            {
                text = "";
            }
        }

        return text;
    }

}

