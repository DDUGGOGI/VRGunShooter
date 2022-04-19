using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class InteractObJ : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    [Header("Control")]
    public bool isOpen = false;
    public bool isConnect = false;
    public bool isDisplay = true;

    [Header("Smooth Time")]
    public float smoothTime;

    [Header("Position")]
    public bool PositionMechine;
    public Vector3 defaultPosition;
    public Vector3 targetPosition;

    [Header("Rotation")]
    public bool RotationMechine;
    public Vector3 startRotation;
    public Vector3 defaultRotation;
    public Vector3 targetRotation;
    public Ease ease;


    


    //public Animation ani;

    //public string openMove;
    //public string closeMmove;



    void Start()
    {
        //ani = GetComponent<Animation>();
        transform.DOLocalRotate(startRotation, smoothTime).SetEase(ease);
    }


    void Update()
    {
        // ChangePositionCode();
        if (PositionMechine == true)
        {
            PV.RPC("ChangePositionLocal", RpcTarget.AllBuffered);
            //ChangePositionLocal();
        }
        else if (RotationMechine==true)
        {
            PV.RPC("ChangePositionLocal", RpcTarget.AllBuffered);
            //ChangeRotationLocal();
        }
        
    }

    public void ChangePosition()    //¿¡´Ï¸ÞÀÌ¼Ç ¹æ½Ä
    {
        if (isOpen==false)
        {
            /*
             ani.Play(openMove);
             Invoke("OnBool", 1f);

             print("¹® ¿­¸²");
             Vector3 vel = Vector3.zero;
             transform.position = Vector3.Lerp(this.transform.position, targetPosition, 0.1f);
             Invoke("offConnect", 1.1f);
             Invoke("OnBool", 1.2f);
             */
        }
        else if(isOpen ==true)
        {
            /*
           ani.Play(closeMmove);
            Invoke("OffBool", 1f);
            
            Vector3 vel = Vector3.zero;
            print("¹® ´ÝÈû");
            transform.position = Vector3.Lerp(this.transform.position, defaultPosition, 0.1f);
            Invoke("offConnect", 1.1f);
            Invoke("OffBool", 1.2f);
            */

        }
    }

    public void OnBool()
    {
        isOpen = true;
    }

    public void OffBool()
    {
        isOpen = false;
    }

    public void OnConnect()
    {
        isConnect = true;
    }

    void offConnect()
    {
        isConnect = false;
    }

    [PunRPC]
    public void ChangePositionLocal()
    {
        if (isOpen == false && isConnect == true)
        {
            print("¹® ¿­¸²");
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, smoothTime);
            Invoke("offConnect", 1.1f);
            Invoke("OnBool", 1.2f);

        }
        else if (isOpen == true && isConnect == true)
        {
            Vector3 vel = Vector3.zero;
            print("¹® ´ÝÈû");
            transform.position = Vector3.SmoothDamp(transform.position, defaultPosition, ref vel, smoothTime);
            Invoke("offConnect", 1.1f);
            Invoke("OffBool", 1.2f);
        }
    }

    [PunRPC]
    public void ChangeRotationLocal()
    {
        if (isOpen == false && isConnect == true)
        {
            print("¹® ¿­¸²");
            Vector3 vel = Vector3.zero;
            transform.DOLocalRotate(targetRotation, smoothTime).SetEase(ease);

            Invoke("offConnect", 1.1f);
            Invoke("OnBool", 1.2f);

        }
        else if (isOpen == true && isConnect == true)
        {
            Vector3 vel = Vector3.zero;
            print("¹® ´ÝÈû");
            transform.DOLocalRotate(defaultRotation, smoothTime).SetEase(ease);

            Invoke("offConnect", 1.1f);
            Invoke("OffBool", 1.2f);
        }
    }
}
