using UnityEngine;
public class AutoReturnStage : MonoBehaviour
{ 
    void FixedUpdate()
    {
        var i = transform.position;
        if
        (
            -1 > i.y ||//�t�c�[�ɏ����甲���������ꍇ
            (-0.71 < i.y && i.y < 7 && (i.x > 24.64f || i.x < -14.5f || i.z > 30 || i.z < -45.7)) ||//1F�t���A�����炷�蔲�����ꍇ
            (i.y > 30 && (Mathf.Abs(i.x) > 208 || Mathf.Abs(i.z) > 193))//2F&3F�����甲���h�~
        )
        {
            transform.position = new Vector3(17.17f, 1, -11.1f);
        }
    }
}
