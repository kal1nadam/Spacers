using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private Image img;
    private bool selected;
    private bool increasing;
    private float minThicknes = 0.0017f;
    private float maxThicknes = 0.0080f;

    /* = new Color(0, 10205, 0, 1);*/
    public int _red;
    public int _green;
    private Color intensity;
    private Color notIntensity = new Color(0, 0, 0, 0);

    private float interpolate;

    private void Start()
    {
        img = GetComponent<Image>();
        intensity = new Color(_red, _green, 0, 1);

        increasing = true;

        img.material.SetFloat("Thicknes", this.minThicknes);
        img.material.SetColor("OutlineColor", this.notIntensity);
    }

    private void Update()
    {
        if (selected)
        {
            if (increasing)
            {
                //Debug.Log("INC");
                interpolate += Time.fixedDeltaTime / 10;
                img.material.SetFloat("Thicknes", Mathf.Lerp(minThicknes, maxThicknes, interpolate));
                if (interpolate >= 1) { increasing = false; }
            }
            else
            {
                //Debug.Log("DEC");
                interpolate -= Time.fixedDeltaTime / 10;
                img.material.SetFloat("Thicknes", Mathf.Lerp(minThicknes, maxThicknes, interpolate));
                if (interpolate <= 0) { increasing = true; }
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        //img.material.SetFloat("Thicknes", this.maxThicknes);
        //img.material.SetColor("OutlineColor", this.intensity);

        //Debug.Log("SELECT" + this.name);
        img.material.SetColor("OutlineColor", this.intensity);
        interpolate = minThicknes;
        selected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //img.material.SetFloat("Thicknes", this.minThicknes);
        //img.material.SetColor("OutlineColor", this.notIntensity);

        //Debug.Log("DESELECT" + this.name);
        img.material.SetFloat("Thicknes", minThicknes);
        img.material.SetColor("OutlineColor", this.notIntensity);
        selected = false;
    }

}
