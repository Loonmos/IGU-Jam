using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundshooting : MonoBehaviour
{
    public int soundSpeed;
    public Rigidbody2D rb;
    public Rigidbody2D rbs;
    public GameObject soundWave;
    public float cooldown;
    private Vector2 lookDir;
    private float lookAngl;



    void Update()
    {   lookDir = new Vector2(rb.transform.position.x - Input.mousePosition.x,rb.transform.position.y - Input.mousePosition.y);
        lookAngl = Mathf.Atan2(lookDir.y,lookDir.x);
        if(Input.GetMouseButton(0) & cooldown >= 3)
        {
            cooldown = 0;
            Instantiate(soundWave);
            rbs.AddForce(lookDir, ForceMode2D.Force);
        }
        cooldown += Time.deltaTime;
        Debug.Log(cooldown);
    }
}
