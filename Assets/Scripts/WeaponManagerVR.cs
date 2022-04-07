using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponManagerVR : MonoBehaviour
{

    public Transform weaponParent;

    public Transform Gun;
    public Transform reloadcover;
    public Vector3 shotReloadMove;
    public float shotReloadTime;

    public bool isRightHandGrab=false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;

    public CustomControllerInput CCI;
    public GameObject triggerOBJ;



    void Start()
    {
        CCI = GetComponent<CustomControllerInput>();
        Gun = gameObject.transform;
    }


    void Update()
    {
        RightHandGrab();
    }

    private void OnTriggerStay(Collider other)
    {
        
        PickEquipment(other);
    }

    void RightHandGrab()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))     //오른손 그립
        {
            isRightHandGrab = true;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))      //왼손 트리거
        {
            isLeftHandGrab = true;
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))   //오른손 트리거
        {
            isRightTrigger = true;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))     //왼손 트리거
        {
            isLefttTrigger = true;
        }
        isRightHandGrab = false;
        isLeftHandGrab = false;
        isRightTrigger = false;
        isLefttTrigger = false;
}

    public void PickEquipment(Collider other)
    {
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))   //오큘러스용
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = weaponParent.transform;
            gameObject.transform.position = weaponParent.position;
            gameObject.transform.localRotation = weaponParent.localRotation;
            //CCI.CurrentWeaponChange(Gun.name);
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.One))
        {
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            //CCI.CurrentWeaponChange(null);
        }

        if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.F))  //PC개발용 
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            //CCI.CurrentWeaponChange(Gun.name);

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.One))
        {
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            //CCI.CurrentWeaponChange(null);
        }

    }

    public void Shoot()
    {
        reloadcover.transform.DOLocalMove(shotReloadMove, shotReloadTime).SetLoops(1, LoopType.Yoyo);
    }

    private void PickEquipment(string EquipmentName)
    {
        if(EquipmentName == gameObject.name)
        {
            if(CCI.currentWeapon==null)
            {
                CCI.currentWeapon = Gun.name;
                gameObject.transform.parent = weaponParent;
                gameObject.transform.position = weaponParent.position;
                gameObject.transform.rotation = weaponParent.rotation;
            }
        }
    }
}
