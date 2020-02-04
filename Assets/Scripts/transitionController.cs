using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionController : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    public bool startsAlreadyFadedIn = true;
    private bool fadedIn = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!startsAlreadyFadedIn && !fadedIn)
        {
            transitionAnim.Play("FadeIn");
            fadedIn = true;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(toggleFadein());
        }
  
    }
    public void QTEtrigger()
    {
        //transitionAnim.SetTrigger("QTEtrigger");
        transitionAnim.Play("QTEIntro");
    }
    public IEnumerator toggleFadein()
    {
        if(startsAlreadyFadedIn || fadedIn)
        {
            transitionAnim.Play("FadeOut"); 
            fadedIn = false;
            startsAlreadyFadedIn = false;
        }
        else if(!fadedIn)
        {
            transitionAnim.Play("FadeIn");
            fadedIn = true;
        }

     yield return new WaitForSeconds(1.5f);
     //SceneManager.LoadScene(sceneName);
    }
}
