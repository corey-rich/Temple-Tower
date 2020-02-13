using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class levelChanger : MonoBehaviour
{
    public string levelName;
    void Update()
    {
        //Cursor.visible = true;
        //if (Input.anyKey)
        //{
            //SceneManager.LoadScene(levelName);
        //}
    }

    public void GoToNextLevel()
    {
        //levelName = "'" + levelName + "'";
        SceneManager.LoadScene(levelName);
    }
}

