using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private int speed = 0;

    public int speedCoef = 17;
    public int jumpForce = 0;
    public Rigidbody2D rb;  
    private bool grounded;

    public AudioSource jumpSource;
    public AudioSource landSource;

    void Update()
    {   
        speed = 0;
        jumpForce = 0;
        
        if(Input.GetKey("a"))
        {  
            speed = -1;
            transform.localScale = new Vector3(1,1,1);
        }
        
        if(Input.GetKey("d"))
        {
            speed = 1;
            transform.localScale = new Vector3(-1,1,1);
        }
        
        if(Input.GetKey("w"))
        {   
            if(grounded)
            {
                jumpForce= 30;
            }
            else
            {
                jumpForce = 0;
            }    
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * speedCoef, rb.velocity.y);       
        if(grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    void OnTriggerEnter2D()
    {

        grounded = true;
    }
    void OnTriggerExit2D()
    {
        grounded = false;
    }

}
