using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScrollDown : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
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

        ScrollOffset += (Time.deltaTime * scrollSpeed) / 10f;
        gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0, ScrollOffset);
        

        //float offset = Time.time * scrollSpeed;
        //displayMat.SetTextureOffset("a", new Vector2(0, scrollSpeed));
        //display.gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("displayMat", new Vector2(0, offset));
    }    
}
