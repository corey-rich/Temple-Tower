using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerHighlight : MonoBehaviour
{
    public Animator anim;
    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && !isPlaying)
            {
                anim.Play("InteractableHighlightControllerFadeIn"); 
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
