using UnityEngine;

public class flowersRotate : MonoBehaviour
{
    int pos, np;
    float cnt;
    public Vector3 Scale, OFF;
    float[] v = new float[] { 0, 1, 0, 0.4f, 0f, 1, 1, 0.5f, 0.2f, 1f, 0f, 1 };
     
    void Update()
    {
        cnt += Time.deltaTime * 0.5f;
        if (cnt >= v.Length) cnt -= v.Length;
        pos = (int)cnt;
        np = pos + 1 >= v.Length ? 0 : pos + 1; 
        transform.localRotation = Quaternion.Euler(((v[np] - v[pos]) * (cnt % 1f) + v[pos]) * Scale + OFF);  
    }
}
