using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionalArrows : MonoBehaviour
{
    private Animator anim;
    public GameObject cameraFollowObject;
    public Transform originalPosition;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        originalPosition = cameraFollowObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey("a") || Input.GetAxis("Horizontal") < 0)
            {
                anim.Play("LeftArrowSelect");
                cameraFollowObject.transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0)
            {
                anim.Play("RightArrowSelect");
                cameraFollowObject.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (Input.GetKey("w") || Input.GetAxis("Vertical") > 0)
            {
                anim.Play("UpArrowSelect");
                cameraFollowObject.transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else if (Input.GetKey("s") || Input.GetAxis("Vertical") > 0)
            {
                anim.Play("DownArrowSelect");
                cameraFollowObject.transform.position += Vector3.down * speed * Time.deltaTime;
            }
    }

    public void ResetAndDestroy()
    {
        cameraFollowObject.transform.localPosition = new Vector3 (0, 0, 0) ;
        gameObject.SetActive(false);
    }
}
