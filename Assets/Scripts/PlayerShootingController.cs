using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private GameObject projectileStartPosition;
    [SerializeField] private GameObject rotationHelpObject;

    //BASIC AMMO
    [SerializeField] private GameObject basicProjectile;
    [SerializeField] private TMP_Text basicProjectilesLeftInfo;
    private float basicProjectileSpeed;
    private bool basicReadyToFire;
    private bool basicOutOfAmmo;

    //EXPLOSIVE AMMO
    [SerializeField] private GameObject explosiveProjectile;
    [SerializeField] private TMP_Text explosiveProjectilesLeftInfo;
    private float explosiveProjectileSpeed;
    private bool explosiveReadyToFire;
    private bool explosiveOutOfAmmo;

    //PROXIMITY AMMO
    [SerializeField] private GameObject proximityProjectile;
    [SerializeField] private TMP_Text proximityProjectilesLeftInfo;
    private float proximityProjectileSpeed;
    private bool proximityReadyToFire;
    private bool proximityOutOfAmmo;


    private void OnEnable()
    {
        basicAmmo.basicAmmoCollectedInfo += BasicAmmoCollected;
        explosiveAmmo.explosiveAmmoCollectedInfo += ExplosiveAmmoCollected;
        proximityAmmo.proximityAmmoCollectedInfo += ProximityAmmoCollected;
    }

    private void OnDisable()
    {
        basicAmmo.basicAmmoCollectedInfo -= BasicAmmoCollected;
        explosiveAmmo.explosiveAmmoCollectedInfo -= ExplosiveAmmoCollected;
        proximityAmmo.proximityAmmoCollectedInfo -= ProximityAmmoCollected;
    }

    private void Awake()
    {
        //BASIC AMMO
        basicProjectileSpeed = 10f;
        basicReadyToFire = true;
        //EXPLOSIVE AMMO
        explosiveProjectileSpeed = 7f;
        explosiveReadyToFire = true;
        //PROXIMITY AMMO
        proximityProjectileSpeed = 5f;
        proximityReadyToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (HotBar.currentMain == 0 && basicReadyToFire && !basicOutOfAmmo)
            {
                FireBullet(Player.lookDirection, basicProjectileSpeed, ref basicProjectile, ref basicProjectilesLeftInfo, ref basicOutOfAmmo);
                basicReadyToFire = false;
                StartCoroutine(FireDelay(0));
            }
            else if (HotBar.currentMain == 1 && explosiveReadyToFire && !explosiveOutOfAmmo)
            {
                FireBullet(Player.lookDirection, explosiveProjectileSpeed, ref explosiveProjectile, ref explosiveProjectilesLeftInfo, ref explosiveOutOfAmmo);
                explosiveReadyToFire = false;
                StartCoroutine(FireDelay(1));
            }
            else if (HotBar.currentMain == 2 && proximityReadyToFire && !proximityOutOfAmmo)
            {
                FireBullet(Player.lookDirection, proximityProjectileSpeed, ref proximityProjectile, ref proximityProjectilesLeftInfo, ref proximityOutOfAmmo);
                proximityReadyToFire = false;
                StartCoroutine(FireDelay(2));
            }
        }
        
    }

    private void FireBullet(Vector2 direction, float projectileSpeed, ref GameObject projectileType, ref TMP_Text projectileLeftInfo, ref bool outOfAmmo)
    {
        GameObject _bullet = Instantiate(projectileType) as GameObject;
        _bullet.transform.position = projectileStartPosition.transform.position;
        _bullet.transform.rotation = rotationHelpObject.transform.rotation;
        _bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

        projectileLeftInfo.text = (Convert.ToInt32(projectileLeftInfo.text) - 1).ToString();
        if (Convert.ToInt32(projectileLeftInfo.text) == 0) { outOfAmmo = true; }
    }

    public IEnumerator FireDelay(int projectileType) //basic = 0.. explosive = 1.. proximity = 2 
    {
        if(projectileType == 0) { yield return new WaitForSeconds(0.2f); basicReadyToFire = true; }
        else if(projectileType == 1) { yield return new WaitForSeconds(0.6f); explosiveReadyToFire = true; }
        else if(projectileType == 2) { yield return new WaitForSeconds(1); proximityReadyToFire = true; }
        
    }

    private void BasicAmmoCollected()
    {
        basicOutOfAmmo = false;
        basicProjectilesLeftInfo.text = (Convert.ToInt32(basicProjectilesLeftInfo.text) + 30).ToString();
    }

    private void ExplosiveAmmoCollected()
    {
        explosiveOutOfAmmo = false;
        explosiveProjectilesLeftInfo.text = (Convert.ToInt32(explosiveProjectilesLeftInfo.text) + 10).ToString();
    }

    private void ProximityAmmoCollected()
    {
        proximityOutOfAmmo = false;
        proximityProjectilesLeftInfo.text = (Convert.ToInt32(proximityProjectilesLeftInfo.text) + 15).ToString();
    }

}
