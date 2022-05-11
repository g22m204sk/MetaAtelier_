using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscriptionHologram : MonoBehaviour
{
    public float ApproachThreshold;
    [Range(0.1f,20)]
    public float Speed = 1f;
    public Vector3 move;
    public bool IsLook;
     
    [Space(120)]
    public Transform child;
    public Vector3 offset, size;
    public float cnt, c;
    public bool IsEbl, update ;


    void Start()
    {
        child = transform.Find("child"); 
        offset = child.localPosition;
        size = child.localScale; 
    }
     

    void Update()
    {
        cnt -= Time.deltaTime;
        if (cnt <= 0)
        {
            bool e = IsEbl;
            float distance = Vector3.Distance(Players.Player.position, transform.position);
            IsEbl = (distance < ApproachThreshold);
            update = update || IsEbl != e;
            cnt = distance < ApproachThreshold ? 0.05f :  (distance < 15 ? (distance - ApproachThreshold)/20 : 1);
        }

        if (update)
        {
            c += Speed * (IsEbl ? Time.deltaTime : -Time.deltaTime);
            if (c <= 0 && !IsEbl || c >= 1 && IsEbl)
            {
                update = false;
                c = IsEbl ? 1 : 0;
            }
        }

        child.localPosition = offset + c * move;
        child.localScale = size * c * c;
        if (IsLook)
        {
            child.LookAt(new Vector3(Players.Player.position.x, child.position.y, Players.Player.position.z));
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ApproachThreshold);
    }
}
