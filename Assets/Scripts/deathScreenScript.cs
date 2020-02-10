using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathScreenScript : MonoBehaviour
{
public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("MilesDeathKneel");
    }
}
