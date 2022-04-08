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
        if (other.name == "magazineHole" && isMagazineAvailable==false)       //���� �Ű��� Ȧ�� Ʈ���� Ȯ��
        {
            audioSource.PlayOneShot(audio[0]);                  //Ŭ�� ����� ����
            gameObject.transform.parent.gameObject.transform.SetParent(other.transform);        //�Ű����� �θ� ���� �Ű��� Ȧ�� ����
            gameObject.transform.parent.gameObject.transform.localPosition = Vector3.zero;      //�Ű����� ������ ������ ����
            gameObject.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0,0,180);     //�Ű����� �Ѱ� �����̼� ��ġ
            currentInGun = gameObject.transform.parent.gameObject.transform;                    //�Ű����� �� Ȯ��


            gameObject.GetComponent<BoxCollider>().enabled = false;                                             //�Ű��� üũ Ʈ���� �۵� ����
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     //�Ű��� ��ü Ʈ���� �۵� ����
            isMagazineAvailable = true;         // �Ű��� ��� ����
            WMVR = other.transform.parent.gameObject.transform.parent.gameObject.transform;              //���� Ŭ���� ������
            WMVR.GetComponent<WeaponManagerVR>().currentMagazine = gameObject.transform;        //�ѿ��� �Ű��� Ŭ���� ����
            WMVR.GetComponent<WeaponManagerVR>().GunSlideForward();             // ������ ���� �Լ�
        }
    }

    public void MagazineThrowaway()
    {
        gameObject.transform.parent.gameObject.transform.DOLocalMove(ThrowPosition, ThrowTime);
        WMVR.GetComponent<WeaponManagerVR>().currentMagazine = null;
        isMagazineAvailable = false;                    // ��밡�� �Ұ�
        gameObject.transform.parent.gameObject.transform.SetParent(null);        //�Ű����� �θ� ���� �Ű��� Ȧ�� ����
        currentInGun = null;                //�Ű����� �� Ȯ��

        gameObject.GetComponent<BoxCollider>().enabled = true;                                             //�Ű��� üũ Ʈ���� �۵� ����
        gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;
        WMVR = null;
        
    }
}
