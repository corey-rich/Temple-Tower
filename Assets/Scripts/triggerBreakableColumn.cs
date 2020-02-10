using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBreakableColumn : MonoBehaviour
{
    public Animator anim;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            anim.Play("breakableColumn"); 
    }
}
