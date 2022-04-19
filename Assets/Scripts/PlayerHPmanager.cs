using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHPmanager : MonoBehaviour
{

    public int PlayerMaxHP;
    public int PlayerCurrnetHP;

    void Start()
    {
        PlayerCurrnetHP = PlayerMaxHP;
        print("플레이어의 현제 체력은 : " + PlayerCurrnetHP);
    }

    void Update()
    {

    }

    void PlayerHPManagerF()
    {
        if (PlayerCurrnetHP<1)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayerHPdown(int damage)
    {
        PlayerCurrnetHP = PlayerCurrnetHP - damage;
        print("공격을 받았습니다. 데미지 : " + PlayerCurrnetHP);

        if (PlayerCurrnetHP < 1)
        {
            print("플레이어 체력이 0입니다.");
            gameObject.SetActive(false);
        }

    }
}
