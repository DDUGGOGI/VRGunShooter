using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Photon.Pun;

public class WeaponManagerVR : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    [Header("Position")]
    public Vector3 defaultPosition;
    public Vector3 defaultRotation;
    public Transform defaultParent;
    public Transform[] meshParent;
    public bool isUnlinitItem=false;

    [Header("Scale")]
    public Vector3 defaultScale;
    public Vector3 firstScale;

    [Header("RAY")]
    public RaycastHit hit;
    public RaycastHit RPChit;

    [Header("Controller Check")]
    public bool isRightHandGrab = false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;

    [Header("Gun Shot")]
    public Transform Gun;
    public Transform weaponParent;
    public Transform muzzle;
    public Transform rayOrigin;
    public Transform reloadcover;
    public Vector3 shotReloadPosition;
    public Vector3 shotDefaultPosition;
    public Vector3 feedModePosition;
    public float shotReloadTime;
    public bool isFeedmode=false;

    public Transform reloadHammer;
    public Vector3 HammeReloadRosition;
    public Vector3 HammeDefaultRosition;
    public Vector3 gunReloadRosition;
    public Vector3 gunDefaultRotation;


    public float HammerReloadTime;

    public ParticleSystem particleSystem;
    public ParticleSystem[] shotTargetParticleSystem;

    [Header("Custom Controller Input")]
    public Transform CCI;
    public Transform triggerOBJ;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audio;

    [Header("Magazine")]
    public Transform currentMagazine;









    void Start()
    {

        PV = GetComponent<PhotonView>();
        Gun = gameObject.transform;

        if (isUnlinitItem == true)      //UnlimitItem 경우 파지 상태
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // 총의 콜라이더 켜 충돌방지
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // 총의 물리 중지
        }

    }


    void Update()
    {
        InputCheck();

        GetAnotherButton();
        //PV.RPC("GetAnotherButton", RpcTarget.AllBuffered);
<<<<<<< HEAD

=======
>>>>>>> parent of 123c2fc (LightUp)
    }


    private void OnTriggerStay(Collider other)
    {
<<<<<<< HEAD
        otherCollider = other;
=======
        
>>>>>>> parent of 123c2fc (LightUp)
        PickEquipment(other);
        //PV.RPC("PickEquipment", RpcTarget.AllBuffered);

        //gameObject.GetComponent<BoxCollider>().isTrigger = true;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (other.gameObject.tag=="Floor")
        {
            touchFloor();
            //PV.RPC("touchFloor", RpcTarget.AllBuffered);
        }
    }
<<<<<<< HEAD

