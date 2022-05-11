using System; 
using UnityEngine; 
using UnityEngine.Video;

public class MovieSystems : MonoBehaviour
{
    public float Line = 16;
    const float movLen = 2176; 
    float cnt,d;
    void Awake()
    { 
        GetComponent<VideoPlayer>().prepareCompleted += Completed;
        GetComponent<VideoPlayer>().Prepare();
    }

    private void Update()
    {
        cnt += Time.deltaTime;
        if (cnt > 0.1)
        {
            cnt = 0;
            d = Vector3.Distance(Players.Player.position, new Vector3(4.76f,1f,-22f));
            GetComponent<AudioSource>().volume = (d > Line ? 0 : ((Line - d) / Line) * (Line - d) / Line);
        }
    }

    void Completed(VideoPlayer s)
    { 
        s.Play();  
        float x = ((float)((DateTime.Now - new DateTime(2022,1,1,0,0,0,0)).TotalMilliseconds) * 0.001f)%movLen;
        s.time = x; 
    }
}
  