using UnityEngine;

public sealed class PlayerTransform : MonoBehaviour
{
    void Start()
    { 
      // Players.Player = GameObject.Find("Player" ).transform;//Œã‚Å”ƒ‚¦‚é
      // Players.PlayerCam = GameObject.Find("Player").transform;//Œã‚Å”ƒ‚¦‚é
      //  Players.Player = GameObject.Find("Player" + AvatorSelect_ver_A.select.ToString()).transform;//Œã‚Å”ƒ‚¦‚é
       // Players.PlayerCam = GameObject.Find("Player" + AvatorSelect_ver_A.select.ToString() + "/Main Camera").transform;//Œã‚Å”ƒ‚¦‚é
    }
}


public class Players 
{
    public static Transform Player;
    public static Transform PlayerCam; 
}
