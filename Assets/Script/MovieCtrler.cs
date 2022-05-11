using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCtrler : MonoBehaviour
{
    public MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        mr.materials[0].SetTexture("_EmissionMap",mr.materials[0].GetTexture("_MainTex")) ;
    }
}
