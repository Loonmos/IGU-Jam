using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour
{
    public float animationTime;
    public float cooldown;

    public bool playing = false;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Player")
        {
            playing = true;
            anim.SetBool("Fallen", true);
            //play the sound
            // spawn the light
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
