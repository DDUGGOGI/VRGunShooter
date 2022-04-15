using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CustomControllerInput : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public bool isRightHandGrab = false;
    public bool isLeftHandGrab = false;
    public bool isRightTrigger = false;
    public bool isLefttTrigger = false;
    public Transform WM;

    public Transform currentWeapon;
    public Transform triggerWeapon;
    public Transform hand;
    public Transform rightHandMesh;
    public Transform leftHandMesh;

    public Transform handAnchor;

    void Start()
    {
        
        currentWeapon = hand;
    }
    

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        else
        {
            
            RightHandGrab();
            
            //RighthandDisapearHandmesh();
            //LefthandDisapearHandmesh();
            
            WeaponCheck();

            //PV.RPC("RightHandGrab", RpcTarget.AllBuffered);
            //PV.RPC("WeaponCheck", RpcTarget.AllBuffered);
            //PV.RPC("RighthandDisapearHandmesh", RpcTarget.AllBuffered);
            //PV.RPC("LefthandDisapearHandmesh", RpcTarget.AllBuffered);
            
        }

        
    }


    void RightHandGrab()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            isRightHandGrab = !isRightHandGrab;
        }
        else if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            isLeftHandGrab = !isLeftHandGrab;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isRightTrigger = !isRightTrigger;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isLefttTrigger = !isLefttTrigger;
        }
    }

    public void CurrentWeaponChange(Transform name)
    {
        currentWeapon = name;
    }

    
    private void OnTriggerStay(Collider other)
    {
        triggerWeapon = other.gameObject.transform;
        //WM=other.gameObject.GetComponent<WeaponManagerVR>();
        WM = other.transform;
        //WM.PickEquipment(other);

        if (other.transform.gameObject.GetComponent<InteractObJ>() != null)     // 사물과의 상호작용 
        {
            print("사물의 인터렉 클래스 작동!");
            other.transform.gameObject.GetComponent<InteractObJ>().OnConnect();
        }
    }

    

    
    
    void WeaponCheck()
    {
        if (triggerWeapon==currentWeapon)
        {
            if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))        //플레이어 입장에서 발사
            {
                print("발사!!");
                WM.GetComponent<WeaponManagerVR>().Shoot();     //총발사
                
            }
            
            else if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
             {
                return;
            }
        }
        else
        {
            WM = null;
        }
    }

    
    void RighthandDisapearHandmesh()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            PV.RPC("RPCrdh1", RpcTarget.AllBuffered);
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            PV.RPC("RPCrdh2", RpcTarget.AllBuffered);
        }

    }
    void RPCrdh1()
    {
        rightHandMesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    void RPCrdh2()
    {
        rightHandMesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    
    void LefthandDisapearHandmesh()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            PV.RPC("RPCldh1", RpcTarget.AllBuffered);
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            PV.RPC("RPCldh2", RpcTarget.AllBuffered);
        }

    }
    void RPCldh1()
    {
        leftHandMesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
    void RPCldh2()
    {
        leftHandMesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }




}
