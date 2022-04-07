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
        
        if (other.name == "GrabVolumeBigRight" && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))   // 1. ������ �׸� ������ ����
        {
            CCI = other.transform;
            gameObject.GetComponent<Collider>().isTrigger = true;               // ���� �ݶ��̴� �� �浹����
            gameObject.GetComponent<Rigidbody>().isKinematic = true;        // ���� ���� ����
            //PickEquipment(pickItemName);
            triggerOBJ = other.gameObject;                                                 // Ʈ���� ���� ���� ������Ʈ ǥ��
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.localRotation = other.gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = gameObject.transform;       // ���� ���� ���⸦ �ֽ�ȭ
            
            if (gameObject.name == "Pistol9" || gameObject.name == "magazine")
            {
                audioSource.PlayOneShot(audio[2]);                                      // �Ⱦ� ����
            }
        }
        else if (other.name == "GrabVolumeBigRight" && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))    // 2. ������ �ڵ� ������ ����
        {
            gameObject.GetComponent<Collider>().isTrigger = false;      // ���� �ݶ��̴� �� ��������
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
        }
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))   // 3. �޼� �׸� ������ ����
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
        else if (other.name == "GrabVolumeBigLeft" && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))    // 4. �޼� �ڵ� ������ ����
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent = null;
            //gameObject.transform.position = gameObject.transform.position;
            //gameObject.transform.localRotation = gameObject.transform.localRotation;
            other.GetComponent<CustomControllerInput>().currentWeapon = null;
            CCI = null;
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

    

    public void ActiveChange(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

   

}
