using UnityEngine;

public class GoAvator : MonoBehaviour
{
#if UNITY_EDITOR
    public static bool isAvator;
    // Start is called before the first frame update
    public void Start()
    {
        if (!isAvator)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Avator");
        isAvator = true;
        Destroy(this);
    }
#endif
}
