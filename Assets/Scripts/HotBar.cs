using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    [SerializeField] private GameObject basicAmmoInfo;
    [SerializeField] private GameObject explosiveAmmoInfo;
    [SerializeField] private GameObject proximityAmmoInfo;
    private GameObject[] ammoList = new GameObject[3];

    private int _left = 0;
    private int _main = 1;
    private int _right = 2;

    public static int currentMain; //0 = basic.. 1 = explosive.. 2 = proximity

    private void OnEnable()
    {
        explosiveAmmo.explosiveAmmoCollectedInfo += ExplosiveFirst;
        proximityAmmo.proximityAmmoCollectedInfo += ProximityFirst;
    }

    private void OnDisable()
    {
        explosiveAmmo.explosiveAmmoCollectedInfo -= ExplosiveFirst;
        proximityAmmo.proximityAmmoCollectedInfo -= ProximityFirst;
    }

    // Start is called before the first frame update
    void Start()
    {
        //explosiveAmmoInfo.SetActive(false);
        //proximityAmmoInfo.SetActive(false);
        ammoList[_main] = basicAmmoInfo;
        ammoList[_main].GetComponent<Animator>().SetTrigger("StartMain");
    }
    // Update is called once per frame
    void Update() 
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (ammoList.Count() > 1)
            {
                if (Input.mouseScrollDelta.y == 1f)
                {
                    RotateRight();
                }
                else if (Input.mouseScrollDelta.y == -1f)
                {
                    RotateLeft();
                }
            }
        }
    }

    private void RotateRight()
    {
        GameObject left = ammoList[_left];
        GameObject main = ammoList[_main];
        GameObject right = ammoList[_right];

        ammoList[_main].GetComponent<Animator>().SetTrigger("MainToRight"); 
        if (ammoList.Count() == 2)
        {
            if (ammoList[_left] != null)
            {
                ammoList[_left].GetComponent<Animator>().SetTrigger("LeftToMain"); ammoList[_main] = left;
                ammoList[_left] = right;
            }
            else
            {
                ammoList[_right].GetComponent<Animator>().SetTrigger("RightToMain"); ammoList[_main] = right;
                ammoList[_left] = left;
            }
        }

        else
        {
            ammoList[_left].GetComponent<Animator>().SetTrigger("LeftToMain"); ammoList[_main] = left;
            ammoList[_right].GetComponent<Animator>().SetTrigger("RightToLeft"); ammoList[_left] = right;

        }
        ammoList[_right] = main;
        ChangeCurrentMain();
    }

    private void RotateLeft()
    {
        GameObject left = ammoList[_left];
        GameObject main = ammoList[_main];
        GameObject right = ammoList[_right];

        ammoList[_main].GetComponent<Animator>().SetTrigger("MainToLeft");
        if (ammoList.Count() == 2)
        {
            if (ammoList[_right] != null)
            {
                ammoList[_right].GetComponent<Animator>().SetTrigger("RightToMain"); ammoList[_main] = right;
                ammoList[_right] = left;
            }
            else
            {
                ammoList[_left].GetComponent<Animator>().SetTrigger("LeftToMain"); ammoList[_main] = left;
                ammoList[_right] = right;
            }
        }

        else
        {
            ammoList[_right].GetComponent<Animator>().SetTrigger("RightToMain"); ammoList[_main] = right;
            ammoList[_left].GetComponent<Animator>().SetTrigger("LeftToRight"); ammoList[_right] = left;
        }
        ammoList[_left] = main;
        ChangeCurrentMain();
    }

    private void ChangeCurrentMain()
    {
        if (ammoList[_main] == basicAmmoInfo)
        {
            currentMain = 0;
        }
        else if (ammoList[_main] == explosiveAmmoInfo)
        {
            currentMain = 1;
        }
        else if (ammoList[_main] == proximityAmmoInfo)
        {
            currentMain = 2;
        }
    }

    private void ExplosiveFirst()
    {
        explosiveAmmoInfo.SetActive(true);
        for (var i = 0; i < ammoList.Length; i++)
        {
            if (ammoList[i] == null)
            {
                ammoList[i] = explosiveAmmoInfo;
                if (i == 0) { explosiveAmmoInfo.GetComponent<Animator>().SetTrigger("StartLeft"); }
                else if (i == 2) { explosiveAmmoInfo.GetComponent<Animator>().SetTrigger("StartRight"); }
                break;
            }
        }
        explosiveAmmo.explosiveAmmoCollectedInfo -= ExplosiveFirst;
    }

    private void ProximityFirst()
    {
        proximityAmmoInfo.SetActive(true);
        for(var i = 0; i < ammoList.Length; i++)
        {
            if (ammoList[i] == null)
            {
                ammoList[i] = proximityAmmoInfo;
                if(i == 0) { proximityAmmoInfo.GetComponent<Animator>().SetTrigger("StartLeft"); }
                else if( i == 2) { proximityAmmoInfo.GetComponent<Animator>().SetTrigger("StartRight"); }
                break;
            }
        }
        proximityAmmo.proximityAmmoCollectedInfo -= ProximityFirst;
    }
}


public static class MyExtensions
{
    public static int Count(this GameObject[] _array)
    {
        int count = 0;
        foreach (var i in _array)
        {
            if (i != null) { count++; }
        }
        return count;
    }
}