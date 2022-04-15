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
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))   // 1. ������ �׸� ������ ����
        {
            PV.RequestOwnership();
            print("���ʽ� Ȯ��");

            gameObject.GetComponent<BoxCollider>().isTrigger = true;               // ���� �ݶ��̴� �� �浹����
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // ���� ���� ����
            

            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject.transform;                                                 // Ʈ���� ���� ���� ������Ʈ ǥ��
            
            print("������ ����");
            //PV.RPC("RPCsetparent", RpcTarget.AllBuffered, gameObject.transform, other.gameObject.transform);
            gameObject.transform.parent = other.gameObject.transform;
            print("������ ����");
            //gameObject.transform.position = other.gameObject.transform.position;

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;

            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // ���� ���� ���⸦ �ֽ�ȭ
            CCI = other.transform;
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //������ �� �ʱ�ȭ
            //other.gameObject.transform.lossyScale = firstScale;

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine" || gameObject.name == "Pistol9_magazineRight")
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

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
            {
                audioSource.PlayOneShot(audio[2]);
            }
            
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))   // 3. �޼� �׸� ������ ����
        {
            
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject.transform;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180f);  //  �޼� ������ ���ڼ�
            
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;
            CCI = other.transform;
            //other.gameObject.transform.DOScale(firstScale, 0.1f);     //������ �� �ʱ�ȭ

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
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
            gameObject.GetComponent<BoxCollider>().isTrigger = false;           // �ݶ��̴� ����
            gameObject.GetComponent<Rigidbody>().isKinematic = false;       // ���� ����
            //other.gameObject.transform.DOScale(defaultScale, 0.1f);     //������ �� �ʱ�ȭ

            
            if (gameObject.name == "Pistol9" || gameObject.name == "Pistol9_magazine 1" || gameObject.name == "Pistol9_magazineRight")
            {
                audioSource.PlayOneShot(audio[2]);
            }
            
        }
        
        






        /*
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
        */
    }


    public void GetAnotherButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))   // 5. ������ A ��ư �Է½�
        {
            audioSource.PlayOneShot(audio[5]);                  // Ŭ�� ����� ����
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

           
            if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount < 1)
            {

                if (isFeedmode == false)
                {
                    print("������!!!");
                    reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // �߻�� Ŀ�� ������
                    reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // �ظӰ� �ڷ� �غ� ����
                    audioSource.PlayOneShot(audio[3]);
                    isFeedmode = true;
                }
                else if (isFeedmode == true)
                {
                    audioSource.PlayOneShot(audio[2]);
                }

                return;
            }




            reloadcover.transform.DOLocalMove(shotReloadPosition, shotReloadTime);      // �߻�� Ŀ�� ������
            reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);

            reloadHammer.transform.DOLocalRotate(HammeDefaultRosition, HammerReloadTime);       // �ظӰ� ���� ħ
            reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);        // �ظ� �غ� ����
            

            currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount -= 1;        // �߻�� �Ű����� �Ѿ� ����
            audioSource.PlayOneShot(audio[0]);                          // �߻� ����

            RayCreate();        // ���̹߻�
            //PV.RPC("RPChitRefresh", RpcTarget.AllBuffered);
            //MuzzleFlash();      //���� ����
            //PV.RPC("MuzzleFlash", RpcTarget.AllBuffered);

            

            RayHit();       // �Ѿ˿� ���� ������Ʈ�� ���ͷ�Ʈ

            CCI.DOLocalRotate(gunReloadRosition, HammerReloadTime);       // �ݵ�
            CCI.DOLocalRotate(gunDefaultRotation, HammerReloadTime);        

            if (currentMagazine.GetComponent<MagazineTop>().magazineCurrentBulletCount < 1)
            {

                if (isFeedmode == false)
                {
                    print("������!!!");
                    reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // �߻�� Ŀ�� ������
                    reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // �ظӰ� �ڷ� �غ� ����
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

        // �Ű��� ���� & �Ѿ� ������ ������
        else if (currentMagazine==null)      
        {
            if (isFeedmode == false)
            {
                print("������!!!");
                reloadcover.transform.DOLocalMove(feedModePosition, shotReloadTime);      // �߻�� Ŀ�� ������
                reloadHammer.transform.DOLocalRotate(HammeReloadRosition, HammerReloadTime);    // �ظӰ� �ڷ� �غ� ����
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
    public void GunSlideForward()       // �Ű��� ���� �����̵� ����
    {
        reloadcover.transform.DOLocalMove(shotDefaultPosition, shotReloadTime);
        audioSource.PlayOneShot(audio[4]);
        isFeedmode = false;
    }



    [PunRPC]
    public void MuzzleFlash()       //�ѱ� ���� �Լ�!
    {
        hit = RPChit;
        particleSystem.Play();

        for (int i = 0; i < shotTargetParticleSystem.Length; i++)       // �ǰ� ����
        {
            shotTargetParticleSystem[i].transform.position = hit.point;
            shotTargetParticleSystem[i].transform.forward = hit.normal;
            shotTargetParticleSystem[i].Play();

            shotTargetParticleSystem[i].transform.position = RPChit.point;
            shotTargetParticleSystem[i].transform.forward = RPChit.normal;
            shotTargetParticleSystem[i].Play();
        }
    }

    void RayCreate()        //���� �����ɽ�Ʈ �߻�!
    {
        Ray ray = new Ray();
        ray.origin = rayOrigin.transform.position;
        ray.direction = rayOrigin.transform.forward;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
        {
            print(hit.transform.gameObject.name + "�� �Ѿ˿� �¾ҽ��ϴ�.");
            
            PV.RPC("MuzzleFlash", RpcTarget.AllBuffered);
        }
        else if (hit.transform == null)
        {
            print("�Ѿ˿� ���� �繰�� �ְų� �����ϴ�... ");
        }
    }

    [PunRPC]
    void RPChitRefresh()
    {
        print("RPChit �ֽ�ȭ �ռ� ����");
        hit = RPChit;
        print("RPChit �ֽ�ȭ �Ϸ�");
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
            print("�ٴ���ġ");
            gameObject.transform.GetChild(2).gameObject.GetComponent<MagazineTop>().magazineCurrentBulletCount =
                gameObject.transform.GetChild(2).gameObject.GetComponent<MagazineTop>().magazineMaxBulletCount;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;           // �ݶ��̴� ����
            gameObject.GetComponent<Rigidbody>().isKinematic = true;       // ���� ����

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
