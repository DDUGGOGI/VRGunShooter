using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInputManager : MonoBehaviour
{
    private XRIDefaultInputActions xrInput;
    
    void OnEnable()
    {
        xrInput.Enable();
    }

    void OnDisable()
    {
        xrInput.Disable();
    }
    
    void Awake()
    {
        xrInput = new XRIDefaultInputActions();
    }
}
