using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderLevelCameraAndMilesController : MonoBehaviour
{
    private Animator anim;
    public int SwitchCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();         
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("b"))
        {
            SwitchViews();
        }
    }

public void SwitchViews()
{
        if((SwitchCount >= 0))
        {
            anim.Play("SideToFront");
            SwitchCount++;
        }
        if (SwitchCount == 2)
        {
            SwitchCount = 0;
            anim.Play("FrontToSide");
        }
    }
}
