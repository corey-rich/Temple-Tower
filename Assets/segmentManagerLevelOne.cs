using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segmentManagerLevelOne : MonoBehaviour
{
    private Animator anim;
    private GameObject gears;
    public GameObject waterfall;
    public GameObject platformTrigger;
    // Start is called before the first frame update
    void Start()
    {
        gears = GameObject.Find("GearHandler");
        anim = gears.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enableGears()
    {
        anim.Play("GearsMoving");
        waterfall.SetActive(true);
        platformTrigger.SetActive(true);
    }
    public void disableGears()
    {
        anim.Play("GearsIdle");
        waterfall.SetActive(false);
        platformTrigger.SetActive(false);
    }
}