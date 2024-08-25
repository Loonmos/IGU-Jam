using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSpawner : MonoBehaviour
{
    private float dripTimer = 0;
    public int timeToDrip;
    private bool timeForNewDrip = true;
    public GameObject drip;

    public Animator anim;
    private float animTimer = 0;
    public float timeForAnim;

    void Update()
    {
        if (timeForNewDrip == true)
        {
            dripTimer += Time.deltaTime;

            if (dripTimer >= timeToDrip + Random.Range(0, 5))
            {
                dripTimer = 0;
                timeForNewDrip = false;
            }
        }

        if (timeForNewDrip == false)
        {
            anim.SetBool("Forming", true);

            animTimer += Time.deltaTime;
            Debug.Log(animTimer);

            if (animTimer >= timeForAnim)
            {
                animTimer = 0;
                anim.SetBool("Forming", false);
                Instantiate(drip, transform.position, Quaternion.identity);
                timeForNewDrip = true;
            }
        }
    }
}
