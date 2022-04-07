using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponManagerVR : MonoBehaviour
{
    [Header("RAY")]
    RaycastHit hit;

    [Header("Controller Check")]
    public bool isRightHandGrab = false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;

    [Header("Gun Shot")]
    public Transform Gun;
    public Transform weaponParent;
    public Transform muzzle;
    public Transform reloadcover;
    public float shotReloadTime;
    public Vector3 shotReloadPosition;
    public Vector3 shotDefaultPosition;
    public Transform[] muzzleFlash;
    public ParticleSystem particleSystem;
    public ParticleSystem shotTargetParticleSystem;
    
    [Header("Custom Controller Input")]
    public CustomControllerInput CCI;
    public GameObject triggerOBJ;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audio;








    void Start()
    {
        Gun = gameObject.transform;
    }


    void Update()
    {
        InputCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        
        PickEquipment(other);
    }

    void InputCheck()
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
            CCI.GetComponent<CustomControllerInput>().CurrentWeaponChange(Gun);
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.One))
        {
            gameObject.transform.parent = null;
            gameObject.transform.position = gameObject.transform.position;
            gameObject.transform.localRotation = gameObject.transform.localRotation;
            CCI.GetComponent<CustomControllerInput>().CurrentWeaponChange(null);
        }

        if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.F))  //PC개발용  총 집는 기능
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            CCI.GetComponent<CustomControllerInput>().CurrentWeaponChange(Gun);

        }
        else if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.G))     //총을 버리는 기능
        {
            gameObject.transform.parent = gameObject.transform;
            gameObject.transform.position = gameObject.transform.position;
            gameObject.transform.localRotation = gameObject.transform.localRotation;
            CCI.GetComponent<CustomControllerInput>().CurrentWeaponChange(null);
        }

    }

    public void Shoot()     //총을 쏘면 실행시키는 함수
    {
        //reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime).SetLoops(-1, LoopType.Yoyo);
        reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);
        reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);

        
        RayCreate();

        MuzzleFlash();
        shotTargetParticleSystem.transform.position = hit.point;
        shotTargetParticleSystem.transform.forward = hit.normal;
        shotTargetParticleSystem.Play();
        audioSource.PlayOneShot(audio[0]);


    }

    public void MuzzleFlash()
    {
        particleSystem.Play();
    }

    public void ActiveChange(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    void RayCreate()
    {
        Ray ray = new Ray();
        ray.origin = muzzle.transform.position;
        ray.direction = muzzle.transform.forward;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
        {
            print(hit.transform.gameObject.name + "이 총알에 맞았습니다.");
            

        }
        else if (hit.transform == null)
        {
            print("총알에 맞은 사물이 멀거나 없습니다... ");
        }

    }

    private void PickEquipment(string EquipmentName)
    {
        if(EquipmentName == gameObject.name)
        {
            if(CCI.currentWeapon==null)
            {
                CCI.currentWeapon = Gun;
                gameObject.transform.parent = weaponParent;
                gameObject.transform.position = weaponParent.position;
                gameObject.transform.rotation = weaponParent.rotation;
            }
        }
    }
}
