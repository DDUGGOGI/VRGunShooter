using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>() as T;

                if(_instance == null)
                {
                    Debug.LogWarning("There's no active " + typeof(T) + "in this scene.");
                }
            }

            return _instance;
        }
    }
}
