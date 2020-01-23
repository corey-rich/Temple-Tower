using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBreakableColumn : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            anim.Play("breakableColumn"); 
    }


}
