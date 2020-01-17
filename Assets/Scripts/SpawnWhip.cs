using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhip : MonoBehaviour
{

    public Transform spawnPos;
    public GameObject spawn;
    private float counter = 0;
    public float waitTime = 20;

    public bool isWhip;   
    void Update()
    {
        if (Input.GetAxis("Fire2") > 0 || Input.GetButtonDown("Fire4"))
            isWhip = true;
        else
            isWhip = false;

        if(counter <= 21)
            counter++;

        if (counter > waitTime)
        {
            if (Input.GetMouseButton(0) || isWhip)
            {
                Instantiate(spawn, spawnPos.position, spawnPos.rotation);
                counter = 0;
            }
            
        }

    }

}
