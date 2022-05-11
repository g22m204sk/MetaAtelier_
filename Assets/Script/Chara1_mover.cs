using UnityEngine;

public class Chara1_mover : MonoBehaviour
{
    public Transform body, SideGear, Prope;
    [Range(0.0001f, 0.6f)]
    public float BorderSpeed;
    public float PANSPEED;
    Vector3 last;
    float BodyCnt;
   
    void Update()
    { 
        bool isMove = Mathf.Abs(last.x - transform.position.x) + Mathf.Abs(last.y - transform.position.y) + Mathf.Abs(last.z - transform.position.z) > BorderSpeed;
        BodyCnt += (isMove ? PANSPEED : -PANSPEED) *Time.deltaTime;
        if (BodyCnt > 1) BodyCnt = 1; else if (BodyCnt < 0) BodyCnt = 0; 
        Prope.localRotation *= Quaternion.Euler(Vector3.up * (isMove ? 12 : 4) * 360 * Time.deltaTime);
        SideGear.localRotation *= Quaternion.Euler(Vector3.left * (isMove ? 3 : 1) * 360 * Time.deltaTime);
        body.localRotation = Quaternion.Euler(0, -90,  ((1 - BodyCnt) * 26.401f));
        last = transform.position;
    }
}
 
