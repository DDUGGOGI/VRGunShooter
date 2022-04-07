using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawn : MonoBehaviourPunCallbacks
{
    public Transform spawnPoint;
    public GameObject obj;


    void Start()
    {
        PhotonNetwork.Instantiate("Mac-10_Item", transform.position, Quaternion.identity);
        //GameObject newGun = Instantiate( obj, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
