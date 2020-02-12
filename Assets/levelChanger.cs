using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class levelChanger : MonoBehaviour
{
    public string levelName;
    public void GoToNextLevel()
    {
        //levelName = "'" + levelName + "'";
        SceneManager.LoadScene(levelName);
    }
}

