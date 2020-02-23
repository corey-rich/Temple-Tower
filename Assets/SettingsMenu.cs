using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsButton;
    public GameObject controlsButton;
    public GameObject resumeButton;
    public GameObject quitButton;
    public GameObject backButton;
    public GameObject gammaSlider;
    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject gammaText;
    public GameObject masterText;
    public GameObject musicText;
    public GameObject sfxText;
    public GameObject firstObject;
    public GameObject secondObject;
    public GameObject thirdObject;
    public GameObject controlsMenu;

    public AudioSource myFX;
    public AudioClip clickForwardFx;
    public AudioClip clickBackwardFx;
    public AudioClip hoverFx;
    public bool pressedOnce = false;

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 && pressedOnce == false)
        {
            myFX.PlayOneShot(hoverFx);
            pressedOnce = true;
        }
        else if (Input.GetAxis("Vertical") == 0)
            pressedOnce = false;

        if (Input.GetButtonDown("Fire1"))
            myFX.PlayOneShot(clickForwardFx);
    }


    public void OnSettingsPress()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);
        controlsButton.SetActive(false);
        settingsButton.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(true);
        gammaSlider.SetActive(true);
        slider1.SetActive(true);
        slider2.SetActive(true);
        slider3.SetActive(true);
        gammaText.SetActive(true);
        musicText.SetActive(true);
        masterText.SetActive(true);
        sfxText.SetActive(true);
    }

    public void BackButton()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(secondObject, null);
        controlsButton.SetActive(true);
        settingsButton.SetActive(true);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        controlsMenu.SetActive(false);
        backButton.SetActive(false);
        gammaSlider.SetActive(false);
        slider1.SetActive(false);
        slider2.SetActive(false);
        slider3.SetActive(false);
        gammaText.SetActive(false);
        musicText.SetActive(false);
        masterText.SetActive(false);
        sfxText.SetActive(false);
    }
    public void ControlsButton()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(thirdObject, null);
        controlsMenu.SetActive(true);
        controlsButton.SetActive(false);
        settingsButton.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(false);
        gammaSlider.SetActive(false);
        slider1.SetActive(false);
        slider2.SetActive(false);
        slider3.SetActive(false);
        gammaText.SetActive(false);
        musicText.SetActive(false);
        masterText.SetActive(false);
        sfxText.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickForwardSound()
    {
        myFX.PlayOneShot(clickForwardFx);
    }

    public void ClickBackwardSound()
    {
        myFX.PlayOneShot(clickBackwardFx);
    }

    public void HoverButton()
    {
        myFX.PlayOneShot(hoverFx);
    }
}
