using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    
    public void Awake()
    {

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();           //1
    }
    /*
    private void Update()
    {
        print(PhotonNetwork.NetworkClientState.ToString());
    }
    */

    public override void OnConnectedToMaster()      //2
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        print("서버접속완료  !!!");
        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
        base.OnJoinedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("");
        base.OnJoinRoomFailed(returnCode, "Room Join Fail");
    }
}
