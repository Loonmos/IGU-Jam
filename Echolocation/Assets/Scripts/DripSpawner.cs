using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSpawner : MonoBehaviour
{
    private float dripTimer = 0.1f;
    public int timeToDrip;
    private bool timeForNewDrip = true;
    public GameObject drip;

    public Animator anim;
    private float animTimer;
    public float animTime;

    void Update()
    {
        if (dripTimer == 0)
        {
            animTimer = 0;
            anim.SetBool("Forming", true);

            animTimer += Time.deltaTime;

            if (animTimer >= animTime)
            {
                anim.SetBool("Forming", false);
                Instantiate(drip, transform.position, Quaternion.identity);
                timeForNewDrip = true;
            }
        }

        if (timeForNewDrip == true)
        {
            dripTimer += Time.deltaTime;

            if (dripTimer >= timeToDrip + Random.Range(0,5))
            {
                dripTimer = 0;
                timeForNewDrip = false;
            }
        }
    }
}
