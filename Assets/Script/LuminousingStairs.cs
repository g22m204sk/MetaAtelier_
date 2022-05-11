using UnityEngine;

public class LuminousingStairs : MonoBehaviour
{ 
    private MeshRenderer r;
    public Vector3 offset = new Vector3(-1.9f, 0.4f, 0);
    public float cnt;
    Ray ray;
    readonly Color v = new Color(0.7f, 0.7f, 1f, 1f);
    Vector3 POS => transform.position + transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z;

    void Start()
    {
        r = GetComponent<MeshRenderer>();
        r.material.EnableKeyword("_EMISSION"); 
        ray = new Ray(POS, Vector3.up);
    }
     
    void Update()
    {
        if (Physics.Raycast(ray, 0.4f))
        {
            cnt += Time.deltaTime * 1.5f;
            if (cnt > 1) cnt = 1;
        }
        else
        { 
            cnt -= Time.deltaTime*0.5f;
            if (cnt < 0) cnt = 0;
        }  
        r.material.SetColor("_EmissionColor", v * cnt);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 1);
        Gizmos.DrawWireSphere(POS, 0.1f);
    }

}
