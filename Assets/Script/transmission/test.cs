using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    InputField _field;
    
    // Start is called before the first frame update
    void Start()
    {
        _field = this.GetComponent<InputField>();
        _field.ActivateInputField(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
