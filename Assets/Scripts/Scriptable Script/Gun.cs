using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName ="Gun")]
public class Gun : ScriptableObject
{
    public string gunName;

    public GameObject gunPrefab;
    public float damage;
    public float fireRate;

    public int ammo;
    public int cilipSize;
    private int currentAmmo;
    private int currentClip;

    public float recoil;
    public float reloadTime;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
