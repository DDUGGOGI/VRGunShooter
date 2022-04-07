using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemScript : MonoBehaviourPunCallbacks
{
    public string poolItemName = "Mac-10(Clone) (UnityEngine.GameObject)";
    public PhotonView PV;
    Weapon theWeapon;

    void Start()
    {
        theWeapon = GetComponent<Weapon>();
    }

    
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.layer == 7)
        {
            if(Input.GetKeyDown(KeyCode.F) ||  OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
