using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueGroundChecker : MonoBehaviour
{
    public GameObject player;
    public Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Movement>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        movement.isGrounded = true;
        //Debug.Log("Not Jumping");
    }
    void OnTriggerStay(Collider other) 
    {
        movement.isGrounded = true;
        //Debug.Log("Not Jumping");
    }
    void OnTriggerExit(Collider other)
    {
        movement.isGrounded = false; 
        //Debug.Log("Jumping");
    }
}
