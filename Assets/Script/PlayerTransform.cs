using UnityEngine;

public sealed class PlayerTransform : MonoBehaviour
{
    void Start()
    { 
      // Players.Player = GameObject.Find("Player" ).transform;//��Ŕ�����
      // Players.PlayerCam = GameObject.Find("Player").transform;//��Ŕ�����
      //  Players.Player = GameObject.Find("Player" + AvatorSelect_ver_A.select.ToString()).transform;//��Ŕ�����
       // Players.PlayerCam = GameObject.Find("Player" + AvatorSelect_ver_A.select.ToString() + "/Main Camera").transform;//��Ŕ�����
    }
}


public class Players 
{
    public static Transform Player;
    public static Transform PlayerCam; 
}
