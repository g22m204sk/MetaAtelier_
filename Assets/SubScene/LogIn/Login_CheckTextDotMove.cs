using UnityEngine; 

public class Login_CheckTextDotMove : MonoBehaviour
{
    float c;
    int cnt;  
    void Awake() => cnt = 0; 

    void Update()
    {
        c += Time.deltaTime;
        if (c > 0.3f)
        {
            c = 0; 
            cnt = (cnt + 1) % 4;
            GetComponent<UnityEngine.UI.Text>().text = "".PadLeft(cnt, '.');
        }
    }
}
