using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerHighlight : MonoBehaviour
{
    public Animator anim;
    private bool isPlaying = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPlaying)
        {
            //anim.Play("InteractableHighlightControllerFadeIn"); 
            anim.SetTrigger("fadeIn");
            isPlaying = true;               
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isPlaying)
        {
            //anim.Play("InteractableHighlightControllerFadeOut");
            anim.SetTrigger("fadeOut");
            isPlaying = false;               
        }
    }
}
