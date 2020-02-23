using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public Movement paused;
    public GameObject pauseMenu;
    public GameObject firstObject;

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                paused.isPaused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        gamePaused = false;
        paused.isPaused = false;
        Time.timeScale = 1;
    }
}
