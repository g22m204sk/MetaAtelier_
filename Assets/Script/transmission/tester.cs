using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    public CustomData _data= new CustomData();
    public NetworkCtrler ctrler;


    // Start is called before the first frame update
    void Start()
    {
        _data = this.gameObject.GetComponent<CustomData>();
        ctrler = GetComponent<NetworkCtrler>();
         
        ctrler._enter = _data.OnEnterRoom;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
