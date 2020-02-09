using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultsScreen : MonoBehaviour
{
    public transitionController blackOut;
    public Canvas resultsObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "end")
        {
            StartCoroutine(blackOut.toggleFadein());
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        resultsObject.enabled = true;
        Time.timeScale = 0;
    }
}
