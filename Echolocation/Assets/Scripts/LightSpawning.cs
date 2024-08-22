using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawning : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject lightPrefab;
    public float lightDestroyTime;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnCollisionEnter2D()
    {
        GameObject light = Instantiate(lightPrefab);
        light.transform.position = rb.position;
        Destroy(light, lightDestroyTime);
        Debug.Log("sg");
    }
}
