using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirScript : MonoBehaviour
{
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        transform.up = rb.velocity; 
    }
}
