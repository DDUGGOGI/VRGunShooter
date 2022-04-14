using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    //�̵� �ӵ�
    public float speed = 5;
    //CC������Ʈ
    CharacterController cc;

    //�߷� ���ӵ��� ũ��
    public float gravity = -20;
    //���� �ӵ�
    float yVelocity = 0;
    //���� ��
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
        //������� �Է��� �޴´�.
        float h = ARAVRInput.GetAxis("Horizontal",ARAVRInput.Controller.LTouch);
        float v = ARAVRInput.GetAxis("Vertical", ARAVRInput.Controller.LTouch);
        //������ �����.
        Vector3 dir = new Vector3(h, 0, v);
        //����ڰ� ���� ���� dir
        dir = Camera.main.transform.TransformDirection(dir);
        //�߷��� ������ �������� �߰�
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

        //�̵��Ѵ�.
        cc.Move(dir * speed * Time.deltaTime);
    }    
}
