using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttach : MonoBehaviour
{
    public GameObject player;
    public GameObject OriginalParent;
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        player.transform.rotation = rotation;    
    }
 void OnTriggerStay (Collider other)
  {
      if(other.gameObject.tag == "Player")
      {
        player.transform.parent = this.gameObject.transform;
        //player.transform.rotation = Quaternion.identity;
        //gameObject.transform.parent = player.transform;
        //Debug.Log("Player Attached");
      }
  }
  void OnTriggerExit ()
  {
      player.transform.parent = OriginalParent.transform;
  }
}
