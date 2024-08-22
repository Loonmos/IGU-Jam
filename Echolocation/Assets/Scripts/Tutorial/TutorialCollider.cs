using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public TutorialManager tutMan;

    public string whatState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (whatState == "Jump")
            {
                Jump();
            }

            if (whatState == "Fight")
            {
                Fight();
            }
        }
    }

    public void Jump()
    {
        tutMan.state = TutorialManager.State.Jump;
        Destroy(gameObject);
    }

    public void Fight()
    {
        tutMan.state = TutorialManager.State.Fight;
        Destroy(gameObject);
    }
}
