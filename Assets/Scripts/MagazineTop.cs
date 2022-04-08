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
        if (other.name == "magazineHole" && isMagazineAvailable==false)       //���� �Ű��� Ȧ�� Ʈ���� Ȯ��
        {
            audioSource.PlayOneShot(audio[0]);                  //Ŭ�� ����� ����
            gameObject.transform.parent.gameObject.transform.SetParent(other.transform);        //�Ű����� �θ� ���� �Ű��� Ȧ�� ����
            gameObject.transform.parent.gameObject.transform.localPosition = Vector3.zero;      //�Ű����� ������ ������ ����
            currentInGun = gameObject.transform.parent.gameObject.transform;                    //�Ű����� �� Ȯ��


            gameObject.GetComponent<BoxCollider>().enabled = false;                                             //�Ű��� üũ Ʈ���� �۵� ����
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;     //�Ű��� ��ü Ʈ���� �۵� ����
            isMagazineAvailable = true;         // �Ű��� ��� ����
            WMVR = other.transform.parent.gameObject.transform.parent.gameObject.transform;              //���� Ŭ���� ������
            WMVR.GetComponent<WeaponManagerVR>().currentMagazine = gameObject.transform;        //�ѿ��� �Ű��� Ŭ���� ����
            WMVR.GetComponent<WeaponManagerVR>().GunSlideForward();             // ������ ���� �Լ�
        }
    }

    private void IsMagazineAvailable()
    {

    }
}
