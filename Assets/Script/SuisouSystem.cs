using UnityEngine;

public class SuisouSystem : MonoBehaviour
{ 
    RenderTexture rt;
    public Camera cam;
    public int pos, np;
    public float cnt;
    public Transform X, Z;
    public Vector3[] poss;
    public float distance;

    private void Start()
    {
        rt = cam.targetTexture;
    }

    void Update()
    {
        cnt += Time.deltaTime;
        if (cnt >= poss.Length)
            cnt -= poss.Length; 
        pos = (int)cnt;
        np = pos + 1 >= poss.Length ? 0 : pos+1;
        float a = cnt % 1f;
        X.localPosition = new Vector3(((poss[np].x - poss[pos].x)*a + poss[pos].x),X.localPosition.y, X.localPosition.z);
        Z.localPosition = new Vector3( Z.localPosition.x, Z.localPosition.y, ((poss[np].z - poss[pos].z) * a + poss[pos].z));
        cam.enabled = Vector3.Distance(cam.transform.position, Players.Player.position)< distance;
        cam.targetTexture = cam.enabled ? rt : null;
    }
}
 