=======
>>>>>>> parent of 123c2fc (LightUp)


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
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))   // 1. 오른손 그립 아이템 집기
        {
            PV.RequestOwnership();
            print("오너쉽 확인");

            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // 총의 콜라이더 켜 충돌방지
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // 총의 물리 중지
            

            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject.transform;                                                 // 트리거 중인 게임 오브젝트 표시
            
            print("셋페어런츠 이전");
            //PV.RPC("RPCsetparent", RpcTarget.AllBuffered, gameObject.transform, other.gameObject.transform);
            gameObject.transform.parent = other.gameObject.transform;
            print("셋페어런츠 이후");
            //gameObject.transform.position = other.gameObject.transform.position;

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;

            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // 손의 현재 무기를 최신화
            CCI = other.transform;
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //스케일 값 초기화
            //other.gameObject.transform.lossyScale = firstScale;

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine" || gameObject.name == "Pistol9_magazineRight")
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
            //other.gameObject.transform.DOScale(defaultScale, 0.1f);     //스케일 값 초기화

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
            {
                audioSource.PlayOneShot(audio[2]);
            }
            
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))   // 3. 왼손 그립 아이템 집기
        {
            
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject.transform;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180f);  //  왼손 집으면 정자세
            
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //스케일 값 초기화

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
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
            gameObject.GetComponent<BoxCollider>().isTrigger = false;           // 콜라이더 실행
            gameObject.GetComponent<Rigidbody>().isKinematic = false;       // 물리 실행
            //other.gameObject.transform.DOScale(defaultScale, 0.1f);     //스케일 값 초기화

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
            {
                audioSource.PlayOneShot(audio[2]);
            }
            
        }
        
        






        /*
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
        */
    }


    public void GetAnotherButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))   // 5. 오른손 A 버튼 입력시
        {
            audioSource.PlayOneShot(audio[5]);                  // 클립 오디오 사운드
            print("탄창 해제 !!");
            currentMagazine.GetComponent<MagazineTop>().MagazineThrowaway();        
            currentMagazine.GetComponent<MagazineTop>().MagazineColliderChange();       // 메거진 콜라이더 상호작용 못하도록 내려오는동안 정지
        }
    }


    public void Shoot()     //총을 쏘면 실행시키는 함수
    {
        // 무기 일치 확인 & 매거진 장착 확인
        if (CCI.GetComponent<CustomControllerInput>().currentWeapon==gameObject.transform && currentMagazine != null)
        {

           
            if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount < 1)
            {

                if (isFeedmode == false)
                {
                    print("밥줘모드!!!");
                    reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // 발사시 커버 움직임
                    reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // 해머가 뒤로 준비 상태
                    audioSource.PlayOneShot(audio[3]);
                    isFeedmode = true;
                }
                else if (isFeedmode == true)
                {
                    audioSource.PlayOneShot(audio[2]);
                }

                return;
            }




            reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);      // 발사시 커버 움직임
            reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);

            reloadHammer.transform.DOLocalRotate(HammeDefaultRosition, HammerReloadTime);       // 해머가 공이 침
            reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);        // 해머 준비 상태
            

            currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount -= 1;        // 발사시 매거진의 총알 감소
            audioSource.PlayOneShot(audio[0]);                          // 발사 사운드

            RayCreate();        // 레이발사
            //PV.RPC("RPChitRefresh", RpcTarget.AllBuffered);
            //MuzzleFlash();      //머즐 섬광
            //PV.RPC("MuzzleFlash", RpcTarget.AllBuffered);

            

            RayHit();       // 총알에 맞은 오브젝트와 인터렉트

            CCI.DOLocalRotate(gunReloadRosition, HammerReloadTime);       // 반동
            CCI.DOLocalRotate(gunDefaultRotation, HammerReloadTime);        

            if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount < 1)
            {

                if (isFeedmode == false)
                {
                    print("밥줘모드!!!");
                    reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // 발사시 커버 움직임
                    reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // 해머가 뒤로 준비 상태
                    audioSource.PlayOneShot(audio[3]);
                    isFeedmode = true;
                }
                else if (isFeedmode == true)
                {
                    audioSource.PlayOneShot(audio[2]);
                }
                return;
            }


            
        }

        // 매거진 없고 & 총알 없을시 밥줘모드
        else if (currentMagazine==null)      
        {
            if (isFeedmode == false)
            {
                print("밥줘모드!!!");
                reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // 발사시 커버 움직임
                reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // 해머가 뒤로 준비 상태
                audioSource.PlayOneShot(audio[3]);
                isFeedmode = true;
            }
            else if (isFeedmode ==true)
            {
                audioSource.PlayOneShot(audio[2]);
            }
        }
    }


    [PunRPC]
    public void GunSlideForward()       // 매거진 들어가며 슬라이드 전진
    {
        reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);
        audioSource.PlayOneShot(audio[4]);
        isFeedmode = false;
    }



    [PunRPC]
    public void MuzzleFlash()       //총구 섬광 함수!
    {
        hit = RPChit;
        particleSystem.Play();

        for (int i = 0; i < shotTargetParticleSystem.Length; i++)       // 피격 섬광
        {
            shotTargetParticleSystem[i].transform.position = hit.point;
            shotTargetParticleSystem[i].transform.forward = hit.normal;
            shotTargetParticleSystem[i].Play();

            shotTargetParticleSystem[i].transform.position = RPChit.point;
            shotTargetParticleSystem[i].transform.forward = RPChit.normal;
            shotTargetParticleSystem[i].Play();
        }
    }

    void RayCreate()        //총의 레이케스트 발사!
    {
        Ray ray = new Ray();
        ray.origin = rayOrigin.transform.position;
        ray.direction = rayOrigin.transform.forward;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
        {
            print(hit.transform.gameObject.name + "이 총알에 맞았습니다.");
            
            PV.RPC("MuzzleFlash", RpcTarget.AllBuffered);
        }
        else if (hit.transform == null)
        {
            print("총알에 맞은 사물이 멀거나 없습니다... ");
        }
    }

    [PunRPC]
    void RPChitRefresh()
    {
        print("RPChit 최싱화 합수 진입");
        hit = RPChit;
        print("RPChit 최싱화 완료");
    }

    [PunRPC]
    void RayHit()
    {
        if (hit.transform.gameObject.name == "SphereTarget")
        {
            hit.transform.gameObject.GetComponent<HItBall>().Hitball();
        }
    }


    void touchFloor()
    {
        if (isUnlinitItem == true)
        {
            print("바닦터치");
            gameObject.transform.GetChild(2).gameObject.GetComponent<MagazineTop>().magazineCurrentBulletCount =
                gameObject.transform.GetChild(2).gameObject.GetComponent<MagazineTop>().magazineMaxBulletCount;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;           // 콜라이더 중지
            gameObject.GetComponent<Rigidbody>().isKinematic = true;       // 물리 중지

            meshParent[0].gameObject.SetActive(false);
            meshParent[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
            //gameObject.SetActive(false);
            Invoke("apearSecond", 0.5f);

            gameObject.transform.parent = defaultParent.transform;
            gameObject.transform.localPosition = defaultPosition;
            gameObject.transform.localRotation = Quaternion.Euler(defaultRotation);


            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1")
            {
                audioSource.PlayOneShot(audio[2]);
            }
            if (gameObject.name == "Pistol9_magazineRight")
            {
                audioSource.PlayOneShot(audio[4]);
            }
        }
    }

    [PunRPC]
    void apearSecond()
    {
        meshParent[0].gameObject.SetActive(true);
        //meshParent[0].gameObject.GetComponent<MeshRenderer>().enabled = true;
        meshParent[1].GetComponent<SkinnedMeshRenderer>().enabled = true;
        //gameObject.SetActive(true);
    }

}
