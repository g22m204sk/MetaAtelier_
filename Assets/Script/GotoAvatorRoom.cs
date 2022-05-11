using UnityEngine;

public class GotoAvatorRoom : MonoBehaviour
{ 
    void OnTriggerEnter(Collider a)
    {
        if (a.gameObject == Players.Player.gameObject)
            SceneLoader.Load("Avator");

        //追記　全プレイヤーの画面から古いplayerのGameObjectを削除
        MonobitEngine.MonobitNetwork.Destroy(Players.Player.gameObject);
        Players.Player = CharaInit._dummy1.transform;       //念のためdummyをあてがう
        Players.PlayerCam = CharaInit._dummy2.transform; 
    }
}
