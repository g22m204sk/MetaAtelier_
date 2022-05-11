using UnityEngine;
public class AutoReturnStage : MonoBehaviour
{ 
    void FixedUpdate()
    {
        var i = transform.position;
        if
        (
            -1 > i.y ||//フツーに床から抜け落ちた場合
            (-0.71 < i.y && i.y < 7 && (i.x > 24.64f || i.x < -14.5f || i.z > 30 || i.z < -45.7)) ||//1Fフロア横からすり抜けた場合
            (i.y > 30 && (Mathf.Abs(i.x) > 208 || Mathf.Abs(i.z) > 193))//2F&3F横から抜け防止
        )
        {
            transform.position = new Vector3(17.17f, 1, -11.1f);
        }
    }
}
