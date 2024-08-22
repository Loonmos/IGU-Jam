using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleSounds : MonoBehaviour
{
    public AudioSource[] audioSources;

    public void Play(AudioSource waveSource)
    {
        int i = Random.Range(0, audioSources.Length);
        audioSources[i].Play();
        waveSource.clip = audioSources[i].clip;
    }
}
