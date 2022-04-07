using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviourPunCallbacks
{
    public string playerPrefab;
    public Transform spawnPoint;
    public int myNum;
    public PhotonView  PlayerPV;

   

    void Start()
    {
        
            Spawn();

        /*
        for (int i = 0; i <4; i++)
        {

            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
            {

                myNum = i;
                print("myNum : " + i);
            }
        }
        */
    }

    
    void Update()
    {
        
        //ReSpawn();
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    [PunRPC]
    private void ReSpawn()
    {
    }
}
