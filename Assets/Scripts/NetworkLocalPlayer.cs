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
    public Camera cam;
    public OVRCameraRig ovr;

    public Transform leftHand;
    public Transform rightHand;
    public Camera centerCam;
    Vector3 pos;
    public float speed;




    //CC������Ʈ
    public CharacterController cc;

    //�߷� ���ӵ��� ũ��
    public float gravity = -20;
    //���� �ӵ�
    float yVelocity = 0;
    //���� ��
    public float jumpPower = 2;

    public Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        pos = transform.position;
        if (PV.IsMine)
        {
            cc = GetComponent<CharacterController>();
            ovrCamRig.SetActive(true);
            cam.GetComponent<Camera>().enabled = true;
            ovr.GetComponent<OVRCameraRig>().enabled = true;
        }

    }

    void Update()
    {
        if (!PV.IsMine)
        {
            ovrCamRig.SetActive(false);
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

            PlayerArrowMove();      // CC������ ����
            //youtubesMove();

            // ������ ���̽�ƽ ������ƽ ȸ�� ����
            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation=Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);
        }
    }

    //Youtube������
    void youtubesMove()
    {
        // ���� ���̽�ƽ ������ƽ ��ġ ����
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (primaryAxis.y > 0f)
        {
            pos += (primaryAxis.y * transform.forward * Time.deltaTime * speed);
        }
        if (primaryAxis.y < 0f)
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
    }


    // CC��Ʈ�ѷ�
    void PlayerArrowMove()
    {
        //������� �Է��� �޴´�.
        float h = ARAVRInput.GetAxis("Horizontal", ARAVRInput.Controller.LTouch);
        float v = ARAVRInput.GetAxis("Vertical", ARAVRInput.Controller.LTouch);
        //������ �����.
        Vector3 dir = new Vector3(h, 0, v);
        //����ڰ� ���� ���� dir
        dir = Camera.main.transform.TransformDirection(dir);
        //�߷��� ������ �������� �߰�
        yVelocity += gravity * Time.deltaTime;
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }
        dir.y = yVelocity;

        //�̵��Ѵ�.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
