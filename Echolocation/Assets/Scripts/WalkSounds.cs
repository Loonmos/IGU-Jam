using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WalkSounds : MonoBehaviour
{
    public AudioSource[] audioSources;
    public Light2D walkLight;
    public float fadeSpeed;

    public bool playing = false;
    float timer = 0;
    float delay = 0;

    public void Play(float _delay)
    {
        delay = _delay;
        playing = true;
    }

    public void Stop()
    {
        playing = false;
        timer = 0;
    }


    void Update()
    {
        walkLight.intensity = Mathf.Clamp(walkLight.intensity - fadeSpeed * Time.deltaTime, 0, 1);
        if(playing)
        {
            timer += Time.deltaTime;
            if(timer > delay) 
            {
                audioSources[Random.Range(0, audioSources.Length)].Play();
                walkLight.intensity = 0.6f;
                timer = 0;
            }
        }
    }
}
