using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _maxPosition = 25f;
    private void FixedUpdate()
    {
        if (transform.position.x < -_maxPosition || transform.position.x > _maxPosition || transform.position.y < -_maxPosition || transform.position.y > _maxPosition)
        {
            Destroy(gameObject);
        }
    }
}
