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
    private int jumpForce;
    public int jumpForceGround;
    public int jumpForceWall;
    public Rigidbody2D rb;  
    private bool grounded = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    public Transform wallCheckLeft;
    public Transform wallCheckRight;

    public WalkSounds walkSounds;
    public AudioSource landSource;
    public SpriteRenderer sprite;
    public LayerMask wallLayer;
    public Animator anim;
    public float walkSoundDelay = 0.5f;



    void Update()
    {   
        speed = 0;
        jumpForce = 0;

        if ((Input.GetKey("a") || Input.GetKey("d")) && grounded && !walkSounds.playing)
        {
            walkSounds.Play(walkSoundDelay);
        }
        if(!Input.GetKey("d") && !Input.GetKey("a"))
        {
            walkSounds.Stop();
        }

        if(Input.GetKey("a"))
        {  
            speed = -1;
            sprite.flipX = false;
            anim.SetBool("Running", true);
        }
        else if(Input.GetKey("d"))
        {
            speed = 1;
            sprite.flipX = true;
            anim.SetBool("Running", true);
        }
        else anim.SetBool("Running", false);

        if (Input.GetKey("w"))
        {   
            if(grounded)
            {
                jumpForce = jumpForceGround;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if((isWalledLeft() || isWalledRight()) && Input.GetKey("f"))
            {
                jumpForce = jumpForceWall;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        isWalledLeft();
        isWalledRight();

        wallSlide();
        
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * speedCoef, rb.velocity.y);       
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Tutorial") return;
        grounded = true;
        anim.SetBool("Jumping", false);
        landSource.Play();
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Tutorial") return;
        grounded = false;
        anim.SetBool("Jumping", true);
        walkSounds.Stop();
    }
    private bool isWalledLeft()
    {
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f,wallLayer);
    }
    private bool isWalledRight()
    {
        return Physics2D.OverlapCircle(wallCheckRight.position, 0.2f,wallLayer);
    }
    private void wallSlide()
    {
        if(isWalledLeft() || isWalledRight())
        {   if(grounded == false && Input.GetKey("f"))
            {
                isWallSliding = true;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                Debug.Log("gsgs");
            }
        }
        else
        {
            isWallSliding = false;
        }
    }


}
