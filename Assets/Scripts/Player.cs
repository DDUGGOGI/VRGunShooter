using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
    Rigidbody myRigid;

    public float applySpeed;
    public float walkSpeed;
    public bool isWalk=true;

    public float sprintSpeed;
    public bool isSprint=false;

    public float crounchSpeed;

  
    CapsuleCollider capsuleCollider;
    public LayerMask Ground;
    public Transform groundDetector;

    public float currentHP;
    public float maxHP = 100f;
    public GameObject barHP;

    Weapon theWeapon;
    Manager theManager;
    

    void Start()
    {
        //theManager = GetComponent<Manager>();
        theWeapon = GetComponent<Weapon>();
        currentHP = maxHP;

        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();

        if(!photonView.IsMine)
        {
            gameObject.layer = 9;
        }
    }

    
    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Jump();
        }
        else if(!photonView.IsMine)
        {
            barHP.SetActive(false);
        }
    }

    private void ziroMove()
    {
        float key = 0f;
        if (Input.acceleration.x > 0.6f)
        {
            key = 0.5f;
        }

        if (Input.acceleration.x < -0.6f)
        {

            key = -0.5f;
        }


        Vector3 dir = new Vector3(0f, 0f, key);

        myRigid.AddForce(dir * walkSpeed * Time.deltaTime, ForceMode.Impulse);
        
        /*
        if (key > 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (key < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        /*
        if (Input.GetMouseButtonDown(0) && !isJump)
        {
            myRigid.AddForce(Vector2.up * 5, ForceMode.Impulse);
            isJump = true;

            anim.SetBool("Walk", key > 0f || key < 0f);
        }
        */
    }

    private void Move()
    {
        float Xmove = Input.GetAxisRaw("Horizontal");
        float Zmove = Input.GetAxisRaw("Vertical");

        Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = transform.forward * Zmove;
        Vector3 movePhoneV= transform.forward * 1f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            applySpeed = sprintSpeed;
            isSprint = true;
            isWalk = false;
        }
        else
        {
            applySpeed = walkSpeed;
            isWalk = true;
            isSprint = false;
        }

        Vector3 rVec = (moveH + moveV).normalized * applySpeed  * Time.deltaTime;

        myRigid.MovePosition(transform.position + rVec);
        

        //myRigid.velocity = vecMove * walkSpeed * Time.deltaTime;
        /*
        if(Input.GetMouseButton(0))
        {
            Vector3 PhoneRVec = (moveH + movePhoneV).normalized * applySpeed * Time.deltaTime;
            myRigid.MovePosition(transform.position + PhoneRVec);
        }
        */
    }

    void Jump()
    {
        bool isGround = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, Ground);

            if(Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                isGround = false;
                myRigid.AddForce(Vector3.up*5f, ForceMode.Impulse);
            }
        
    }

    [PunRPC]
    void GetDamage()
    {
            currentHP -= theWeapon.theGun[theWeapon.currentItemIndex].damage;
            barHP.GetComponent<Image>().fillAmount -= theWeapon.theGun[theWeapon.currentItemIndex].damage * 0.01f;

            if (currentHP < 0)
            {
            
            photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
            
            }
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            photonView.RPC("GetDamage", RpcTarget.AllBuffered);
            
        }
    }



}
