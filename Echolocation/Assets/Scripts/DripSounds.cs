using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSounds : MonoBehaviour
{
    public AudioSource[] audioSources;
    void Play()
    {
        audioSources[Random.Range(0, audioSources.Length)].Play();
    }
    void Start()
    {
        Play();
    }
}
