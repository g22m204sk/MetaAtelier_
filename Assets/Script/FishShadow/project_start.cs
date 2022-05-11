using UnityEngine;
using UnityEngine.Video;

public class project_start : MonoBehaviour
{   
    public VideoPlayer fPlayer;
    private int dateTime;

    void Update()
    {
        Vector3 dd = Players.Player.transform.position;

        if (dd.x < -113 && dd.x > -206.7 && dd.y <93 && dd.y > 33 &&  dd.z < 33 && dd.z > -33)
        {
            if (!fPlayer.isPlaying)
            {
                fPlayer.Play();
                dateTime = int.Parse(System.DateTime.Now.Second.ToString())%111;
                fPlayer.time = dateTime;
            }
        }
        else if (fPlayer.isPlaying)
        {
            fPlayer.Pause();
        }
    } 
}

