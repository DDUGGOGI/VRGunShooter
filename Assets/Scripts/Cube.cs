using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Xrotation = 0;

        if(Input.GetMouseButton(0))
        {
            //transform.rotation = Quaternion.Euler(10, 0, 0);
           // transform.Rotate(new Vector3(100, 0, 0));
            //transform.eulerAngles = new Vector3(100, 0, 0);
            

            transform.localEulerAngles = new Vector3(Xrotation, 0, 0);



        }
    }
}
