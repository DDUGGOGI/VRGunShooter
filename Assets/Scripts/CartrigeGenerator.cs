using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CartrigeGenerator : MonoBehaviourPunCallbacks
{
    public GameObject cartrigePrefab;
    public Transform Gen;



    public void Start()
    {
        photonView.RequestOwnership();
    }

    
    public void Update()
    {
        
        
            if (photonView.IsMine)
            {
                if (Input.GetMouseButton(0))
                {
                photonView.RPC("InstantiateCartrige", RpcTarget.AllBuffered);
               // photonView.RPC("InstantiateCartrige2", RpcTarget.AllBuffered);
            }
            }
        
    }


    [PunRPC]
     void InstantiateCartrige()
    {
        //Instantiate(cartrigePrefab, Gen.transform.position, Gen.transform.rotation);

        

        GameObject Cartrige = ObjectPoolManager.Instance.PopFromPool("Cartrige", Gen);
        Cartrige.transform.position = Gen.transform.position;
        Cartrige.transform.rotation = Gen.transform.rotation;
        Cartrige.SetActive(true);
        
    }
}
