using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineTop : MonoBehaviour
{
    public Vector3 magazineTargetPosition;
    public Transform WMVR;

    [Header("InteractGUN")]
    public Transform currentInGun;

    [Header("BulletCount")]
    public bool isMagazineAvailable=false;
    public int magazineMaxCount;
    public int magazineCurrentBulletCount;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audio;

    void Start()
    {
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
            currentInGun = gameObject.transform.parent.gameObject.transform;                    //매거진의 총 확인


            gameObject.GetComponent<BoxCollider>().enabled = false;                                             //매거진 체크 트리거 작동 방지
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     //매거진 몸체 트리거 작동 방지
            isMagazineAvailable = true;         // 매거진 사용 가능
            WMVR = other.transform.parent.gameObject.transform.parent.gameObject.transform;              //총의 클래스 가져옴
            WMVR.GetComponent<WeaponManagerVR>().currentMagazine = gameObject.transform;        //총에서 매거진 클래스 참조
            WMVR.GetComponent<WeaponManagerVR>().GunSlideForward();             // 밥줘모드 해제 함수
        }
    }

    private void IsMagazineAvailable()
    {

    }
}
