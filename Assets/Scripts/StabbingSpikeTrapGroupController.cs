using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingSpikeTrapGroupController : MonoBehaviour
{
    public Animator anim;
    public bool isReady = true;
    public float deployDelayTime;
    public AudioSource audioData;
    public AudioClip[] audioClipArray;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isReady == true)
        {
            //audioData.PlayOneShot(audioData.clip); 
            anim.Play("StabbingSpikeTrapGroup_ShootUp");
            isReady = false;
            StartCoroutine(StabbingSpikeTrapGroupDeployDelay());
            audioData.clip=audioClipArray[0];
            //audioData.Stop();
            audioData.PlayOneShot(audioData.clip);
        }
    }

    IEnumerator StabbingSpikeTrapGroupDeployDelay()
    {
        yield return new WaitForSeconds(deployDelayTime);
        isReady = true;
    }
}
