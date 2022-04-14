using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Laptop : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public InteractObJ interacobj;

    public GameObject laptopBody;

    public List<Material> lapMat;

    public Color32 white= new Color32(191, 191, 191, 255);

    
    void Start()
    {
        interacobj = GetComponent<InteractObJ>();
    }


    void Update()
    {
        DisplayOff();
        //PV.RPC("DisplayOff", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DisplayOff()
    {
        if (interacobj.isOpen==true)
        {
            //gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            //laptopBody.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;

            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = lapMat[0];
            laptopBody.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = lapMat[1];
            laptopBody.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * 66f);

        }
        else if (interacobj.isOpen == false)
        {
            //gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            //laptopBody.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = lapMat[3];
            laptopBody.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = lapMat[2];
            laptopBody.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", white );
            
        }
        else
        {
            return;
        }
    }
}
