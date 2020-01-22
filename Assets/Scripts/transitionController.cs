using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionController : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    private bool firstFadein;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnim.SetTrigger("fadeIn");
        firstFadein = true;         
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(toggleFadein());
        }
  
    }
    IEnumerator toggleFadein()
    {
        if(firstFadein)
        {
            transitionAnim.SetTrigger("fadeOut"); 
            firstFadein = false;
        }
        else if(!firstFadein)
        {
            transitionAnim.SetTrigger("fadeIn");
            firstFadein = true;
        }

     yield return new WaitForSeconds(1.5f);
     //SceneManager.LoadScene(sceneName);
    }
}
