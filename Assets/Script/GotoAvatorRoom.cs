using UnityEngine;

public class GotoAvatorRoom : MonoBehaviour
{ 
    void OnTriggerEnter(Collider a)
    {
        if (a.gameObject == Players.Player.gameObject)
            SceneLoader.Load("Avator");

        //�ǋL�@�S�v���C���[�̉�ʂ���Â�player��GameObject���폜
        MonobitEngine.MonobitNetwork.Destroy(Players.Player.gameObject);
        Players.Player = CharaInit._dummy1.transform;       //�O�̂���dummy�����Ă���
        Players.PlayerCam = CharaInit._dummy2.transform; 
    }
}
