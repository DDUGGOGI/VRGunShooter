using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagazineManagerVR : MonoBehaviour
{

    [Header("Gun Shot")]
    public Transform Gun;
    public Transform weaponParent;
    public Transform muzzle;
    public Transform reloadcover;
    public float shotReloadTime;
    public Vector3 shotReloadPosition;
    public Vector3 shotDefaultPosition;
    public ParticleSystem particleSystem;
    public ParticleSystem[] shotTargetParticleSystem;
    
    [Header("Custom Controller Input")]
    public Transform CCI;
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
    }

    private void OnTriggerStay(Collider other)
    {
        
        PickEquipment(other);
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }


    public void PickEquipment(Collider other)
    {
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))   // 1. 오른손 그립 아이템 집기
        {
            CCI = other.transform;
            gameObject.GetComponent<Collider>().isTrigger = true;               // 총의 콜라이더 켜 충돌방지
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // 총의 물리 중지
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;                                                 // 트리거 중인 게임 오브젝트 표시
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // 손의 현재 무기를 최신화
            
            if (gameObject.name == "Pistol9" || gameObject.name == "magazine")
            {
                audioSource.PlayOneShot(audio[2]);                                      // 픽업 사운드
            }
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))    // 2. 오른손 핸드 아이템 놓기
        {
            gameObject.GetComponent<Collider>().isTrigger = false;      // 총의 콜라이더 꺼 물리실행
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))   // 3. 왼손 그립 아이템 집기
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            if (gameObject.name == "Pistol9" || gameObject.name == "magazine")
            {
                audioSource.PlayOneShot(audio[2]);
            }

        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))    // 4. 왼손 핸드 아이템 놓기
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
        }






        else if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.F))  // 5. PCPCPCPCPC 개발용  총 집는 기능
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);
            }
        }
        else if (other.name == "GrabVolumeBigLeft" && Input.GetKeyDown(KeyCode.F))  // 6. PCPCPCPCPC 개발용  총 집는 기능
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);
            }
        }
        else if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.G))     // 7. PCPCPC총 을 버리는 기능
        {
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
        }
        else if (other.name == "GrabVolumeBigLeft" && Input.GetKeyDown(KeyCode.F))  // 6. PCPCPCPCPC 개발용  총 집는 기능
        {
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);
            }
        }

    }

    

    public void ActiveChange(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

   

}
