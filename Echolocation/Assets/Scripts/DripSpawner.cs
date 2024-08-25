using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSpawner : MonoBehaviour
{
    private float Timer;
    public int timeToDrip;
    public GameObject drip; 


    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= timeToDrip)
        {
            Instantiate(drip, transform);
            Timer = 0;
        }
    }
}
