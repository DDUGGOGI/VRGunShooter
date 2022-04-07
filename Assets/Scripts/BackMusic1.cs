using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic1 : MonoBehaviour
{
    public GameObject Player;
    public AudioSource audioSource;
    public AudioClip ac1;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(ac1);
        this.transform.position = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Play2FloorBackMusic();
    }

    void Play2FloorBackMusic()
    {
        if (Player.transform.position.y<1.9f)
        {
            this.transform.position = Player.transform.position;
            
        }
        else
        {
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

    }
}
