using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletMove : MonoBehaviourPunCallbacks
{
    Rigidbody myRigid;
    public string poolItemName = "SphereBullet";
    public float moveSpeed = 100;
    public float lifeTime = 3f;

    public float _elapsedTime = 0f;
    private bool isInitialized = false;

    

    private void OnEnable()
    {
        if (isInitialized)
            isInitialized = true;
    }

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        myRigid.velocity = transform.forward * moveSpeed * Time.deltaTime;


        if (GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPoolManager.Instance.PushToPool(poolItemName, gameObject);
        }
    }

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }

    void SetTimer()
    {
        _elapsedTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
