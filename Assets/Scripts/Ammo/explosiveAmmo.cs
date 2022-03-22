using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (explosiveAmmoCollectedInfo != null) explosiveAmmoCollectedInfo();
        }
    }


    public static event Ammo.AmmoCollected explosiveAmmoCollectedInfo;
}
