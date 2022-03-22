using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDisable : MonoBehaviour
{
    public static bool disable;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Control();
    }

    private void Update()
    {
        Control();
    }

    private void Control()
    {
        if (disable)
        {
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
