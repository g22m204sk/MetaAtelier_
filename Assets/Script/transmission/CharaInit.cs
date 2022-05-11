using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
public class CharaInit :MonobitEngine.MonoBehaviour
{
    /*
    [Range(0, 1)]
    public int selecter = 0;    */
    public bool INIT_BTN =true;

    public float _waitTime = 0.5f;
    public float _timer = 0f;
    public const string RoomName = "MetaAtelier";

    static Vector3  _spawnPoint = new Vector3(18f, 2f, -15f);
    static Vector3 _spawnRotate = new Vector3(0, -45f, 0);

    public static GameObject _dummy1 , _dummy2;

    bool _entering = false;

    private void Start()
    {
        INIT_BTN = true;
        _entering = false;
        _dummy1 = GameObject.Find("dummy_1");
        _dummy2 = GameObject.Find("dummy_2");
        Players.Player = _dummy1.transform;
        Players.PlayerCam = _dummy2.transform;
    }

    // Update is called once per frame
    /* */
    void Update()
    {
        _timer += Time.deltaTime;
        if (_waitTime > _timer) return;

        if (MonobitNetwork.inRoom)
        {
            if (INIT_BTN)
            { 
                Init(AvatorSelect_ver_A.select);
                //_dummy1.SetActive(false);
                //_dummy2.SetActive(false);
                INIT_BTN = false;
                this.gameObject.SetActive(false); 
            }
        }
        else
        {
            if (_entering) return;
            RoomData[] roomDatas = MonobitNetwork.GetRoomData();
            try
            {
                if (roomDatas.Length >= 1) MonobitNetwork.JoinRoom(RoomName);
                else MonobitNetwork.CreateRoom(RoomName);
                _entering = true;
            }
            catch (System.Exception e) { }
        }

    }

    public static void Init(int value)
    {  
        GameObject tmp = MonobitNetwork.Instantiate("Player/Player" + value.ToString(),_spawnPoint, Quaternion.Euler(_spawnRotate), 0);
        Players.Player = tmp.transform;//GameObject.Find("Player" + value.ToString()).transform;//Œã‚Å”ƒ‚¦‚é
        Players.PlayerCam = tmp.transform.GetChild(0);//GameObject.Find("Player" + value.ToString() + "/Main Camera").transform;//Œã‚Å”ƒ‚¦‚é
    }

}
