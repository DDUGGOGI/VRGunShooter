using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cartrige : MonoBehaviourPunCallbacks
{
    Rigidbody myRigid;

    public string poolItemName = "Cartrige";
    public float moveSpeed = 30;
    public float lifeTime = 0.5f;

    public float _elapsedTime = 0f;
    private bool isInitialized = false;

    private void OnEnable()
    {
        if (isInitialized)
            isInitialized = true;
    }

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        myRigid.AddForce(gameObject.transform.right * Random.Range(3f, 5f), ForceMode.Impulse);
        myRigid.AddForce(gameObject.transform.up * Random.Range(1f, 2f), ForceMode.Impulse);
    }

    
    void Update()
    {
        
        
            transform.Rotate(new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), 5f));
            

        if (GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPoolManager.Instance.PushToPool("Cartrige", gameObject);
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
}
