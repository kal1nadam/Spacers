using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximityAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (proximityAmmoCollectedInfo != null) proximityAmmoCollectedInfo();
        }
    }


    public static event Ammo.AmmoCollected proximityAmmoCollectedInfo;
}
