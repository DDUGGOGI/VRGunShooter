using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponManagerVR : MonoBehaviour
{
    [Header("Position")]
    public Vector3 defaultPosition;
    public Transform defaultParent;
    public Transform[] meshParent;
    public bool isUnlinitItem=false;

    [Header("Scale")]
    public Vector3 defaultScale;
    public Vector3 firstScale;

    [Header("RAY")]
    public RaycastHit hit;

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
    public Vector3 feedModePosition;
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

        if (isUnlinitItem == true)      //UnlimitItem ��� ���� ����
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // ���� �ݶ��̴� �� �浹����
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // ���� ���� ����
        }

    }


    void Update()
    {
        InputCheck();
        GetAnotherButton();
    }

    private void OnTriggerStay(Collider other)
    {
        
        PickEquipment(other);
        //gameObject.GetComponent<BoxCollider>().isTrigger = true;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (other.gameObject.tag=="Floor")
        {
            touchFloor();
        }
    }

    void InputCheck()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))     //������ �׸�
        {
            isRightHandGrab = true;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))      //�޼� Ʈ����
        {
            isLeftHandGrab = true;
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))   //������ Ʈ����
        {
            isRightTrigger = true;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))     //�޼� Ʈ����
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
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))   // 1. ������ �׸� ������ ����
        {
            
            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // ���� �ݶ��̴� �� �浹����
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // ���� ���� ����
            

            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;                                                 // Ʈ���� ���� ���� ������Ʈ ǥ��
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // ���� ���� ���⸦ �ֽ�ȭ
            CCI = other.transform;
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //������ �� �ʱ�ȭ
            //other.gameObject.transform.lossyScale = firstScale;
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);                                      // �Ⱦ� ����
            }

            
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))    // 2. ������ �ڵ� ������ ����
        {
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;      // ���� �ݶ��̴� �� ��������
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //other.gameObject.transform.DOScale(defaultScale, 0.1f);     //������ �� �ʱ�ȭ
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))   // 3. �޼� �׸� ������ ����
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
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //������ �� �ʱ�ȭ
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine")
            {
                audioSource.PlayOneShot(audio[2]);
            }

        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))    // 4. �޼� �ڵ� ������ ����
        {
            
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //other.gameObject.transform.DOScale(defaultScale, 0.1f);     //������ �� �ʱ�ȭ
        }
        
        







        else if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.F))  // 5. PCPCPCPCPC ���߿�  �� ���� ���
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
        else if (other.name == "GrabVolumeBigLeft" && Input.GetKeyDown(KeyCode.F))  // 6. PCPCPCPCPC ���߿�  �� ���� ���
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
        else if (other.name == "GrabVolumeBigRight" && Input.GetKeyDown(KeyCode.G))     // 7. PCPCPC�� �� ������ ���
        {
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
        }
        else if (other.name == "GrabVolumeBigLeft" && Input.GetKeyDown(KeyCode.F))  // 6. PCPCPCPCPC ���߿�  �� ���� ���
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

    public void GetAnotherButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))   // 5. ������ A ��ư �Է½�
        {
            print("źâ ���� !!");
            currentMagazine.GetComponent<MagazineTop>().MagazineThrowaway();        
            currentMagazine.GetComponent<MagazineTop>().MagazineColliderChange();       // �ް��� �ݶ��̴� ��ȣ�ۿ� ���ϵ��� �������µ��� ����
        }
    }


    public void Shoot()     //���� ��� �����Ű�� �Լ�
    {
        // ���� ��ġ Ȯ�� & �Ű��� ���� Ȯ��
        if (CCI.GetComponent<CustomControllerInput>().currentWeapon==gameObject.transform && currentMagazine != null)
        {
            

            if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount <1)
            {
                
                reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // �Ѿ� 0�Ͻ� ������
                audioSource.PlayOneShot(audio[2]);

                return;
            }
            

            reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);      // �߻�� Ŀ�� ������
            reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);

            reloadHammer.transform.DOLocalRotate(HammeDefaultRosition, HammerReloadTime);       // �ظӰ� ���� ħ
            reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);        // �ظ� �غ� ����
            

            currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount -= 1;        // �߻�� �Ű����� �Ѿ� ����
            audioSource.PlayOneShot(audio[0]);                          // �߻� ����

            RayCreate();        // ���̹߻�

            MuzzleFlash();      //���� ����

            for (int i = 0; i < shotTargetParticleSystem.Length; i++)       // �ǰ� ����
            {
                shotTargetParticleSystem[i].transform.position = hit.point;
                shotTargetParticleSystem[i].transform.forward = hit.normal;
                shotTargetParticleSystem[i].Play();
            }
            

            if (hit.transform.gameObject.name== "SphereTarget")
            {
                hit.transform.gameObject.GetComponent<HItBall>().Hitball();
            }
            

        }

        // �Ű��� ���� & �Ѿ� ������ ������
        else if (currentMagazine==null)      
        {
            print("������!!!");
            reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // �߻�� Ŀ�� ������
            reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // �ظӰ� �ڷ� �غ� ����
            audioSource.PlayOneShot(audio[2]);
        }
    }



    public void GunSlideForward()       // �Ű��� ���� �����̵� ����
    {
        reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);
        audioSource.PlayOneShot(audio[2]);
    }


   

    public void MuzzleFlash()       //�ѱ� ���� �Լ�!
    {
        particleSystem.Play();
    }

    void RayCreate()        //���� �����ɽ�Ʈ �߻�!
    {
        Ray ray = new Ray();
        ray.origin = muzzle.transform.position;
        ray.direction = muzzle.transform.forward;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
        {
            print(hit.transform.gameObject.name + "�� �Ѿ˿� �¾ҽ��ϴ�.");
        }
        else if (hit.transform == null)
        {
            print("�Ѿ˿� ���� �繰�� �ְų� �����ϴ�... ");
        }

        
    }

    void touchFloor()
    {
        if (isUnlinitItem == true)
        {
            print("�ٴ���ġ");

            //meshParent[0].GetComponent<MeshRenderer>().enabled=false;
            //meshParent[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
            gameObject.SetActive(false);


            gameObject.transform.position = defaultPosition;
            gameObject.transform.parent = defaultParent.transform;
            Invoke("disapearSecond", 0.5f);
        }
    }

    void disapearSecond()
    {
        //meshParent[0].GetComponent<MeshRenderer>().enabled = true;
        //meshParent[1].GetComponent<SkinnedMeshRenderer>().enabled = true;
        gameObject.SetActive(true);
    }

}
