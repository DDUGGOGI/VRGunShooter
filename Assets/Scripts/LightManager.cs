using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public List<GameObject> floor1Light;
    public List<GameObject> floor2Light;

    public GameObject Player;

    void Start()
    {
        
    }


    void Update()
    {
        AllFloorLightChange();
    }

    void AllFloorLightChange()
    {
        if (Player.transform.position.y > 0.4f && Player.transform.position.y < 2.3f)
        {
            for (int x = 0; x < 24; x++)
            {
                floor1Light[x].SetActive(true);
            }
            for (int y = 0; y < 18; y++)
            {
                floor2Light[y].SetActive(false);
            }
        }

        else if (Player.transform.position.y > 2.2f && Player.transform.position.y < 6.2f)
        {
            for (int x = 0; x < 24; x++)
            {
                floor1Light[x].SetActive(false);
            }
            for (int y = 0; y < 18; y++)
            {
                floor2Light[y].SetActive(true);
            }
        }

        else if (Player.transform.position.y > 6.2f && Player.transform.position.y < 8.1f)
        {
        }
    }
}
