using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipItemScript : MonoBehaviourPunCallbacks
{

    void Start()
    {
        //equipItemPV.TransferOwnership(PhotonNetwork.PlayerList[theWeapon.myNum]);
        //equipItemPV.RequestOwnership();
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }



    }

   


    void Update()
    {
        //this.photonView.RequestOwnership();

    }

}
