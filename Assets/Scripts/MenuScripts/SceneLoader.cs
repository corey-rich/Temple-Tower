using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    // Update is called once per frame
    public void RunGame()
    {
        SceneManager.LoadScene("sprint1Build2");
    }
}
