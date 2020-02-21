using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderControl : MonoBehaviour
{
    public GameObject cameraShake;
    public AudioSource boulderHit;
    public GameObject milesControl;
    private Rigidbody rb;
    private Rigidbody thisRB;
    private bool playedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = GameObject.Find("CameraFollowsThisObject");
        milesControl = GameObject.Find("MilesNewWorking");
        rb = milesControl.GetComponent<Rigidbody>();
        thisRB = GetComponent<Rigidbody>();
        thisRB.AddForce(-transform.up * 12f, ForceMode.VelocityChange);

    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && !playedOnce)
        {
            thisRB.AddForce(-transform.forward * 8f, ForceMode.VelocityChange);
            cameraShake.GetComponent<cameraShake>().triggerShakeSmall(); 
            boulderHit.Play();
            playedOnce = true;
            StartCoroutine(selfDestruct());
        }
        if (collision.gameObject.tag == "Player")
        {
            //something happens
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -12f);
            Debug.Log("yup");
        }
    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
