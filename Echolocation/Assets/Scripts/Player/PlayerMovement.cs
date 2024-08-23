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


    private float wallSlidingSpeed = 2f;
    public Transform wallCheckLeft;
    public Transform wallCheckRight;

    public WalkSounds walkSounds;
    public AudioSource landSource;
    public SpriteRenderer bodySprite;
    public SpriteRenderer eyesSprite;
    public LayerMask wallLayer;
    public Animator bodyAnim;
    public Animator eyesAnim;
    public float walkSoundDelay = 0.5f;

    bool wallJump = false;
    bool jump = false;


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

        if (Input.GetKey("a"))
        {
            speed = -1;
            bodySprite.flipX = false;
            eyesSprite.flipX = false;
            bodyAnim.SetBool("Running", true);
            eyesAnim.SetBool("Running", true);
        }
        else if (Input.GetKey("d"))
        {
            speed = 1;
            bodySprite.flipX = true;
            eyesSprite.flipX = true;
            bodyAnim.SetBool("Running", true);
            eyesAnim.SetBool("Running", true);
        }
        else 
        { 
            bodyAnim.SetBool("Running", false);
            eyesAnim.SetBool("Running", false);
        }

        if (Input.GetKeyDown("w"))
        {   
            if(grounded)
            {
                jumpForce = jumpForceGround;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if(Input.GetMouseButton(1))
            {
                jumpForce = jumpForceWall;
                    
                if (isWalledLeft()) rb.velocity = Vector2.up * jumpForce;
                if (isWalledRight()) rb.velocity = Vector2.up * jumpForce;

                wallJump = true;
            }
        }

        if(Input.GetKeyUp("w"))
        {
            if (jump)
            {
                jump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
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
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Tutorial") return;
        if (!grounded && col.tag == "Ground")
        {
            Debug.Log("Enter");
            grounded = true;
            bodyAnim.SetBool("Jumping", false);
            eyesAnim.SetBool("Jumping", false);
            jump = false;
            landSource.Play();
            walkSounds.walkLight.intensity = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Tutorial") return;
        grounded = false;
        bodyAnim.SetBool("Jumping", true);
        eyesAnim.SetBool("Jumping", true);
        jump = true;
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
        if (!isWalledLeft() && !isWalledRight() && wallJump) wallJump = false;
        if(isWalledLeft() || isWalledRight())
        {   if(grounded == false && Input.GetMouseButton(1) && !wallJump)
            {

                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                Debug.Log("gsgs");
            }
        }
        else
        {

        }
    }


}
