using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPlatform : MonoBehaviour
{
    public Animator anim;
    private AudioSource winch;
    // Start is called before the first frame update
    void Start()
    {
        winch = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play("levelOnePlatformUp");
            winch.Play();
        }
    }
}