using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderSpawner : MonoBehaviour
{
    public GameObject cameraShake;
    public GameObject boulderPrefab;
    public ParticleSystem rockslide;
    public GameObject rubbleObject;
    public GameObject player;
    public Transform targetPos;
    public Transform playerPos;
    public AudioSource rockFall;
    public int manualDistance = 8;
    public bool alwaysOn = false;
    private bool playedOnce = false;
    private bool triggerBoulder = false;
    // Start is called before the first frame update
    void Start()
    {
        //spawnBoulderRelay(); for boulders that always spawn at the start of the round and continuously. 
        player = GameObject.Find("MilesNewWorking");
        rockslide = rubbleObject.GetComponent<ParticleSystem>();
        if (alwaysOn)
        {
            spawnBoulderRelay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!alwaysOn)
        {
            float playerDistance = Vector3.Distance(targetPos.transform.position, player.transform.position);
            if(playerDistance < manualDistance)
            {
                if (!playedOnce)
                {
                    spawnBoulderRelay(); 
                    playedOnce = true;
                }
            }
        }

        if (triggerBoulder && !playedOnce)
        {
            spawnBoulderRelay();     
            playedOnce = true;      
        }
    }
    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.tag == "Player")            
        {
            triggerBoulder = true;
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.tag == "Player")            
        {
            triggerBoulder = false;
        }
    }
    private void spawnBoulderRelay()
    {
        StartCoroutine(spawnBoulder());
    }
    IEnumerator spawnBoulder()
    {
        cameraShake.GetComponent<cameraShake>().triggerShakeSmall();
        rockFall.Play();
        rockslide.Play();
        yield return new WaitForSeconds(1.5f);
        Instantiate (boulderPrefab, targetPos.position, this.transform.rotation);
        yield return new WaitForSeconds(8);
        playedOnce = false;
        if (alwaysOn)
        {
            spawnBoulderRelay(); //turn on alwaysOn bool for automatic generation of boulders.
        }        
    }
}
