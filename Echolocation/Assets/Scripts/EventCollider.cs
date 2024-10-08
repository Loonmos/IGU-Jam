using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCollider : MonoBehaviour
{
    public UnityEvent colliderEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliderEvent.Invoke();
        }
    }
}
