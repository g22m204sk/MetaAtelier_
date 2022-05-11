using UnityEngine;

public class OpenQuestionnaire : MonoBehaviour
{
    void OnApplicationQuit()
    {
#if !UNITY_EDITOR
     
        if (!CheckUpdate.IsUpdateQuit)   
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeqj250bFxlpkmHiLzlfxVBs1lQlAZTV3C5aDgwcU7IKx6kPQ/viewform");
#endif
    }

    [RuntimeInitializeOnLoadMethod]
    static void MAKE()=> DontDestroyOnLoad(new GameObject("[OpenQuestionnaire]").AddComponent<OpenQuestionnaire>().gameObject);
}
