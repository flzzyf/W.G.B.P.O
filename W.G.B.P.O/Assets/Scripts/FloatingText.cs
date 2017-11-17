using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Text text;

    public void SetText(string _text)
    {
        text.text = _text;
    }

}
