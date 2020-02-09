using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairFixer : MonoBehaviour
{
    public PhysicMaterial originalPhys;
    public PhysicMaterial stairPhys;
    public Collider player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Player")            
        {
            Debug.Log("hit");
            player.material = stairPhys;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")            
        {
            player.material = originalPhys;
        }
    }
}
