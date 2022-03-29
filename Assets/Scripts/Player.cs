using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    //HEALTH BAR
    [SerializeField] private Slider healthBar;
    public Gradient gradient;
    public Image fill;
    private int health;

    public static int speed;
    [SerializeField] private GameObject rotationHelpObject;

    [SerializeField] private GameObject mainCamera;


    private Text projectilesLeft;
    private Animator animator;
    

    private Vector3 mousePosition;
    public static Vector2 lookDirection;
   
    private float projectileSpeed;
    private bool readyToFire;

    private bool outOfAmmo;

    private void Awake()
    {
        //HEALTH BAR
        speed = 5;
        health = 20;
        healthBar.maxValue = health;
        healthBar.value = health;
        fill.color = gradient.Evaluate(1f);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        lookDirection = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );
    }

    private void FixedUpdate()
    {
        //MOVING
        float Ymove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float Xmove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //animator.SetFloat("Speed", Mathf.Abs(Ymove) + Mathf.Abs(Xmove));
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        transform.position = new Vector2(transform.position.x + Xmove, transform.position.y + Ymove);

        rotationHelpObject.transform.position = transform.position;

        rotationHelpObject.transform.up = lookDirection;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationHelpObject.transform.rotation, Time.deltaTime * 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redViperBullet"))
        {
            BulletHit(1);
            Destroy(collision.gameObject);
        }
    }

    private void BulletHit(int power)
    {
        animator.SetTrigger("BulletHit");
        health -= power;
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
        if (health <= 0) { Destroy(gameObject); }
    }
}
