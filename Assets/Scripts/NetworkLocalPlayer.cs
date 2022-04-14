using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class NetworkLocalPlayer : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public GameObject ovrCamRig;
    public Transform leftHand;
    public Transform rightHand;
    public Camera centerCam;
    Vector3 pos;
    public float speed;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            Destroy(ovrCamRig);
        }
        else
        {
            // �÷��̾� ī�޶� ����
            if (centerCam.tag != "MainCamera")
            {
                centerCam.tag = "MainCamera";
                centerCam.enabled = true;
            }

            // �� ������ Ʈ��ŷ ����
            leftHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            rightHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);

            leftHand.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
            rightHand.localPosition = InputTracking.GetLocalPosition(Node.RightHand);

            // ���� ���̽�ƽ ������ƽ ��ġ ����
            Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (primaryAxis.y > 0f)
            {
                pos += (primaryAxis.y * transform.forward * Time.deltaTime * speed);
            }
            if (primaryAxis.y <0f)
            {
                pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime * speed);
            }
            if (primaryAxis.x > 0f)
            {
                pos += (primaryAxis.y * transform.right * Time.deltaTime * speed);
            }
            if (primaryAxis.x < 0f)
            {
                pos += (Mathf.Abs(primaryAxis.y) * -transform.right * Time.deltaTime * speed);
            }
            transform.position = pos;

            // ������ ���̽�ƽ ������ƽ ȸ�� ����
            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation=Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);
        }
    }
}
