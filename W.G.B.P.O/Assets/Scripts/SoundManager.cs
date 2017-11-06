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
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlaySound(AudioClip _clip)
    {

        audioSource.PlayOneShot(_clip);
    }

    public void PlaySoundRandomPitch(AudioClip _clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        audioSource.pitch = randomPitch;

        audioSource.PlayOneShot(_clip);

    }


}
