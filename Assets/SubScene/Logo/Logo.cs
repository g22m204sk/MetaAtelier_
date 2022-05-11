using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{ 
    public float CntAudioTime { get => cntA; } 
    public int ImagePoint { get => p; } 
    public float WaitStartTime = 0.4f;
    public float ColorReplacementTime = 0.4f;
    public string targetScene = "ここにシーン名を入れる";

    [Space(20)]
    public IMG[] Graphics = new IMG[1];

    [Space(30)]
    public Audio[] Audios = new Audio[1];

    int p;
    float cntA, cntG;
    AudioSource as_;
    RawImage img, tex;

    void Start()
    {
        img = GameObject.Find("Canvas/scr").GetComponent<RawImage>();
        tex = GameObject.Find("Canvas/tex").GetComponent<RawImage>();
        as_ = gameObject.AddComponent<AudioSource>();
        if (Graphics.Length > 0)
        {
            tex.texture = Graphics[0].img;
            img.color = Graphics[0].FadeIn.color;
        }
    }

    void Update()
    {
        if (WaitStartTime > 0)
        {
            WaitStartTime -= Time.deltaTime; 
            return;
        }
        WaitStartTime = 0;

        cntA += Time.deltaTime;
        if (Graphics[p] != null)
        {
            cntG += Time.deltaTime;
            switch (Graphics[p].mode)
            {
                case IMG.MODE.IN:
                    img.color = new Color
                     (
                         Graphics[p].FadeIn.color.r,
                         Graphics[p].FadeIn.color.g,
                         Graphics[p].FadeIn.color.b,
                         1 - cntG / Graphics[p].FadeIn.time
                     );
                    if (cntG > Graphics[p].FadeIn.time)
                    {
                        Graphics[p].mode = IMG.MODE.WAIT;
                        cntG = 0;
                    }
                    break;

                case IMG.MODE.WAIT:
                    img.color = new Color();
                    if (cntG > Graphics[p].FadeIn.time)
                    {
                        if (p + 1 == Graphics.Length)
                        {
                            int y = 0;
                            foreach (var i in Audios) if (i != null) y++;
                            if (y == 0)
                            {
                                SceneLoader.Load( CheckUpdate.IsUpdateQuit ? "MobileUpdate" : targetScene);
                                Destroy(this);
                            }
                        }
                        else
                        {
                            Graphics[p].mode = IMG.MODE.OUT;
                            cntG = 0;
                        }
                    }
                    break;

                case IMG.MODE.OUT:
                    img.color = new Color
                     (
                         Graphics[p].FadeOut.color.r,
                         Graphics[p].FadeOut.color.g,
                         Graphics[p].FadeOut.color.b,
                         cntG / Graphics[p].FadeOut.time
                     );
                    if (cntG > Graphics[p].FadeOut.time)
                    {
                        p++;
                        if (p < Graphics.Length)
                            tex.texture = Graphics[p].img;
                        cntG = 0;
                        Graphics[p].mode = IMG.MODE.FF;
                    }
                    break;


                case IMG.MODE.FF:
                    if (p < Graphics.Length)
                    {
                        img.color = Color.Lerp(Graphics[p - 1].FadeOut.color, Graphics[p].FadeIn.color, cntG / ColorReplacementTime);
                    }
                    else
                    {
                        Debug.LogError("FATAL ERROR");
                    }

                    if (cntG >= ColorReplacementTime)
                    {
                        cntG = 0;
                        Graphics[p].mode = IMG.MODE.IN;
                    }
                    break;
            }
        }
        else //nullなら次を走査していく
        {
            p++;
            while (Graphics != null && Graphics[p] != null)
            {
                if (p >= Graphics.Length)
                {
                    p = Graphics.Length - 1;
                    Graphics = null;
                }
            }
        }
        for (int v = 0; v < Audios.Length; v++)
        {
            if (Audios[v] != null && Audios[v].time < cntA)
            {
                if (Audios[v].clip != null && Audios[v].volume > 0)
                    as_.PlayOneShot(Audios[v].clip, Audios[v].volume);
                Audios[v] = null;
            }
        }
    }



#if UNITY_EDITOR
    private void OnValidate()
    { 
        if (Graphics.Length > 0 && Graphics[Graphics.Length - 1].FadeOut.color == SceneLoader.FadeColor)
        {
            Debug.LogWarning("LOGO画面の最後のフェードアウトの色はSceneLoader.FadeColorと一致してなくてはなりません");
            Graphics[Graphics.Length - 1].FadeOut.color = SceneLoader.FadeColor;
        }
    }
#endif


    [System.Serializable]
    public class Audio
    {
        public AudioClip clip;
        [Range(0, 20)]
        public float time;
        [Range(0, 1)]
        public float volume;
    }


    [System.Serializable]
    public class IMG
    {
        public Texture2D img;
        [Range(0.1f, 2)]
        public float waitTime = 0.5f;
        public FadeParam FadeIn, FadeOut;

        [System.NonSerialized]
        public MODE mode;

        [System.Serializable]
        public class FadeParam
        {
            public Color color = Color.black;
            [Range(0.1f, 2)]
            public float time = 0.5f;
        }

        public enum MODE
        {
            IN,
            WAIT,
            OUT,
            FF
        }
    }
}
