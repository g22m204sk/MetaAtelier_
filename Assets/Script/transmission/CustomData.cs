using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEditor;

public class CustomData : MonobitEngine.MonoBehaviour
{
    // ���f���ԍ��ƃv���C���[ID���i�[
    // ��������ID������static���X�g�ɒǉ�
    // �ޏo���Ɏ��g���폜

    public int _myID { private set{} get {
#if UNITY_EDITOR          
            if (EditorApplication.isPlaying)return MonobitNetwork.player.ID;
            else { return -1; }
#else
return -1;
#endif
        }
    }
    public string _data = "";
    MonobitView _monobitView = null;

    public static List<CustomData> PlayerList = new List<CustomData>();

    int _saveNum = -1;
    string _saveData = "-1";
     

    /*
    private void Start()
    {
        if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            _monobitView = GetComponentInParent<MonobitEngine.MonobitView>();
        
        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)// �e�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���ׂĂ̎q�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            _monobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
        
        else// �e�q�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���g�̃I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g���������Đݒ肷��
            _monobitView = GetComponent<MonobitEngine.MonobitView>();
        
        if(_monobitView == null)
            _monobitView = this.gameObject.AddComponent<MonobitView>();
        //_myID = MonobitNetwork.player.ID;

        _monobitView.ObservedComponents.Add(this);
    }
    private void Update()
    {
        //�����Ƃ��f�[�^�������Ă����Ƃ�
        if(_saveData != "-1" && _saveNum != -1)
        {

            CustomData tmp_data = new CustomData(_saveNum, _saveData);

            PlayerList.Add(tmp_data);
             

            _saveNum = -1;
            _saveData = "-1";
        }
    }*/
     

    public CustomData(int id, string data)
    {
        _myID = id;
        _data = data;
    }
    public CustomData()
    {
        _myID = -1;
        _data = "";
    }
    #region Dont Use
    [MunRPC]
    public void Receive(object[] data)
    {
        _myID = (int)data[0];
        _data = (string)data[1];
    }

    [MunRPC]
    public void GetList(object[,] data)
    {
        PlayerList = ArrayToList(data);
    }


    //�N������������x�z�X�g���S���Ƀv���C���[�̏����X�V������
    [MunRPC]
    public void SendList()
    {
        var tmp = ListToArray(); 
        _monobitView.RPC("GetList", MonobitTargets.All, tmp);// doesn't support type object[,]
    }

    //�z�X�g�̃��X�g�Ɏ��g��ǉ������A�ŐV�̃��X�g���擾
    [MunRPC]
    public void AddMyData(object[] data)
    {
        PlayerList.Add(new CustomData((int)data[0], (string)data[1]));
        SendList();
    }

    //�z�X�g��CustomData�̃��X�g�����N�G�X�g
    public void GetAllData()
    {
        _monobitView.RPC("SendList", MonobitTargets.Host);
    }

    public void SendData()
    {
        _monobitView.RPC("Receive", MonobitTargets.OthersBuffered, new object[] { _myID, _data });
    }
#endregion

    [MunRPC]
    public void ReceiveNum(int value = -1)
    {
        _saveNum = value;
    }

    [MunRPC]
    public void ReceiveData(string value = "-1")
    {
        _saveData = value;
    }

    public void OnEnterRoom()
    {
        if (MonobitNetwork.isHost)
        {
            PlayerList.Add(this); 
        }
        else
        {
            _monobitView.RPC("ReceiveNum", MonobitTargets.Host, _myID);
            _monobitView.RPC("ReceiveData", MonobitTargets.Host, _data);
        }

    }

    object[,] ListToArray()
    {
        object[,] tmp_array = new object[PlayerList.Count, 2];
        for (int i = 0; i < tmp_array.GetLength(0); i++)
        {
            tmp_array[i, 0] = PlayerList[i]._myID;
            tmp_array[i, 1] = PlayerList[i]._data;
        }
        return tmp_array;
    }

    List<CustomData> ArrayToList(object[,] array)
    {
        int len = array.Length;
        List<CustomData> tmp_list = new List<CustomData>();
        for (int i = 0; i < len; i++)
        {
            tmp_list.Add(new CustomData((int)array[i, 0], (string)array[i, 1]));
        }
        return tmp_list;
    }
     
     
    Hashtable[] ListToHash()
    {
        Hashtable[] tmp_hash = new Hashtable[PlayerList.Count];
        for(int i = 0;i < tmp_hash.Length; i++)
        {
            tmp_hash[i].Add("ID", PlayerList[i]._myID);
            tmp_hash[i].Add("DATA", PlayerList[i]._data);
        }
        return tmp_hash;
    }

    List<CustomData> HashToList(Hashtable[] data)
    {
        List<CustomData> tmp_list = new List<CustomData>();
        
        for(int i =0; i < data.Length; i++)
        {
            //�L�[����Ɏ��o��List�ɕϊ�
            tmp_list.Add(new CustomData((int)data[i]["ID"],(string)data[i]["DATA"]));
        }

        return tmp_list;
    }


    //RPC�ŐV�K�ǉ�
    //Stream�őS���ɋ��L

    //PlayerList�����L
    /*
    public void  OnMonobitSerializeViewWrite(MonobitEngine.MonobitStream stream, MonobitEngine.MonobitMessageInfo info)
    {
        Hashtable[] tmp_data = ListToHash();
        stream.Enqueue(tmp_data); 
    }
    //PlayerList���󂯎�肽��
    public void OnMonobitSerializeViewRead(MonobitEngine.MonobitStream stream, MonobitEngine.MonobitMessageInfo info)
    {
        Hashtable[] receive = (Hashtable[])stream.Dequeue();
        List<CustomData> data = HashToList(receive);

        PlayerList = data; 

    }*/
    public override string ToString()
    {
        return "ID:" + _myID + "   Data:" + _data; 
    }




}

