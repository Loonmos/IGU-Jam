using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Drip : MonoBehaviour
{
    public float animationTime;
    public float cooldown;

    public bool playing = false;
    public Animator anim;
    public GameObject lightPrefab;
    public AudioSource dripSound;
    public AudioClip[] dripSounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Player")
        {
            playing = true;
            anim.SetBool("Fallen", true);
            dripSound.PlayOneShot(dripSounds[Random.Range(0, dripSounds.Length)]);
            var lightObj = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            var light = lightObj.GetComponent<Light2D>();
            light.pointLightOuterRadius = 3;
            light.pointLightInnerRadius = 0;
            light.intensity = 0.6f;
        }
    }

    private void Update()
    {
        if (playing == true)
        {
            cooldown += Time.deltaTime;
        }

        if (cooldown >= animationTime)
        {
            playing = false;
            Destroy(gameObject);
        }
    }
}
