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
    public Vector3 shotReloadPosition;
    public Vector3 shotDefaultPosition;
    public float shotReloadTime;

    public Transform reloadHammer;
    public Vector3 HammeReloadRosition;
    public Vector3 HammeDefaultRosition;
    public float HammerReloadTime;

    public ParticleSystem particleSystem;
    public ParticleSystem[] shotTargetParticleSystem;

    [Header("Custom Controller Input")]
    public Transform CCI;
    public GameObject triggerOBJ;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audio;

    [Header("Magazine")]
    public Transform currentMagazine;









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
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))   // 1. 오른손 그립 아이템 집기
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // 총의 콜라이더 켜 충돌방지
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // 총의 물리 중지
            

            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;                                                 // 트리거 중인 게임 오브젝트 표시
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // 손의 현재 무기를 최신화
            CCI = other.transform;
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);                                      // 픽업 사운드
            }
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))    // 2. 오른손 핸드 아이템 놓기
        {
            
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;      // 총의 콜라이더 꺼 물리실행
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))   // 3. 왼손 그립 아이템 집기
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))    // 4. 왼손 핸드 아이템 놓기
        {
            
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
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

    public void Shoot()     //총을 쏘면 실행시키는 함수
    {
        // 총알 나가기전 검열
        if (CCI.GetComponent<CustomControllerInput>().currentWeapon==gameObject.transform   && currentMagazine == gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0) )
        {
            //OVRInput.SetControllerVibration(0.00005f, 0.05f, OVRInput.Controller.RTouch);        //오큘러스 진동

            reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);      // 발사시 커버 움직임
            reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);

            reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);        // 헤머 움직임
            reloadHammer.transform.DOLocalRotate(HammeDefaultRosition, HammerReloadTime);

            RayCreate();        // 레이발사

            MuzzleFlash();      //머즐 섬광

            for (int i = 0; i < shotTargetParticleSystem.Length; i++)       // 피격 섬광
            {
                shotTargetParticleSystem[i].transform.position = hit.point;
                shotTargetParticleSystem[i].transform.forward = hit.normal;
                shotTargetParticleSystem[i].Play();
            }

            audioSource.PlayOneShot(audio[0]);

            if (hit.transform.gameObject.name== "SphereTarget")
            {
                hit.transform.gameObject.GetComponent<HItBall>().Hitball();
            }
        }

        // 매거진 없고 & 총알 없을시 밥줘모드
        else if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount ==0 || currentMagazine==null)      
        {
            reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);      // 발사시 커버 움직임
        }
    }

    public void GunSlideForward()       // 매거진에서 호출
    {
        reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);
    }


   

    public void MuzzleFlash()       //총구 섬광 함수!
    {
        particleSystem.Play();
    }

    void RayCreate()        //총의 레이케스트 발사!
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

}
