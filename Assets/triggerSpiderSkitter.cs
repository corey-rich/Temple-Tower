using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSpiderSkitter : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //anim.Play("SpiderSkitterReverse");
            anim.SetTrigger("Retreat");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //anim.Play("SpiderSkitter2");
            anim.SetTrigger("Skitter");
        }
    }
}
