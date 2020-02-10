using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpikeTrapController : MonoBehaviour
{
    public Animator anim;
    public bool isReady = true;
    public float deployDelayTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isReady == true)
        {
            //audioData.PlayOneShot(audioData.clip); 
            anim.Play("ShootingSpikeTrapShoot");
            isReady = false;
            StartCoroutine(ShootingSpikeTrapDeployDelay());
        }
    }

    IEnumerator ShootingSpikeTrapDeployDelay()
    {
        yield return new WaitForSeconds(deployDelayTime);
        isReady = true;
    }
}

