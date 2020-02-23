using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZoomOut : MonoBehaviour
{
    public MoveSegment zoom;
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
            zoom.ZoomOut();
            Debug.Log("working");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            zoom.ZoomIn();
            Debug.Log("working2");
        }
    }
}
