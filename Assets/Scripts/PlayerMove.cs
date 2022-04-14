using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    //이동 속도
    public float speed = 5;
    //CC컴포넌트
    CharacterController cc;

    //중력 가속도의 크기
    public float gravity = -20;
    //수직 속도
    float yVelocity = 0;
    //점프 힘
    public float jumpPower = 2;

    public PhotonView PV;

    public GameObject cam;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (PV.IsMine)
        {
            cam.SetActive(true);
        }
    }


    void Update()
    {
      if (!PV.IsMine)
        {
            cam.SetActive(false);
            return;
        }


       PlayerArrowMove();
       PV.RPC("PlayerArrowMove", RpcTarget.AllBuffered);
        
        
    }

    [PunRPC]
    void PlayerArrowMove()
    {
        //사용자의 입력을 받는다.
        float h = ARAVRInput.GetAxis("Horizontal",ARAVRInput.Controller.LTouch);
        float v = ARAVRInput.GetAxis("Vertical", ARAVRInput.Controller.LTouch);
        //방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);
        //사용자가 보는 방향 dir
        dir = Camera.main.transform.TransformDirection(dir);
        //중력을 적용한 수직방향 추가
        yVelocity += gravity * Time.deltaTime;
        if(cc.isGrounded)
        {
            yVelocity = 0;
        }
        if(ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch)&&cc.isGrounded)
        {
            yVelocity = jumpPower;
        }

        if(Input.GetKeyDown(KeyCode.Space)&&cc.isGrounded)
        {
            yVelocity = jumpPower;
        }
        dir.y = yVelocity;

        //이동한다.
        cc.Move(dir * speed * Time.deltaTime);
    }    
}
