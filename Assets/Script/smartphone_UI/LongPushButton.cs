using UnityEngine;

public class LongPushButton : MonoBehaviour
{
    public bool push = false; 
    public void PushDown() => push = true; 
    public void PushUp() => push = false;  
}
