using UnityEngine;

using MonobitEngine;
public class AvatorSelect_ver_A :MonobitEngine.MonoBehaviour
{
    public static int select; 
    int v, LV = -100;
    GameObject fg;
    public Vector3[] poss;
    public const string ServerName = "MetaAtelierServer";
    public const string RoomName = "MetaAtelier";

    private void Start()
    {
        //　変更　サーバ接続
        MonobitNetwork.autoJoinLobby = true;
        MonobitNetwork.ConnectServer(ServerName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) LEFT(); 
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) RIGHT();
        if (Input.GetKeyDown(KeyCode.Space)) NEXT();
    }

    public void LEFT()
    {
        v--;
        if (v < 0) v = 1;
    }

    public void RIGHT()
    {
        v++;
        if (v > 1) v = 0;
    }

    public void NEXT()
    {

        //EnterRoom();    
        select = v;
        OpenAndClose.Go();

        //追記　ルーム入室
        Destroy(this);
    }

    private void FixedUpdate()
    {
        if (LV != v)
        {
            if (fg != null) Destroy(fg);
            fg = Instantiate(Resources.Load("CHARA" + v) as GameObject);
            fg.transform.rotation = Quaternion.Euler(0, 180,0);
            fg.transform.position = poss[v];
        }
        LV = v;
    }

    //変更　入室処理
    public void EnterRoom()
    {
        if (!MonobitNetwork.inRoom)
        {
            RoomData[] _roomDatas = MonobitNetwork.GetRoomData();
            try
            {
                  
                if (_roomDatas.Length >= 1) MonobitNetwork.JoinRoom(RoomName);
                else MonobitNetwork.CreateRoom(RoomName); 
            }
            catch (System.Exception e) {   }
        }
    }
}
