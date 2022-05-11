using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickUI_Manager : MonoBehaviour
{
    LongPushButton LL,LR,LT,LB, RL,RR,RT,RB;
    public static Vector3 joyL, joyR;
    public Vector3 ejoyL, ejoyR;

    void Start()
    {
        LL = transform.Find("L/L").GetComponent<LongPushButton>();
        LR = transform.Find("L/R").GetComponent<LongPushButton>();
        LT = transform.Find("L/T").GetComponent<LongPushButton>();
        LB = transform.Find("L/B").GetComponent<LongPushButton>();
        RL = transform.Find("R/L").GetComponent<LongPushButton>();
        RR = transform.Find("R/R").GetComponent<LongPushButton>();
        RT = transform.Find("R/T").GetComponent<LongPushButton>();
        RB = transform.Find("R/B").GetComponent<LongPushButton>(); 
    }

    void Update()
    {
        joyL.x = (LL.push ? 1 : 0) + (LR.push ? -1 : 0);
        joyL.y = (LT.push ? 1 : 0) + (LB.push ? -1 : 0);
        joyR.x = (RL.push ? 1 : 0) + (RR.push ? -1 : 0);
        joyR.y = (RT.push ? 1 : 0) + (RB.push ? -1 : 0);
        ejoyL = joyL;
        ejoyR = joyR; 
    }
}
