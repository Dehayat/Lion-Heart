using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip typeSound;
    public AudioSource audioSource;

    public static SoundManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void Stop()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
    }
    public void Play()
    {
        //Debug.Log("yo");
        audioSource.clip = typeSound;
        audioSource.Play();
    }
}
