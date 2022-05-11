using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccel : MonoBehaviour
{

    /*
    //階段だけスピードアップ 壁抜けがひどくなる
    PlayerMove _move;

    float _originSpeed;
    public float _afterSpeed =15;


    void Start()
    {
        _move = this.GetComponent<PlayerMove>();
        _originSpeed = _move.moveSpeed;
    }

    private void FixedUpdate()
    {
        if (_move == null) _move = this.GetComponent<PlayerMove>();
        _move.moveSpeed = _originSpeed;
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<LightSwitch>())
            _move.moveSpeed = _afterSpeed;
        else
        {
            _move.moveSpeed = _originSpeed;
        }
        return;   
        
    }
     */
}
