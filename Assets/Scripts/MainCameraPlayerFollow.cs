using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraPlayerFollow : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float maxXPosition; //7.7
    [SerializeField] private float maxYPosition; //4.2

    private Vector3 plPs; //playerPosition

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        plPs = _player.transform.position;
        transform.position = new Vector3(plPs.x, plPs.y, transform.position.z);

        if (plPs.x < -maxXPosition) { transform.position = new Vector3(-maxXPosition, transform.position.y, transform.position.z);}
        else if (plPs.x > maxXPosition) { transform.position = new Vector3(maxXPosition, transform.position.y, transform.position.z); }

        if (plPs.y < -maxYPosition) { transform.position = new Vector3(transform.position.x, -maxYPosition, transform.position.z); }
        else if (plPs.y > maxYPosition) { transform.position = new Vector3(transform.position.x, maxYPosition, transform.position.z); }
    }
}
