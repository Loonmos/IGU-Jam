using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundshooting : MonoBehaviour
{
    public int soundSpeed = 300;
    public Rigidbody2D rb;
    private GameObject wave;

    public GameObject soundWave;
    private float cooldown = 0;
    public float cooldownTime = 3;
    private Vector2 lookDir;
    public Transform shootingPnt;
    public float waveDestroyTime = 4;
    public WhistleSounds whistleSounds;


    void Update()
    {   
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // take mouse pos make it into coords
        lookDir = new Vector2(mousePos.x - rb.transform.position.x,mousePos.y - rb.transform.position.y); // vector from player to mouse pos
        
        if(Input.GetMouseButton(0) & cooldown >= cooldownTime)
        {
            cooldown = 0;
            GameObject wave = Instantiate(soundWave, shootingPnt);
            var rbs = wave.GetComponent<Rigidbody2D>();
            wave.transform.up = lookDir;
            rbs.AddForce(lookDir.normalized * soundSpeed * Time.deltaTime, ForceMode2D.Force);
            rbs.AddForce(lookDir.normalized * soundSpeed, ForceMode2D.Impulse);
            whistleSounds.Play(wave.GetComponent<AudioSource>());
            Destroy(wave, waveDestroyTime);          
        }
        
        cooldown += Time.deltaTime;
    }
}
