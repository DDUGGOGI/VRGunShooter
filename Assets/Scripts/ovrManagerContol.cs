using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ovrManagerContol : MonoBehaviour
{
    public PhotonView PV;

    public OVRCameraRig ovrCam;
    public OVRManager ovrMan;
    public OVRHeadsetEmulator ovrHE;



    void Start()
    {
        if (PV.IsMine)
        {
            ovrCam.GetComponent<OVRCameraRig>().enabled = true;
            ovrMan.GetComponent<OVRManager>().enabled = true;
            ovrHE.GetComponent<OVRHeadsetEmulator>().enabled = true;
        }
    }


}
