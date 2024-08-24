using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;
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
    public AudioSource slideSound;
    public Light2D slideLightLeft;
    public Light2D slideLightRight;
    public float slideFadeSpeed;
    public SpriteRenderer bodySprite;
    public SpriteRenderer bodySpriteLight;
    public SpriteRenderer eyesSprite;
    public LayerMask wallLayer;
    public Animator bodyAnim;
    public Animator bodyAnimLight;
    public Animator eyesAnim;
    public float walkSoundDelay = 0.5f;

    bool wallJump = false;
    bool jump = false;
    bool wallStick = false;


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
            bodySpriteLight.flipX = false;
            bodySprite.flipX = false;
            eyesSprite.flipX = false;
            bodyAnim.SetBool("Running", true);
            bodyAnimLight.SetBool("Running", true);
            eyesAnim.SetBool("Running", true);
        }
        else if (Input.GetKey("d"))
        {
            speed = 1;
            bodySprite.flipX = true;
            bodySpriteLight.flipX = true;
            eyesSprite.flipX = true;
            bodyAnim.SetBool("Running", true);
            bodyAnimLight.SetBool("Running", true);
            eyesAnim.SetBool("Running", true);
        }
        else 
        { 
            bodyAnim.SetBool("Running", false);
            bodyAnimLight.SetBool("Running", false);
            eyesAnim.SetBool("Running", false);
        }

        if (Input.GetKeyDown("space"))
        {   
            if(grounded)
            {
                jumpForce = jumpForceGround;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if(wallStick)
            {
                jumpForce = jumpForceWall;

                if (isWalledLeft()) rb.velocity = Vector2.up * jumpForce;
                if (isWalledRight()) rb.velocity = Vector2.up * jumpForce;

                slideSound.Stop();
                wallJump = true;
            }
        }

        if(Input.GetKeyUp("space"))
        {
            if (jump)
            {
                jump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 10;
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
            rb.gravityScale = 0;
            bodyAnim.SetBool("Jumping", false);
            bodyAnimLight.SetBool("Jumping", false);
            eyesAnim.SetBool("Jumping", false);
            jump = false;
            landSource.Play();
            walkSounds.walkLight.intensity = 1;
            slideSound.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bodyAnim.SetBool("WallSticking", false);
        bodyAnimLight.SetBool("WallSticking", false);
        eyesAnim.SetBool("WallSticking", false);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Tutorial") return;
        grounded = false;
        rb.gravityScale = 10;
        bodyAnim.SetBool("Jumping", true);
        bodyAnimLight.SetBool("Jumping", true);
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
        slideLightLeft.intensity = Mathf.Clamp(slideLightLeft.intensity - slideFadeSpeed * Time.deltaTime, 0, 1);
        slideLightRight.intensity = Mathf.Clamp(slideLightRight.intensity - slideFadeSpeed * Time.deltaTime, 0, 1);
        if (!isWalledLeft() && !isWalledRight() && wallJump) wallJump = false;
        if(isWalledLeft())
        {   if(grounded == false && Input.GetKey("a") && !wallJump)
            {
                if (!slideSound.isPlaying) slideSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                Debug.Log("gsgs");
                bodyAnim.SetBool("WallSticking", true);
                bodyAnimLight.SetBool("WallSticking", true);
                eyesAnim.SetBool("WallSticking", true);
                wallStick = true;
                slideLightLeft.intensity = 1;
            }
            else
            {
                bodyAnim.SetBool("WallSticking", false);
                bodyAnimLight.SetBool("WallSticking", false);
                eyesAnim.SetBool("WallSticking", false);
                wallStick = false;
            }
        }
        if (isWalledRight())
        {
            if (grounded == false && Input.GetKey("d") && !wallJump)
            {
                if (!slideSound.isPlaying) slideSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                Debug.Log("gsgs");
                bodyAnim.SetBool("WallSticking", true);
                bodyAnimLight.SetBool("WallSticking", true);
                eyesAnim.SetBool("WallSticking", true);
                wallStick = true;
                slideLightRight.intensity = 1;
            }
            else
            {
                bodyAnim.SetBool("WallSticking", false);
                bodyAnimLight.SetBool("WallSticking", false);
                eyesAnim.SetBool("WallSticking", false);
                wallStick = false;
            }
        }
    }


}
