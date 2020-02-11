using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionalArrows : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < 0)
            {
                anim.Play("LeftArrowSelect");
            }
            else if (Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0)
            {
                anim.Play("RightArrowSelect");
            }
            else if (Input.GetKeyDown("w") || Input.GetAxis("Vertical") > 0)
            {
                anim.Play("UpArrowSelect");
            }
            else if (Input.GetKeyDown("s") || Input.GetAxis("Vertical") > 0)
            {
                anim.Play("DownArrowSelect");
            }
    }
}
