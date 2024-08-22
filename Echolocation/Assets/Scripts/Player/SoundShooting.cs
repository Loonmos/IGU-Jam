using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundShooting : MonoBehaviour
{
    public int soundSpeed = 600;
    public Rigidbody2D rb;
    private GameObject wave;
    public GameObject soundWave;
    private float cooldown = 0;
    public float cooldownTime = 3;
    private Vector2 lookDir;
    public Transform shootingPnt;
    public float waveDestroyTime = 2;



    void Update()
    {   
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // take mouse pos make it into coords
        lookDir = new Vector2(mousePos.x - shootingPnt.position.x,mousePos.y - shootingPnt.position.y); // vector from player to mouse pos
        
        if(Input.GetMouseButton(0) & cooldown >= cooldownTime)
        {
            cooldown = 0;
            GameObject wave = Instantiate(soundWave, shootingPnt);
            var rbs = wave.GetComponent<Rigidbody2D>();
            wave.transform.up = lookDir;
            rbs.AddForce(lookDir.normalized * soundSpeed, ForceMode2D.Impulse);
            Destroy(wave, waveDestroyTime);          
        }
        
        cooldown += Time.deltaTime;
    }
}
