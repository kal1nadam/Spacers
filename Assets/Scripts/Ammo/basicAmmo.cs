using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if(basicAmmoCollectedInfo != null ) basicAmmoCollectedInfo();
        }
    }

    
    public static event Ammo.AmmoCollected basicAmmoCollectedInfo;
}

public class Ammo
{
    public delegate void AmmoCollected();
}