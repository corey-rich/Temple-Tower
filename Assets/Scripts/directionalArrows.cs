using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionalArrows : MonoBehaviour
{
    private Animator anim;
    public Movement movement;
    public GameObject cameraFollowObject;
    public Transform originalPosition;
    public float speed;
    public MoveSegment moveSegment;

    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.Find("MilesNewWorking").GetComponent<Movement>();
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

        if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0)
        {
            anim.Play("RightArrowSelect");
            cameraFollowObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey("w") || Input.GetAxis("Vertical") > 0)
        {
            anim.Play("UpArrowSelect");
            cameraFollowObject.transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.GetAxis("Vertical") < 0)
        {
            anim.Play("DownArrowSelect");
            cameraFollowObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey("q") || Input.GetButtonDown("Fire1"))
        {
            cameraFollowObject.transform.localPosition = new Vector3(0, 0, 0);
            foreach (GameObject sprites in movement.MilesSprites)
            {
                sprites.GetComponent<SpriteRenderer>().enabled = true;
            }
            movement.isLocked = false;
            movement.MilesFrontWalk.enabled = true;
            movement.MilesBackWalk.enabled = true;
            moveSegment.isLocked = false;
            moveSegment.vcam.m_Lens.FieldOfView = moveSegment.originalPosition;
            gameObject.SetActive(false);
        }

        if (Input.GetKey("e") || Input.GetButtonDown("Fire6"))
        {
            cameraFollowObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void ResetAndDestroy()
    {
        cameraFollowObject.transform.localPosition = new Vector3 (0, 0, 0);
        gameObject.SetActive(false);
    }
}
