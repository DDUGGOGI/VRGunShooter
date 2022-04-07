using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{

    public int myNum;
    Manager theManager;
    public PhotonView PV;
    public Transform bulletPosition;

    public Gun[] theGun;
    public Transform weaponParent;
    public GameObject currentWeapon;
    public int currentItemIndex;
    public GameObject newGun;

    public Gun VECTOR;
    public Gun  AK;
    public Gun MAC;
    public Gun gun4;
    
    public EquipItemScript theEquipItem;

    public string bulletName = "SphereBullet";
    public string ItemName = "";
    public Camera theCam;

    public GameObject rightHand;
    public GameObject leftHand;




    void Start()
    {
        theEquipItem = GetComponent<EquipItemScript>();
    }

    void Update()       //UPDATE !!!
    {
        if (PV.IsMine)
        {
            PV.RPC("test", RpcTarget.AllBuffered);
            //test();
            //KeyEquip();
            photonView.RPC("KeyEquip", RpcTarget.AllBuffered);
            PV.RPC("ThrowItem", RpcTarget.AllBuffered, currentItemIndex);

            if (Input.GetMouseButton(0) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))  //발사!
            {
                PV.RPC("Shoot", RpcTarget.AllBuffered);
                ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);
                //photonView.RPC("InstantiateCartrige", RpcTarget.AllBuffered);

            }
        }
    }
    
    [PunRPC]
    private void KeyEquip()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItemIndex = 0;
            PV.RPC("Equip", RpcTarget.AllBuffered, 0);
            //Equip(currentItemIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItemIndex = 1;
            PV.RPC("Equip", RpcTarget.AllBuffered, 1);
            //cartrigeGen = GameObject.FindWithTag("Cartrige");
            //Equip(currentItemIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItemIndex = 2;
            PV.RPC("Equip", RpcTarget.AllBuffered, 2);
            //Equip(currentItemIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentItemIndex = 3;
            PV.RPC("Equip", RpcTarget.AllBuffered, 3);
            //Equip(currentItemIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentItemIndex = 4;
            PV.RPC("Equip", RpcTarget.AllBuffered, 4);
            //Equip(currentItemIndex);

        }
    }

    [PunRPC]
    void test()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentItemIndex = 0;
            PV.RPC("GetVECTOR", RpcTarget.AllBuffered, currentItemIndex);
            ItemName = theGun[currentItemIndex].gunName;
            


            newGun = PhotonNetwork.Instantiate(ItemName, weaponParent.position, weaponParent.rotation);
        }
    }
    

    [PunRPC]     //==================================================아이템 장착====================!!!
    private void Equip(int gunIndex)   
    {
        currentItemIndex = gunIndex;

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
        
        if (theGun[gunIndex] == null)
        {
            return;
        }

        //GameObject newGun = ObjectPoolManager.Instance.PopFromPool(theGun[currentGunIndex].gunName, weaponParent);
        newGun = Instantiate(theGun[gunIndex].gunPrefab, weaponParent.transform.position, weaponParent.transform.rotation, weaponParent) as GameObject;
        //newGun.gameObject.SetActive(true);
        //newGun =  PhotonNetwork.Instantiate(theGun[currentItemIndex].gunName, transform.position, transform.rotation);
        //newGun.transform.parent = weaponParent;
        //GameObject newGun = PhotonNetwork.Instantiate(theGun[gunIndex].gunName, weaponParent.transform.position, weaponParent.transform.rotation,0);
        //photonView.RPC("parent", RpcTarget.All);
        //newGun.transform.localPosition = Vector3.zero;
        //newGun.transform.localEulerAngles = Vector3.zero;

        currentWeapon = newGun;
        //ItemName = theGun[currentGunIndex].gunName;
        //newGun.GetPhotonView().RequestOwnership();

        PV.RPC("parent", RpcTarget.AllBuffered);


    }
    /*
    [PunRPC]
    public void SpawnGun()
    {
        newGun = Instantiate(theGun[currentItemIndex].gunPrefab, weaponParent.transform.position, weaponParent.transform.rotation, weaponParent) as GameObject;
        PhotonView photonView = newGun.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[]
            {
            newGun.transform.position, newGun.transform.rotation, photonView.ViewID
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode, data, raiseEventOptions, sendOptions);
        }
        else
        {
            Debug.LogError("Failed to allocate a ViewId.");

            Destroy(newGun);
        }
    }
    [PunRPC]
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CustomManualInstantiationEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;

            GameObject player = (GameObject)Instantiate(PlayerPrefab, (Vector3)data[0], (Quaternion)data[1]);
            PhotonView photonView = player.GetComponent<PhotonView>();
            photonView.ViewID = (int)data[2];
        }
    }
    */
    [PunRPC]
    void parent()
    {
        //newGun.transform.parent = weaponParent;
        newGun.transform.localPosition = Vector3.zero;
        newGun.transform.localEulerAngles = Vector3.zero;
        //currentWeapon = newGun;
        ItemName = theGun[currentItemIndex].gunName;
        //newGun.GetPhotonView().RequestOwnership();
    }
    [PunRPC]    //아이템 앞으로 던지기
    private void ThrowItem(int gunIndex)       
    {
        if (Input.GetKeyDown(KeyCode.G) && currentWeapon != null)
        {
            //Destroy(currentWeapon);

            //GameObject throwGun = Instantiate(theGun[currentGunIndex].gunPrefab, weaponParent.position, weaponParent.rotation) as GameObject;
            //PhotonNetwork.Instantiate("currentWeapon", weaponParent.position, weaponParent.rotation);
            //photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);

            photonView.RPC("SetActiveFalse", RpcTarget.AllBuffered);
            currentItemIndex = gunIndex;

            //GameObject throwGun = Instantiate(theGun[currentGunIndex].gunPrefab, weaponParent.transform.position, weaponParent.transform.rotation) as GameObject;
            PhotonNetwork.Instantiate(ItemName+"_Item", transform.position, weaponParent.transform.rotation);

            PV.RPC("setoffGun", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    void setoffGun()
    {
        theGun[currentItemIndex] = null;
        Destroy(currentWeapon);
        currentWeapon = null;
        ItemName = null;
    }
    

    [PunRPC]        //프리펩 방식 총알발사
    void Shoot()    
    {
        if(currentWeapon != null)
        {
            GameObject Bullet = ObjectPoolManager.Instance.PopFromPool(bulletName, weaponParent);
            Bullet.transform.position = bulletPosition.position + transform.forward;
            Bullet.transform.rotation = theCam.transform.rotation;
            Bullet.SetActive(true);
        }
    }



    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "vectorGun_Item" || other.gameObject.name == "vectorGun_Item(Clone)")
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && theGun[currentItemIndex] == null)
            {
                PV.RPC("GetVECTOR", RpcTarget.AllBuffered, currentItemIndex);
                PV.RPC("Equip", RpcTarget.AllBuffered, currentItemIndex);
            }
        }

        if (other.gameObject.name == "Ak-47_Item" || other.gameObject.name == "Ak-47_Item(Clone)")
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && theGun[currentItemIndex] == null && newGun == null)
            {
                PV.RPC("GetAK", RpcTarget.AllBuffered, currentItemIndex);
                PV.RPC("Equip", RpcTarget.AllBuffered, currentItemIndex);
            }
        }

        if(other.gameObject.name == "Mac-10_Item" || other.gameObject.name == "Mac-10_Item(Clone)" || other.gameObject.name == "Mac-10(Clone) (UnityEngine.GameObject)")
        {
            if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && theGun[currentItemIndex] == null)
            {
                PV.RPC("GetMAC10", RpcTarget.AllBuffered, currentItemIndex);
                PV.RPC("Equip", RpcTarget.AllBuffered, currentItemIndex);
            }
        }

        /*
        if (other.gameObject.tag == "Weapon")
        {
            photonView.RequestOwnership();
        }
        */
    }

    [PunRPC]
    private Gun GetVECTOR(int gunIndex)
    {
        return theGun[gunIndex] = VECTOR;
    }
    [PunRPC]
    private Gun GetAK(int gunIndex)
    {
        return theGun[gunIndex] = AK;
    }
    [PunRPC]
    private Gun GetMAC10(int gunIndex)
    {
        return theGun[gunIndex] = MAC;
    }

    [PunRPC]
    void SetActiveFalse()
    {
        currentWeapon.gameObject.SetActive(false);
    }


    //=======================================================================
    /*

   [PunRPC]
   void CurrentGunSetActiveTrue()
   {
       currentGun.SetActive(true);
       currentWeapon = currentGun;
   }
   */
}
