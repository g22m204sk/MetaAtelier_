using UnityEngine;

public class SpinBIGP : MonoBehaviour
{
    public float spinSpeed = 2;
    void Update() => transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed * 360);
}