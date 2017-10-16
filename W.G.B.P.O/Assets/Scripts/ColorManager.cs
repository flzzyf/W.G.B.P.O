using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public Color[] color = new Color[5];

    public static ColorManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            
        }
    }

    public Color GetColor(int _amount)
    {
        return color[_amount];
    }

}
