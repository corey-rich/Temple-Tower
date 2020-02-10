using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttach : MonoBehaviour
{
    public GameObject player;
    public GameObject OriginalParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 void OnTriggerStay (Collider other)
  {
      if(other.gameObject.tag == "Player")
      {
        player.transform.parent = this.gameObject.transform;
        //gameObject.transform.parent = player.transform;
        //Debug.Log("Player Attached");
      }
  }
  void OnTriggerExit ()
  {
      player.transform.parent = OriginalParent.transform;
  }
}
