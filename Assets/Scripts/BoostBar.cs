using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{

    private Slider _slider;
    private int playerBoostSpeed = 8;
    private int playerBasicSpeed = 5;

    private void OnEnable()
    {
        RedViperENEMY.redViperKill += RedViperKill;
    }

    private void OnDisable()
    {
        RedViperENEMY.redViperKill -= RedViperKill;
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(_slider.value > 0) { Player.speed = playerBoostSpeed; _slider.value -= Time.deltaTime * 10; }
            else { Player.speed = playerBasicSpeed; }
            
        }
        else { Player.speed = playerBasicSpeed; }
    }

    private void RedViperKill()
    {
        _slider.value += 5;
    }
}
