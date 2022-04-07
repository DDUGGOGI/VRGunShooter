using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Look : MonoBehaviourPunCallbacks
{
    public static bool cursorLocked = true;

    public float XlookSensitivity = 100;
    public float YlookSensitivity = 3;
    public float lookLimit;
    

    public Camera theCam;
    public Transform camParent;
    public GameObject theWeapon;

    Rigidbody myRigid;

    

    void Start()
    {
        if (!photonView.IsMine) camParent.gameObject.SetActive(false);

        myRigid = GetComponent<Rigidbody>();
        theCam.transform.rotation = Quaternion.identity;
    }

    
    void Update()
    {
        if (photonView.IsMine)
        {
            CameraXRotation();
            CharaterYRotation();

            cursorLock();
        }
    }

    private void CameraXRotation()
    {
        float mouseXrotation = Input.GetAxisRaw("Mouse Y") ;
        float Xrotation = mouseXrotation * XlookSensitivity * Time.deltaTime;
        Xrotation = Mathf.Clamp(Xrotation, -lookLimit, lookLimit);
        theCam.transform.localEulerAngles -= new Vector3(Xrotation,0,0);

        theWeapon.transform.rotation = theCam.transform.rotation;
    }

    

    private void CharaterYRotation()
    {
        float mouseYrotation = Input.GetAxisRaw("Mouse X")* YlookSensitivity * Time.deltaTime ;
        Quaternion rotationY = Quaternion.EulerAngles(0f, mouseYrotation,0f);
        myRigid.MoveRotation(myRigid.rotation *rotationY);
    }

    private void cursorLock()
    {
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
}
