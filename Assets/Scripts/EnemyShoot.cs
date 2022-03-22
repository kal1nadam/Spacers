using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject projectileType;
    [SerializeField] GameObject projectileStartPosition;

    private GameObject _player;

    private float enemyShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
