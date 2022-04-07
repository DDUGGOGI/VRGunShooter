using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OwnershipTransfer : MonoBehaviourPun, IPunOwnershipCallbacks
{
    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {

        if (targetView != base.photonView)
            return;

        base.photonView.TransferOwnership(requestingPlayer);


    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        if (targetView != base.photonView)
            return;
    }


    public void OnOwnershipTransferFailed(PhotonView targetView, Photon.Realtime.Player senderOfFailedRequest)
    {
    }

    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void OnMouseDown()
    {
        base.photonView.RequestOwnership();

    }

}
