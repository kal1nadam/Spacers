using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedViperENEMY : MonoBehaviour
{
    //HEALTH BAR
    public GameObject healthBar;
    public Slider healthBarSlider;
    public Image fill;
    public Gradient gradient;
    private int health;
    private int healthBarVisibleTimeLeft;
    private bool allowedToDecreaseHBTime;

    //SHOOTING
    [SerializeField] GameObject projectileType;
    [SerializeField] GameObject projectileStartPosition;

    private float projectileSpeed;
    private float enemyShotDelay = 0.3f;
    private bool readyToShoot;

    //FOLLOWING
    [SerializeField] private GameObject rotationHelpObject;
    private GameObject _player;
    private Vector2 playerPosition;
    private Vector2 lookDirection;

    private Vector2 positionChangeFrom;
    private Vector2 positionChangeTo;
    private bool changePostitionReady;

    private float changePositionInterpolate;

    bool goBack;
    bool ban;

    private Animator animator;

    private void Awake()
    {
        healthBar.SetActive(false);
        allowedToDecreaseHBTime = true;
        goBack = false;
        ban = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        projectileSpeed = 2f;
        readyToShoot = false;
        StartCoroutine(ShootingDelay());
        rotationHelpObject.transform.rotation = new Quaternion(1f, 1f, 1f, 1f);
        changePostitionReady = true;

        //HEALTH BAR
        health = 3;
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        //HEALTH BAR
        if (healthBarVisibleTimeLeft > 0 && allowedToDecreaseHBTime)
        {
            allowedToDecreaseHBTime = false;
            healthBar.SetActive(true);
            StartCoroutine(HealthBarVisibleDelay());
        }
        else if (healthBarVisibleTimeLeft == 0)
        {
            healthBar.SetActive(false);
        }

        //SHOOTING
        playerPosition = _player.transform.position;

        lookDirection = new Vector2(
            playerPosition.x - transform.position.x,
            playerPosition.y - transform.position.y
            );

        Vector3 helpR = rotationHelpObject.transform.rotation.eulerAngles;
        Vector3 enemyR = transform.rotation.eulerAngles;
        if (
            enemyR.z < helpR.z + 15
            && enemyR.z > helpR.z - 15
            && lookDirection.magnitude < 10f
            && readyToShoot
            )
        {
            FireBullet(lookDirection);
            readyToShoot = false;
            StartCoroutine(ShootingDelay());
        }
    }





    private void FixedUpdate()
    {
        //SHOOTING
        rotationHelpObject.transform.up = lookDirection;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationHelpObject.transform.rotation, Time.deltaTime * 2);

        //FOLLOWING
        if (lookDirection.magnitude > 10)
        {
            changePositionInterpolate = 0;
            changePostitionReady = true;
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime / 6);
        }
        else if (((lookDirection.magnitude < 3 && lookDirection.magnitude > 0.1) || goBack) && !ban)
        {
            goBack = true;
            transform.position = Vector2.Lerp(transform.position, positionChangeFrom, Time.deltaTime / 1.5f);
            Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
            if (curPos.magnitude > positionChangeFrom.magnitude - 1 && _player.transform.position.magnitude > positionChangeFrom.magnitude -2)
            {
                ban = true;
                changePositionInterpolate = 0;
                changePostitionReady = true;
                goBack = false;

            }
            else if(curPos.magnitude > positionChangeFrom.magnitude - 1)
            {
                changePositionInterpolate = 0;
                changePostitionReady = true;
                goBack = false;
            }
        }
        else if (changePostitionReady)
        {
            positionChangeFrom = transform.position;
            bool positiveX = Random.Range(0, 2) == 1;
            bool positiveY = Random.Range(0, 2) == 1;
            float Xdimension;
            float Ydimension;
            if (positiveX)
            {
                Xdimension = Random.Range(_player.transform.position.x + 6.0f, _player.transform.position.x + 10.0f);
            }
            else
            {
                Xdimension = Random.Range(_player.transform.position.x - 6.0f, _player.transform.position.x - 10.0f);
            }
            if (positiveY)
            {
                Ydimension = Random.Range(_player.transform.position.y + 6.0f, _player.transform.position.y + 10.0f);
            }
            else
            {
                Ydimension = Random.Range(_player.transform.position.y - 6.0f, _player.transform.position.y - 10.0f);
            }
            positionChangeTo = new Vector2(Xdimension, Ydimension);
            changePostitionReady = false;
        }
        else
        {
            transform.position = Vector2.Lerp(positionChangeFrom, positionChangeTo, changePositionInterpolate);
            changePositionInterpolate += Time.deltaTime / 5;
            if (changePositionInterpolate >= 1) { changePositionInterpolate = 0; changePostitionReady = true; }
        }

        if (lookDirection.magnitude > 3 && ban)
        {
            ban = false;
        }
    }

    private void LateUpdate()
    {
        healthBar.transform.eulerAngles = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("basicProjectile"))
        {
            BulletHit(1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("explosiveProjectile"))
        {
            BulletHit(10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("proximityProjectile"))
        {
            BulletHit(7);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    private void FireBullet(Vector2 direction)
    {
        GameObject _bullet = Instantiate(projectileType);
        _bullet.transform.position = projectileStartPosition.transform.position;
        _bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    private void BulletHit(int power)
    {
        animator.SetTrigger("BulletHit");
        healthBarVisibleTimeLeft = 3;
        health -= power;
        healthBarSlider.value = health;
        fill.color = gradient.Evaluate(healthBarSlider.normalizedValue);
        if (health <= 0) { Destroy(gameObject); if (redViperKill != null) { redViperKill(); } }
    }

    private IEnumerator HealthBarVisibleDelay()
    {
        yield return new WaitForSeconds(1);
        healthBarVisibleTimeLeft--;
        allowedToDecreaseHBTime = true;
    }

    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(enemyShotDelay);
        readyToShoot = true;
    }

    public delegate void RedViperKill();
    public static event RedViperKill redViperKill;


}
