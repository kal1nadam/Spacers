using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximityProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject proximityProjectile;
    [SerializeField] private GameObject directionHelpObject;
    private float proximityProjectileSpeed = 5f;

    //private bool hasTarget;
    private GameObject target;

    private void Update()
    {
        Vector2 flyDirection = directionHelpObject.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = flyDirection.normalized * proximityProjectileSpeed;
    }

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
            target = collision.gameObject; 
        }
    }


    private void FollowEnemy(GameObject enemy)
    {
        Vector2 positionToFollow = enemy.transform.position - transform.position;
        transform.up = positionToFollow;
        proximityProjectile.GetComponent<Animator>().SetTrigger("hasTarget");
        GetComponent<Rigidbody2D>().velocity = positionToFollow.normalized * proximityProjectileSpeed;
    }
}
