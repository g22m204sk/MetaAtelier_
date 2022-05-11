using UnityEngine;

public class RuntimeTask : MonoBehaviour
{
    public static bool isLock;

    [RuntimeInitializeOnLoadMethod]
    public static void TASK()
    { 
        Screen.fullScreen = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DontDestroyOnLoad(new GameObject("[Cursor&Screen System]").AddComponent<RuntimeTask>().gameObject);
    }

    public  void Update()
    {
        Screen.fullScreen = isLock;
        Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLock;
    }
}
