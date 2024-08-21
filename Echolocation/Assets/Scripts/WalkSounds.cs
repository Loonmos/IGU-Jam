using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : MonoBehaviour
{
    public AudioSource[] audioSources;
    
    bool playing = false;
    float timer = 0;
    float delay = 0;

    void Start()
    {
        Play(0.5f);
    }

    public void Play(float _delay)
    {
        delay = _delay;
        playing = true;
    }

    public void Stop()
    {
        playing = false;
    }


    void Update()
    {
        if(playing)
        {
            timer += Time.deltaTime;
            if(timer > delay) 
            {
                audioSources[Random.Range(0, audioSources.Length)].Play();
                timer = 0;
            }
        }
    }
}
