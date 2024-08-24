using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBodyFader : MonoBehaviour
{
    bool fade = false;
    public GameObject origin;
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "BodyFader")
        {
            fade = true;
            origin = collision.gameObject.transform.GetChild(0).gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "BodyFader") fade = false;
    }

    private void Update()
    {
        if (!fade) return;
        float angle = Vector2.Angle(-origin.transform.up, transform.position - origin.transform.position);
        body.color = new Color(1, 1, 1, angle / 30f);
        eyes.color = new Color(1, 1, 1, angle / 30f);
    }
}
