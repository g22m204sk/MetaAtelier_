using UnityEngine;

public class Chara0_mover : MonoBehaviour
{
    Vector3 L;
    public Animator anim;
    public float Threshold;
    public float cnt,w;

    void Start() => L = transform.position; 

    void Update()
    {
        cnt -= Time.deltaTime;
        if (cnt > 0) return;

        Vector3 f = transform.position - L;
        w = Mathf.Abs(f.x) + Mathf.Abs(f.y) + Mathf.Abs(f.z);
        if (anim.GetBool("ISWALK"))
        {
            if (w <= Threshold)
            {
                anim.SetBool("ISWALK", false);
                cnt = 0.1f;
            }
        }
        else
        {
            if (w > Threshold)
            {
                anim.SetBool("ISWALK", true);
                cnt = 0.1f;
            }
            
        }

        L = transform.position;
    }
}
