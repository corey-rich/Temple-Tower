using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfallSplash : MonoBehaviour
{
    public GameObject waterfallSpawn;
    private Vector3 newPos;
    private Vector3 newRot;
    private Vector3 findingY;
    private AudioSource waterSplash;
    // Start is called before the first frame update
    void Start()
    {
        waterSplash = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")            
        {
            findingY = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            float Xvalue = findingY.x;
            float Yvalue = findingY.y;
            float Zvalue = -2.76f; //findingY.z;
            newPos = new Vector3(Xvalue, Yvalue, Zvalue);
            //newRot = new Vector3(180f, -90f, 0f);
            Instantiate (waterfallSpawn, newPos, this.gameObject.transform.rotation);
            //Debug.Log("z value is" + findingY.z);
            waterSplash.Play();
        }
    }
}