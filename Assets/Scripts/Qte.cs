using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Qte : MonoBehaviour
{
    public Image qteBar;
    public GameObject buttonSpawn;
    public int addValue;
    private bool qteOver;
    private bool isSafe;
    private float startBarHealth = 100;
    private float barHealth;
    private int buttonNumber;

    public GameObject vcam;
    public GameObject miles;
    public Sprite sleepingPuma;
    // Start is called before the first frame update
    void Start()
    {
        miles = GameObject.Find("MilesNewWorking");
        barHealth = startBarHealth;
        buttonNumber = Random.Range(0, 3);
        vcam = GameObject.Find("CM vcam1");

        vcam.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;

        switch (buttonNumber)
        {
            case 0:
                //Instantiate(/*A button prefab*/, buttonSpawn);
                break;
            case 1:
                //Instantiate(/*B button prefab*/, buttonSpawn);
                break;
            case 2:
                //Instantiate(/*X button prefab*/, buttonSpawn);
                break;
            case 3:
                //Instantiate(/*Y button prefab*/, buttonSpawn);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        qteBar.fillAmount = barHealth / startBarHealth;

        barHealth--;

        ButtonMash(buttonNumber);
        StartCoroutine(QteEnd());

        if (barHealth < 70)
            qteBar.color = Color.red;
        else
            qteBar.color = Color.green;

        if (barHealth > 100)
            barHealth = 100;
        else if (barHealth < 0)
            barHealth = 0;

        if (qteOver == true)
        {
            miles.transform.position = gameObject.transform.position;
            if (isSafe == true)
            {
                vcam.GetComponent<CinemachineVirtualCamera>().Follow = miles.transform;
                Instantiate(sleepingPuma);
            }
            else if (isSafe == false)
            {

            }

            Destroy(gameObject);
        }
    }

    void ButtonMash(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                if (Input.GetButtonDown("Fire1"))
                    barHealth += addValue;
                break;
            case 1:
                if (Input.GetButtonDown("Fire5"))
                    barHealth += addValue;
                break;
            case 2:
                if (Input.GetButtonDown("Fire3"))
                    barHealth += addValue;
                break;
            case 3:
                if (Input.GetButtonDown("Fire6"))
                    barHealth += addValue;
                break;
            default:
                break;
        }
    }

    IEnumerator QteEnd()
    {
        yield return new WaitForSeconds(8);

        qteOver = true;

        if (barHealth <= 70)
            isSafe = false;
        else
            isSafe = true;
    }
}
