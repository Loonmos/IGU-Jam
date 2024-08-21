using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundshooting : MonoBehaviour
{
    public int soundSpeed;
    public Rigidbody2D rb;
    private GameObject wave;

    public GameObject soundWave;
    public float cooldown = 3;
    private Vector2 lookDir;
    private float lookAngl;
    public Transform shootingPnt;




    void Update()
    {   var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = new Vector2(mousePos.x - rb.transform.position.x,mousePos.y - rb.transform.position.y);
        if(Input.GetMouseButton(0) & cooldown >= 3)
        {
            cooldown = 0;
            GameObject wave = Instantiate(soundWave, shootingPnt);
            var rbs = wave.GetComponent<Rigidbody2D>();
            wave.transform.up = lookDir;
            rbs.AddForce(lookDir.normalized*300, ForceMode2D.Force);
            Destroy(wave, 2);          
        }
        cooldown += Time.deltaTime;
    }
}
