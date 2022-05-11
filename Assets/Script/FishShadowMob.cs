using UnityEngine;

public class FishShadowMob : MonoBehaviour
{
    public FS_POINTER[] point;
    public float cnt;
    public int pos;

    private void Start()
    { 
        for (int v = 0; v < point.Length; v++)
            point[v].pos.y = transform.position.y;
        pos = 1;
        this.transform.localPosition = point[0].pos;
    }

    void Update()
    {
        cnt += Time.deltaTime;
        if (cnt >= point[pos].time)
        {
            cnt = 0;
            pos++;
            if (point.Length == pos) pos = 0;
        }
        int LP = pos == 0 ? point.Length - 1 : pos-1;
        transform.position = Vector3.Lerp(point[LP].pos, point[pos].pos,cnt/point[pos].time);
        transform.LookAt(point[pos].pos);
    }

    private void OnDrawGizmos()
    {
        if (point != null)
        { 
            Gizmos.color = Color.red;
            foreach (var i in point)
                Gizmos.DrawWireSphere(i.pos,0.4f); 
        }
    }
}

[System.Serializable]
public class FS_POINTER
{
    public Vector3 pos = new Vector3(-155.4753f, 34, 4);
    public float time;
}