using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("접속중...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 성공");
        
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;        // 최대 플레이어 인원
        roomOptions.IsVisible = true;       // 공개방 혹은 비공개방
        roomOptions.IsOpen = true;          // IsOpen이 false 라면 다른 플레이어가 방에 참가할 수 없다
        
        // 먼저 해당 이름의 방을 찾고 존재한다면 접근을 시도한다
        // 만약 해당 이름의 방이 존재하지 않는다면 roomOptions의 속성대로 새로 방을 만든다
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방에 참가합니다");
        
        base.OnJoinedRoom();
    }
    
    // Player -> Photon.Realtime 스페이스에 정의된 클래스
    /*
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("새로운 플레이어가 방에 참가하였습니다");
        base.OnPlayerEnteredRoom(newPlayer);
    }*/

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("새로운 플레이어가 방에 참가하였습니다");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
