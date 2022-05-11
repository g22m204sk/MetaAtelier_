using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    void FixedUpdate() => light.enabled = false;
    void OnTriggerStay(Collider c) { if (c.transform.GetComponent<PlayerMove>() != null) light.enabled = true; }
}
