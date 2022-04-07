using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineTop : MonoBehaviour
{
    public Vector3 magazineTargetPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Pistol9")
        {
            gameObject.transform.parent = other.transform.GetChild(0).transform;
            gameObject.transform.position = other.transform.GetChild(0).transform.position;
            gameObject.transform.localRotation = other.transform.GetChild(0).transform.localRotation;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
