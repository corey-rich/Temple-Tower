using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionController : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject transitionMaskGameObject;
    public ScreenTransitionImageEffect transitionMask;
    public string sceneName;
    public bool startsAlreadyFadedIn = true;
    private bool fadedIn = false;
    private bool isFiring = false;
    private bool goingDown = false;
    private bool goingUp = false;
    private float newValue;
    //private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(!startsAlreadyFadedIn && !fadedIn)
        {
            transitionAnim.Play("FadeIn");
            fadedIn = true;
        }       
        transitionMask = transitionMaskGameObject.GetComponent<ScreenTransitionImageEffect>(); 
        if(transitionMask.maskValue <= 0.000f)
        {
            goingUp = true;
            goingDown = false;
        }else if(transitionMask.maskValue >= 1.000f)
        {
            goingDown = true;
            goingUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(toggleFadein());
        }  
        if(Input.GetKeyDown(KeyCode.M))
        {
            triggerMask();
        }  
        //newValue = transitionMask.maskValue + 0.01f;
        //transitionMask.maskValue = newValue;
       if(isFiring)
        {
                if(goingUp)
                {
                    //needs to go up to 1f
                    newValue = transitionMask.maskValue + 0.0100f;
                    //counter++;
                }
                else if(goingDown)
                {
                    //needs to go down to 0f
                    newValue = transitionMask.maskValue - 0.0100f;
                    //counter++;
                }
            transitionMask.maskValue = newValue;
            if(transitionMask.maskValue <= 0.000f)
            {
                //transitionMask.maskValue = 0.000f;
                goingUp = true;
                goingDown = false;
                isFiring = false;
                //counter = 0;
            }
            else if(transitionMask.maskValue >= 1.000f)
            {
                //transitionMask.maskValue = 1.000f;
                goingDown = true;
                goingUp = false;
                isFiring = false;
                //counter = 0;
            }
        }
    }   
    public void QTEtrigger()
    {
        //transitionAnim.SetTrigger("QTEtrigger");
        transitionAnim.Play("QTEIntro");
    }
    public void OutroTrigger()
    {
        //transitionAnim.SetTrigger("QTEtrigger");
        //transitionAnim.Play("QTEIntro");
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
    public void triggerMask()
    {
        isFiring = true;
    }
}
