using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public AudioSource audioSource;
    public AudioSource audioSource_BGM;

    //播放音效
    public void PlaySound(Sound _sound, bool _randomPitch = true)
    {
        if (_randomPitch)
        {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            audioSource.pitch = randomPitch;
        }

        audioSource.PlayOneShot(_sound.clip, _sound.volume);

        audioSource.pitch = 1;
    }

}
