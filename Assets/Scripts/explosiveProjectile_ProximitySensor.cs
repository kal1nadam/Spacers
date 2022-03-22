using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveProjectile_ProximitySensor : MonoBehaviour
{
    [SerializeField] private GameObject explosiveProjectile;
    private float explosiveProjectileSpeed = 5f;

    private bool hasTarget;
    private GameObject target;


    private void FixedUpdate()
    {
        if (target != null)
        {
            FollowEnemy(target);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("ENEMY");
            target = collision.gameObject; 
        }
    }


    private void FollowEnemy(GameObject enemy)
    {
        Vector2 positionToFollow = enemy.transform.position - transform.position;
        transform.up = positionToFollow;
        explosiveProjectile.GetComponent<Animator>().SetTrigger("hasTarget");
        GetComponent<Rigidbody2D>().velocity = positionToFollow.normalized * explosiveProjectileSpeed;
    }
}
