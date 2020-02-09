using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanupDestroyScript : MonoBehaviour
{
    private GameObject player;
    public int manualDistance = 30;
   // private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("MilesNewWorking");
     // trans = player.GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if(playerDistance > manualDistance)
        {
            
            DestroyGameObject();
        }
        //Debug.Log(playerDistance);
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
