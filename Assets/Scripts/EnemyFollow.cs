using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject rotationHelpObject;
    private GameObject _player;
    private Vector2 playerPosition;
    private Vector2 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(_player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = _player.transform.position;

        lookDirection = new Vector2(
            playerPosition.x - transform.position.x,
            playerPosition.y - transform.position.y
            );

        Debug.Log(transform.rotation);
        //Debug.Log(GetComponent<Rigidbody2D>().velocity);
    }

    private void FixedUpdate()
    {
        rotationHelpObject.transform.up = lookDirection;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationHelpObject.transform.rotation, Time.deltaTime * 2);
    }
}
