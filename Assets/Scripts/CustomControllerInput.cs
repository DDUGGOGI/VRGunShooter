using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomControllerInput : MonoBehaviour
{
    public bool isRightHandGrab = false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;
    public WeaponManagerVR WM;

    public string currentWeapon;
    public GameObject triggerWeapon;

    void Start()
    {
        WM = GetComponent<WeaponManagerVR>();
    }
    

    void Update()
    {
        RightHandGrab();
    }

    void RightHandGrab()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            isRightHandGrab = !isRightHandGrab;
        }
        else if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            isLeftHandGrab = !isLeftHandGrab;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isRightTrigger = !isRightTrigger;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isLefttTrigger = !isLefttTrigger;
        }
    }

    public void CurrentWeaponChange(string name)
    {
        currentWeapon = name;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        triggerWeapon = other.gameObject;
        WM.PickEquipment(other);
    }
    */
    
}
