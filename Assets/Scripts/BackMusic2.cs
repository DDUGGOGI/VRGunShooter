using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic2 : MonoBehaviour
{
    public GameObject Player;
    public AudioSource audioSource2;
    public AudioClip ac2;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSource2 = GetComponent<AudioSource>();

        audioSource2.PlayOneShot(ac2);
        this.transform.position = new Vector3(0, 4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Play2FloorBackMusic();
    }

    void Play2FloorBackMusic()
    {
        if (Player.transform.position.y>1.9f && Player.transform.position.y<6.2)
        {
            this.transform.position = Player.transform.position;
            
        }
        else
        {
            this.transform.position = new Vector3(0, 4f, 0);
        }

    }
}
