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
        print("�÷��̾��� ���� ü���� : " + PlayerCurrnetHP);
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
        print("������ �޾ҽ��ϴ�. ������ : " + PlayerCurrnetHP);

        if (PlayerCurrnetHP < 1)
        {
            print("�÷��̾� ü���� 0�Դϴ�.");
            gameObject.SetActive(false);
        }

    }
}
