using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound")]
public class Sound : ScriptableObject{

    public AudioClip clip;
    public float volume = 0.5f;
}
