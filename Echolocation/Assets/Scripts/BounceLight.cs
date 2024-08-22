using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BounceLight : MonoBehaviour
{
    public Light2D light2d;
    public float fadeSpeed;
    void Update()
    {
        light2d.intensity -= fadeSpeed * Time.deltaTime;
        if(light2d.intensity <= 0) Destroy(gameObject);
    }
}
