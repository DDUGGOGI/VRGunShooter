using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySlide : MonoBehaviour
{
    public float slideSpeed;
    float ScrollOffset;


    void Start()
    {
        
    }


    void Update()
    {
        ScrollDown();
    }

    void ScrollDown()
    {

        ScrollOffset += (Time.deltaTime * slideSpeed) / 10f;
        gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(ScrollOffset, 0);
        

        //float offset = Time.time * scrollSpeed;
        //displayMat.SetTextureOffset("a", new Vector2(0, scrollSpeed));
        //display.gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("displayMat", new Vector2(0, offset));
    }    
}
