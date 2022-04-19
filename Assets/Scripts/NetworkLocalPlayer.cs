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




    //CC컴포넌트
    public CharacterController cc;

    //중력 가속도의 크기
    public float gravity = -20;
    //수직 속도
    float yVelocity = 0;
    //점프 힘
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

            // 플레이어 카메라 관리
            if (centerCam.tag != "MainCamera")
            {
                centerCam.tag = "MainCamera";
                centerCam.enabled = true;
            }

            // 손 포지션 트레킹 관리
            leftHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            rightHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);

            leftHand.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
            rightHand.localPosition = InputTracking.GetLocalPosition(Node.RightHand);

            PlayerArrowMove();      // CC움직임 관리
            //youtubesMove();

            // 오른쪽 조이스틱 엄지스틱 회전 관리
            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation=Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);
        }
    }

    //Youtube움직임
    void youtubesMove()
    {
        // 왼쪽 조이스틱 엄지스틱 위치 관리
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


    // CC컨트롤러
    void PlayerArrowMove()
    {
        //사용자의 입력을 받는다.
        float h = ARAVRInput.GetAxis("Horizontal", ARAVRInput.Controller.LTouch);
        float v = ARAVRInput.GetAxis("Vertical", ARAVRInput.Controller.LTouch);
        //방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);
        //사용자가 보는 방향 dir
        dir = Camera.main.transform.TransformDirection(dir);
        //중력을 적용한 수직방향 추가
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

        //이동한다.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
