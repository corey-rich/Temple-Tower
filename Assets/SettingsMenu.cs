using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsButton;
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

    public AudioSource myFX;
    public AudioClip clickForwardFx;
    public AudioClip clickBackwardFx;
    public AudioClip hoverFx;


    public void OnSettingsPress()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);
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
        settingsButton.SetActive(true);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
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
