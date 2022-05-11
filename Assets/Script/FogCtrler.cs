using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FogCtrler : MonoBehaviour
{
    [Range(0f,0.16f)]
    public float Fade; 
    public float maxDis = 10,sen = 0.1f;
    float cnt;
    [Space(10)]
    public FogData[] data;
    Color last;

    void Start()
    {
        Debug.Log("FOGSYS-"+gameObject.name);
        RenderSettings.fog = true;  
    }


    void Update()
    {
        cnt += Time.deltaTime;
        int e = 0;
        FogData fd = new FogData();
        foreach (var o in data)
        {
            var rr = Players.Player.position - o.pos;
            rr = new Vector3(Mathf.Abs(rr.x), Mathf.Abs(rr.y), Mathf.Abs(rr.z));
            var s = o.size / 2;
            if(rr.x < s.x && rr.y < s.y && rr.z < s.z )
            {
                e++; 
                fd.color += o.color; 
            }
        }

        fd.color /= e;

        if (e > 0)
        { 
            RenderSettings.fogColor = (fd.color * Fade + last * (1- Fade));
            RenderSettings.fogDensity = RenderSettings.fogColor.a / 100000 * sen;
            last = RenderSettings.fogColor; 
        }
    }


    private void OnDrawGizmos()
    {
        foreach (var i in data)
        {
            Gizmos.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            Gizmos.DrawWireSphere(i.pos, 0.3f);
            Gizmos.DrawWireCube(i.pos, i.size);
        }
    }

}


[System.Serializable]
public class FogData
{
    public string name;
    public Vector3 pos, size;
    public Color color; 
}
