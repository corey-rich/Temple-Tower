using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSpiderSkitter : MonoBehaviour
{
    public Animator anim;

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
