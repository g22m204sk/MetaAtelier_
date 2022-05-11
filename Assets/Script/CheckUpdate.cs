using UnityEngine; 

public class CheckUpdate 
{
    public static bool IsUpdateQuit = false;
    [RuntimeInitializeOnLoadMethod]
    public static void Check()
    {
        IsUpdateQuit =
            (Application.platform == RuntimePlatform.Android && GetTEXT("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/apkversion.txt") != ApplicationInfo.ANDROID) ||
            (Application.platform == RuntimePlatform.IPhonePlayer && GetTEXT("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/iosversion.txt") != ApplicationInfo.IOS);
         
    }

    public static string GetTEXT(string url)
    {
        WWW w = new WWW(url);
        while (!w.isDone) ;
        return w.text;
    }
}
