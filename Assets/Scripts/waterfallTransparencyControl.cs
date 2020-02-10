using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfallTransparencyControl : MonoBehaviour
{
    public Animator anim;
    public GameObject waterfall;
    public bool reload;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && reload == true)
        {
            //Debug.Log("hit!");
            //anim.Play("waterfallTransparency"); 
            waterfall.SetActive(false);
            reload = false;
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && reload == true)
        {
            //anim.Play("waterfallReturnOpaque"); 
            waterfall.SetActive(true);
            reload = true;
        }         
    }
}
