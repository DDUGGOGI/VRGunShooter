using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItBall : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    public float randomTime;
    float currentTime;

    bool isHit;
    void Start()
    {
        randomTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        appearBall();


    }

    public void Hitball()
    {
        currentTime = 0;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        randomTime = Random.Range(minTime, maxTime);
        
    }

    public void appearBall()
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == false)
        {
            currentTime += Time.deltaTime;
            if (currentTime > randomTime)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
        
    }
}
