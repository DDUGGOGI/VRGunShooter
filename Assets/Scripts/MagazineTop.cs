using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagazineTop : MonoBehaviour
{
    public Vector3 magazineTargetPosition;
    public Transform WMVR;
    public Transform CCII;
    public Transform magazineHandAnchor;
    public Transform magazineParent;


    [Header("InteractGUN")]
    public Transform currentInGun;

    [Header("BulletCount")]
    public bool isMagazineAvailable=false;
    public int magazineMaxBulletCount;
    public int magazineCurrentBulletCount;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audio;

    [Header("Throw Away")]
    public Vector3 DefaultPosition;
    public Vector3 ThrowPosition;
    public float ThrowTime;

    float currentTime;
    

    void Start()
    {
        magazineCurrentBulletCount = magazineMaxBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        MagazineCurrentBulletAppear();
        //ResetMagazine();
        currentTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 총의 매거진 홀에 트리거 확인 + 불값 확인 + 매거진이 들려있는지 확인
        if (other.name == "magazineHole" && isMagazineAvailable==false && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))       
        {
            audioSource.PlayOneShot(audio[0]);                  // 클립 오디오 사운드
            gameObject.transform.parent.gameObject.transform.parent= other.transform;        // 매거진의 부모 총의 매거진 홀로 지정
            gameObject.transform.parent.gameObject.transform.localPosition =new Vector3(0,0,0);      // 매거진의 포지션 총으로 장착
            gameObject.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);     // 매거진이 총과 로테이션 일치
            magazineParent.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
            currentInGun = gameObject.transform.parent.gameObject.transform.parent.transform;                    // 매거진의 총 확인


            gameObject.GetComponent<BoxCollider>().enabled = false;                                             // 매거진 체크 트리거 작동 방지
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     // 매거진 몸체 트리거 작동 방지
            magazineParent.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     //총의 매거진홀 콜라이더 방지
            isMagazineAvailable = true;         // 매거진 장착 완료

            WMVR = other.transform.parent.gameObject.transform.parent.gameObject.transform;              // 총의 클래스 가져옴
            WMVR.GetComponent<WeaponManagerVR>().currentMagazine = gameObject.transform;        // 총에서 매거진 클래스 참조
            WMVR.GetComponent<WeaponManagerVR>().GunSlideForward();             // 밥줘모드 해제 함수
            gameObject.transform.parent.gameObject.transform.localPosition = Vector3.zero;      // 매거진의 포지션 총으로 장착 2번
        }
    }

    public void MagazineThrowaway()     // 매거진 해제
    {
        magazineParent.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;     //총의 매거진홀 콜라이더 사용가능
        //gameObject.transform.parent.gameObject.transform.DOLocalMove(ThrowPosition, ThrowTime);
        WMVR.GetComponent<WeaponManagerVR>().currentMagazine = null;
        isMagazineAvailable = false;                    // 매거진 장착 가능
        gameObject.transform.parent.gameObject.transform.SetParent(null);        // 매거진의 부모 해제
        currentInGun = null;                // 매거진의 총 확인

        gameObject.GetComponent<BoxCollider>().enabled = true;                                             // 건의 매거진 홀 감지 콜라이더 작동 가능
        gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;      //  매거진 잡기 가능
        magazineParent.GetComponent<Rigidbody>().isKinematic = false;   //탄창의 트리거와 물리 작용 허용
        magazineParent.GetComponent<BoxCollider>().isTrigger = false;

        WMVR = null;
        magazineParent.transform.localScale = new Vector3(0.56f, 0.56f, 0.56f);
        

    }

    public void MagazineColliderChange()
    {
        currentTime = 0;
        if (currentTime < 1)
        {
            magazineParent.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            magazineParent.gameObject.GetComponent<BoxCollider>().enabled = true ;
        }
    }

    public void MagazineCurrentBulletAppear()       // 매거진 상단 총알 보이기
    {
        if (magazineCurrentBulletCount >0)
        {
            gameObject.transform.parent.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
        else if (magazineCurrentBulletCount<1)
        {
            gameObject.transform.parent.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            return;
        }
    }

    public void ResetMagazine()         // 매거진에 총알 채우기
    {
        print("리셋 메거진 함수 실행 !");
        if (magazineParent.GetComponent<WeaponManagerVR>().CCI != null && magazineParent.GetComponent<WeaponManagerVR>().triggerOBJ != null && magazineCurrentBulletCount<magazineMaxBulletCount)
        {
            print("리셋 메거진 함수 실행! 메거진 손에 장착 확인 !");

            CCII = magazineParent.GetComponent<WeaponManagerVR>().CCI;
            print("CCII 완료 !");

            magazineHandAnchor = CCII.GetComponent<CustomControllerInput>().handAnchor;
            print("매거진 앵커 체크완료 !");

            if (magazineHandAnchor.localPosition.z < -0.12f)
            {
                print("탄창 리셋 !");
                magazineCurrentBulletCount = magazineMaxBulletCount;
                audioSource.PlayOneShot(audio[0]);                  // 클립 오디오 사운드
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
        
    }
}
