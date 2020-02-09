using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultsScreen : MonoBehaviour
{
    public transitionController blackOut;
    public Canvas resultsObject;
    public musicManager musicMan;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "end")
        {
            StartCoroutine(blackOut.toggleFadein());
            //blackOut.triggerMask();
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        musicMan.levelComplete();
        yield return new WaitForSeconds(3);
        resultsObject.enabled = true;
        //Time.timeScale = 0; moved to musicManager so it can change songs while the tally screen loads
    }
}
