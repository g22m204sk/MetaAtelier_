using System.Collections;
using System.Collections.Generic;
using MonobitEngine;

public delegate void ENTER();
public delegate void LEAVE();
public class NetworkCtrler : MonobitEngine.MonoBehaviour
{
    public const string ServerName = "MetaAtelierServer";
    public const string RoomName = "MetaAtelier";

    public bool _enterFlag = false;
    public bool _entering = false;

    public ENTER _enter;
    public LEAVE _leave;
    private void Awake()
    {
        //CustomData‚Å’Ç‰Á
        //if (!this.gameObject.GetComponent<MonobitView>())
        //    this.gameObject.AddComponent<MonobitView>();
        
    }
    // Ž©“®“üŽº
    void Update()
    {
        if (!MonobitNetwork.isConnect)
        {
            MonobitNetwork.autoJoinLobby = true;
            MonobitNetwork.ConnectServer(ServerName);
        }
        else
        {
            if (!MonobitNetwork.inRoom && !_entering)
            {
                RoomData[] _roomDatas = MonobitNetwork.GetRoomData();
                try
                {
                    if (_roomDatas.Length >= 1) MonobitNetwork.JoinRoom(RoomName);
                    else MonobitNetwork.CreateRoom(RoomName);
                }
                catch (System.Exception e) { }
                //_entering = true;
            }
            else
            {
                if (!_enterFlag)
                {
                    _enter();
                    _enterFlag = true;
                }
            }

        }
        
    }
}
