using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagazineTop : MonoBehaviour
{
    public Vector3 magazineTargetPosition;
    public Transform WMVR;


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
    

    void Start()
    {
        magazineCurrentBulletCount = magazineMaxBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "magazineHole" && isMagazineAvailable==false)       //총의 매거진 홀에 트리거 확인
        {
            audioSource.PlayOneShot(audio[0]);                  //클립 오디오 사운드
            gameObject.transform.parent.gameObject.transform.SetParent(other.transform);        //매거진의 부모 총의 매거진 홀로 지정
            gameObject.transform.parent.gameObject.transform.localPosition = Vector3.zero;      //매거진의 포지션 총으로 장착
            gameObject.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0,0,180);     //매거진이 총과 로테이션 일치
            currentInGun = gameObject.transform.parent.gameObject.transform;                    //매거진의 총 확인


            gameObject.GetComponent<BoxCollider>().enabled = false;                                             //매거진 체크 트리거 작동 방지
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     //매거진 몸체 트리거 작동 방지
            isMagazineAvailable = true;         // 매거진 사용 가능
            WMVR = other.transform.parent.gameObject.transform.parent.gameObject.transform;              //총의 클래스 가져옴
            WMVR.GetComponent<WeaponManagerVR>().currentMagazine = gameObject.transform;        //총에서 매거진 클래스 참조
            WMVR.GetComponent<WeaponManagerVR>().GunSlideForward();             // 밥줘모드 해제 함수
        }
    }

    public void MagazineThrowaway()
    {
        gameObject.transform.parent.gameObject.transform.DOLocalMove(ThrowPosition, ThrowTime);
        WMVR.GetComponent<WeaponManagerVR>().currentMagazine = null;
        isMagazineAvailable = false;                    // 사용가능 불값
        gameObject.transform.parent.gameObject.transform.SetParent(null);        //매거진의 부모 총의 매거진 홀로 지정
        currentInGun = null;                //매거진의 총 확인

        gameObject.GetComponent<BoxCollider>().enabled = true;                                             //매거진 체크 트리거 작동 방지
        gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;
        WMVR = null;
        
    }
}
