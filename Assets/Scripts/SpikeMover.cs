using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMover : MonoBehaviour
{
    public GameObject spikes;        // Add in engine
    public float spikeSpeed;            // Add in engine
    public float spikeFallSpeed;        // Add in engine
    public float waitTime;              // Add in engine
    private float step;
    private float twoStep;

    public Transform firstPosition;     // Add in engine    
    public Transform endPosition;       // Add in engine

    public bool isUp;

    private void Update()
    {
        step = spikeSpeed * Time.deltaTime;
        twoStep = spikeFallSpeed * Time.deltaTime;

        if (spikes.transform.position == endPosition.position)
            isUp = true;

        if (spikes.transform.position == firstPosition.position)
            isUp = false;


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moveSpikes(spikes, step, twoStep, firstPosition, endPosition, isUp); 
        }       
    }

    private void moveSpikes(GameObject allSpikes, float step, float twoStep, Transform firstPosition, Transform endPosition, bool isUp)
    {
        // moves up
        if (!isUp)
        {
            spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, endPosition.position, step);
            //StartCoroutine(Wait(waitTime, twoStep));
        }
        else if (isUp)
        {
            StartCoroutine(Wait(waitTime, twoStep));
        }
    }

    IEnumerator Wait(float waitTime, float twoStep)
    {
        yield return new WaitForSeconds(waitTime);
        // moves down
        spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, firstPosition.position, twoStep);
    }

}
