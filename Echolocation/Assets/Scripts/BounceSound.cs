using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Wave : MonoBehaviour
{
    public GameObject bounceLightPrefab;
    AudioSource source;
    public float outerRadius = 6;
    public float innerRadius = 2;
    public float lightFactor = 0.6f;
    public float soundFactor = 0.8f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        SpawnLight();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.PlayOneShot(source.clip);
        source.volume *= soundFactor;

        SpawnLight();
    }

    void SpawnLight()
    {
        var light = Instantiate(bounceLightPrefab, transform.position, Quaternion.identity).GetComponent<Light2D>();
        light.pointLightOuterRadius = outerRadius;
        light.pointLightInnerRadius = innerRadius;
        outerRadius *= lightFactor;
        innerRadius *= lightFactor;
    }
}
