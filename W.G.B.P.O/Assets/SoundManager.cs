using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

#region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlaySound(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
	

}
