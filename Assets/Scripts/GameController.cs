using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //GENERAL
    [SerializeField] private GameObject _scope;
    [SerializeField] private float _maxSpawn; //20
    private GameObject _player;
    //################################################
    //AMMO
    [SerializeField] private GameObject _basicAmmoBox;
    [SerializeField] private GameObject _explosiveAmmoBox;
    [SerializeField] private GameObject _proximityAmmoBox;
    //BASIC AMMO
    private int basicAmmoSpawnDelay = 20;
    private bool basicAmmoSpawnReady;
    private int basicAmmoSpawnMaximum = 3;
    private int basicAmmoSpawnCurrent;

    //EXPLOSIVE AMMO
    private int explosiveAmmoSpawnDelay = 40;
    private bool explosiveAmmoSpawnReady;
    private int explosiveAmmoSpawnMaximum = 2;
    private int explosiveAmmoSpawnCurrent;

    //PROXIMITY AMMO
    private int proximityAmmoSpawnDelay = 60;
    private bool proximityAmmoSpawnReady;
    private int proximityAmmoSpawnMaximum = 2;
    private int proximityAmmoSpawnCurrent;

    //ENEMIES
    [SerializeField] private GameObject _redViperEnemy;
    //REDVIPER
    private float redViperSpawnDelay = 10;
    private bool redViperSpawnReady;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        _player = GameObject.FindGameObjectWithTag("Player");
        //AMMO
        //BASIC
        basicAmmoSpawnCurrent = 0;
        StartCoroutine(BasicAmmoSpawnDelay());
        //EXPLOSIVE
        explosiveAmmoSpawnCurrent = 0;
        StartCoroutine(ExplosiveAmmoSpawnDelay());
        //PROXIMITY
        proximityAmmoSpawnCurrent = 0;
        StartCoroutine(ProximityAmmoSpawnDelay());

        //ENEMIES
        redViperSpawnReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        //AMMO BOXES SPAWN
        if (basicAmmoSpawnReady && basicAmmoSpawnCurrent < basicAmmoSpawnMaximum)
        {
            GameObject ammoBox = Instantiate(_basicAmmoBox);

            ammoBox.transform.position = GeneratePositionOutOfPlayerView();
            basicAmmoSpawnCurrent++;
            basicAmmoSpawnReady = false;
            StartCoroutine(BasicAmmoSpawnDelay());
        }
        if (explosiveAmmoSpawnReady && explosiveAmmoSpawnCurrent < explosiveAmmoSpawnMaximum && Score.currentScore > 500)
        {
            GameObject ammoBox = Instantiate(_explosiveAmmoBox);

            ammoBox.transform.position = GeneratePositionOutOfPlayerView();
            explosiveAmmoSpawnCurrent++;
            explosiveAmmoSpawnReady = false;
            StartCoroutine(ExplosiveAmmoSpawnDelay());
        }
        if(proximityAmmoSpawnReady && proximityAmmoSpawnCurrent < proximityAmmoSpawnMaximum && Score.currentScore > 1000)
        {
            GameObject ammoBox = Instantiate(_proximityAmmoBox);

            ammoBox.transform.position = GeneratePositionOutOfPlayerView();
            proximityAmmoSpawnCurrent++;
            proximityAmmoSpawnReady = false;
            StartCoroutine(ProximityAmmoSpawnDelay());
        }

        if (redViperSpawnReady)
        {
            RedViperSpawn();
            redViperSpawnReady = false;
            StartCoroutine(RedViperSpawnDelay());
        }
    }

    private void FixedUpdate()
    {
        Vector2 scopePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        _scope.transform.position = scopePosition;
    }

    private void RedViperSpawn()
    {
        GameObject _redViper = Instantiate(_redViperEnemy);
        int side = Random.Range(0, 4); //0..bottom,  1..right,  2..top,  3..left
        if (side == 0)
        {
            _redViper.transform.position = new Vector3(Random.Range(-_maxSpawn, _maxSpawn), -_maxSpawn - 3, 0);
        }
        else if(side == 1)
        {
            _redViper.transform.position = new Vector3(_maxSpawn + 3, Random.Range(-_maxSpawn, _maxSpawn), 0);
        }
        else if (side == 2)
        {
            _redViper.transform.position = new Vector3(Random.Range(-_maxSpawn, _maxSpawn), _maxSpawn + 3, 0);
        }
        else if (side == 3)
        {
            _redViper.transform.position = new Vector3(-_maxSpawn - 3, Random.Range(-_maxSpawn, _maxSpawn), 0);
        }
    }

    private Vector3 GeneratePositionOutOfPlayerView()
    {
        //PlayerViewBorders
        float topBorder = _player.transform.position.y + 6;
        float bottomBorder = _player.transform.position.y - 6;
        float rightBorder = _player.transform.position.x + 9.5f;
        float leftBorder = _player.transform.position.x - 9.5f;

        Vector3 ammoBoxPos;

        do
        {
            ammoBoxPos = new Vector3(Random.Range(-_maxSpawn, _maxSpawn), Random.Range(-_maxSpawn, _maxSpawn), 0);
        }
        while (ammoBoxPos.x < rightBorder && ammoBoxPos.x > leftBorder && ammoBoxPos.y < topBorder && ammoBoxPos.y > bottomBorder);

        return ammoBoxPos;
    }

    //ENEMY
    private IEnumerator RedViperSpawnDelay()
    {
        yield return new WaitForSeconds(redViperSpawnDelay);
        redViperSpawnReady = true;
    }


    //AMMO
    private IEnumerator BasicAmmoSpawnDelay()
    {
        yield return new WaitForSeconds(basicAmmoSpawnDelay);
        basicAmmoSpawnReady = true;
    }

    private IEnumerator ExplosiveAmmoSpawnDelay()
    {
        yield return new WaitForSeconds(explosiveAmmoSpawnDelay);
        explosiveAmmoSpawnReady = true;
    }

    private IEnumerator ProximityAmmoSpawnDelay()
    {
        yield return new WaitForSeconds(proximityAmmoSpawnDelay);
        proximityAmmoSpawnReady = true;
    }


    private void BasicAmmoCollected() //subscriber
    {
        basicAmmoSpawnCurrent--;
    }

    private void ExplosiveAmmoCollected() //subscriber
    {
        explosiveAmmoSpawnCurrent--;
    }

    private void ProximityAmmoCollected() //subscriber
    {
        proximityAmmoSpawnCurrent--;
    }
}
