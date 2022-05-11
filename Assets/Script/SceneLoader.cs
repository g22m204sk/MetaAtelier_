using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class SceneLoader
{
    public static bool IsFade 
    { 
        get => Fader.IsF; 
    }

    public static Color FadeColor
    {
        set
        {
             value.a = 1;
             Fader.FadeColor = value;
        }
        get
        {
            return Fader.FadeColor;
        }
    }



    public static float FadeInTime
    {
        get
        {
            return Fader.FadeInTime;
        }
        set
        {
            if(value < 0)
                Debug.LogError("SceneLoader ::(2)::フェードインの時間は正の値でなくてはなりません");
            Fader.FadeInTime = value > 0 ? value : 2;
        }
    }




    public static float FadeOutTime
    {
        get
        {
            return Fader.FadeOutTime;
        }
        set
        {
            if (value < 0)
                Debug.LogError("SceneLoader ::(1)::フェードアウトの時間は正の値でなくてはなりません");
            Fader.FadeOutTime = value > 0 ? value : 2;
        }
    }





    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void StartUp()
    { 
        SceneManager.sceneLoaded += delegate (Scene n, LoadSceneMode m) 
        {
            Fader.FadeHandle = true; 
        };
    }
    

     
    public static void LoadN(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadN(int index  )
    {
        SceneManager.LoadScene(index);
    }

    public static void Load(string name)
    {
        fdr().Set(name);
    }

    public static void Load(int index  )
    {
        fdr().Set(index);
    }



    static Fader fdr()
    {
        DateTime db = DateTime.Now;
        GameObject canvas = new GameObject("[Fade System]");
        canvas.AddComponent<Fader>();
        canvas.AddComponent<RectTransform>();
        Canvas cc = canvas.AddComponent<Canvas>();
        cc.renderMode = RenderMode.ScreenSpaceOverlay;
        cc.sortingOrder = 38;
        CanvasScaler cs = canvas.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(100, 100);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        cs.matchWidthOrHeight = 0;
        cs.referencePixelsPerUnit = 100;

        GameObject img = new GameObject("Image");
        img.transform.SetParent(canvas.transform);
        RectTransform ir = img.AddComponent<RectTransform>();
        ir.position = new Vector3(0, 0, 0);
        ir.rotation = Quaternion.Euler(0,0,0);
        ir.anchorMin = new Vector2(0.5f, 0.5f);
        ir.anchorMax = new Vector2(0.5f, 0.5f);
        ir.pivot = new Vector2(0.5f, 0.5f);
        ir.localScale = new Vector3(1,1,1);
        ir.sizeDelta = new Vector2(10000, 10000);
        img.AddComponent<CanvasRenderer>();
        RawImage r = img.AddComponent<RawImage>();
        r.uvRect = new Rect(0, 0, 1, 1);

        //DateTime da = DateTime.Now;
        //Debug.Log((da - db).TotalMilliseconds + ":::" + db.Second + "/" + db.Millisecond + ":::" + da.Second + "/" + da.Millisecond);
        return canvas.GetComponent<Fader>();
        //  return (MonoBehaviour.Instantiate(Resources.Load("fader") as GameObject) as GameObject).AddComponent<Fader>();
    }


} 


 

enum FADEMODE
{
    IN = -1,
    OUT = 1
}



public class Fader : MonoBehaviour
{
    public static bool FadeHandle;
    static bool IsActive;
    FADEMODE mode = FADEMODE.OUT;
    string _name = "";
    int _index = int.MinValue;
    public RawImage targetUI;
    public static Color FadeColor = new Color(0,0,0,1);
    public static float FadeInTime = 2;
    public static float FadeOutTime = 2;
    float vals;
    public static bool IsF { get => IsActive; } 

    public void Set(string s)
    {
        SetRAW();
        _name = s;
    }


    public void Set(int a)
    {
        SetRAW();
        _index = a;
    }



    void SetRAW()
    {
        if(IsActive)
            Debug.LogError("SceneLoader ::(3)::同時に2つのフェード処理はできません");
        IsActive = true;
        DontDestroyOnLoad(gameObject);
        FadeHandle = false; 
        targetUI = transform.Find("Image").GetComponent<RawImage>();
        targetUI.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, 0);
    }

    

     
    void Update()
    {
        vals += Time.deltaTime;

        if (mode == FADEMODE.IN)
        {
            targetUI.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, 1 - (float)Math.Sin(((vals - FadeOutTime ) / FadeInTime) * 90 * (Math.PI / 180)));
            if (vals>= FadeInTime + FadeOutTime)
            {
                IsActive = false;
                Destroy(gameObject);
            }
        }

        else
        {
            targetUI.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b,  (float)Math.Sin((vals/FadeOutTime) * 90 * (Math.PI / 180)));

            if (FadeHandle)
                mode = FADEMODE.IN;

            if (vals >= FadeOutTime)
            {
               // vals = 0;
                if (_name != string.Empty)
                    SceneLoader.LoadN(_name);
                else if (_index >= 0)
                    SceneLoader.LoadN(_index);
            } 
        }
 
    }  

}

