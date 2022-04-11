using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomControllerInput : MonoBehaviour
{
    public bool isRightHandGrab = false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;
    public Transform WM;

    public Transform currentWeapon;
    public Transform triggerWeapon;
    public Transform hand;

    public Transform handAnchor;

    void Start()
    {
        
        currentWeapon = hand;
    }
    

    void Update()
    {
        RightHandGrab();
        WeaponCheck();
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

    public void CurrentWeaponChange(Transform name)
    {
        currentWeapon = name;
    }
    
    private void OnTriggerStay(Collider other)
    {
        triggerWeapon = other.gameObject.transform;
        //WM=other.gameObject.GetComponent<WeaponManagerVR>();
        WM = other.transform;
        //WM.PickEquipment(other);

        if (other.transform.gameObject.GetComponent<InteractObJ>() != null)     // �繰���� ��ȣ�ۿ� 
        {
            print("�繰�� ���ͷ� Ŭ���� �۵�!");
            other.transform.gameObject.GetComponent<InteractObJ>().OnConnect();
        }
    }

    void WeaponCheck()
    {
        if (triggerWeapon==currentWeapon)
        {
            if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))        //�÷��̾� ���忡�� �߻�
            {
                print("�߻�!!");
                WM.GetComponent<WeaponManagerVR>().Shoot();     //�ѹ߻�
                
            }
            
            else if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
             {
                return;
            }
        }
        else
        {
            WM = null;
        }
    }
    
    
}
