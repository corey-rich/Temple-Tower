using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;

public class Qte : MonoBehaviour
{
    public Image qteBar;
    public GameObject buttonSpawn;
    public GameObject pumaSpawn;
    public int addValue;
    private string pumaName;
    private bool qteOver;
    private bool isSafe;
    private float startBarHealth = 100;
    private float barHealth;
    private int buttonNumber;

    public GameObject playerHealth;
    public GameObject[] buttonPrefabs;
    public GameObject vcam;
    public GameObject miles;
    public GameObject puma;
    public Sprite sleepingPuma;
    // Start is called before the first frame update
    void Start()
    {
        miles = GameObject.Find("MilesNewWorking");
        playerHealth = GameObject.Find("RedHealth");
        barHealth = startBarHealth;
        buttonNumber = Random.Range(0, 3);
        vcam = GameObject.Find("CM vcam1");
        pumaName = miles.GetComponent<Movement>().pumaName;
        puma = GameObject.Find(pumaName);

        vcam.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;

        Instantiate(buttonPrefabs[buttonNumber], buttonSpawn.transform);
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
            vcam.GetComponent<CinemachineVirtualCamera>().Follow = miles.transform;

            if (isSafe == true)
            {
                Instantiate(sleepingPuma);
            }
            else if (isSafe == false)
            {
                puma.transform.position = pumaSpawn.transform.position;
                playerHealth.GetComponent<RedHealthBar>().AdjustCurrentHealth(20);
            }

            Destroy(gameObject);
        }
    }

    void ButtonMash(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("w"))
                    barHealth += addValue;
                break;
            case 1:
                if (Input.GetButtonDown("Fire5") || Input.GetKeyDown("a"))
                    barHealth += addValue;
                break;
            case 2:
                if (Input.GetButtonDown("Fire3") || Input.GetKeyDown("s"))
                    barHealth += addValue;
                break;
            case 3:
                if (Input.GetButtonDown("Fire6") || Input.GetKeyDown("d"))
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
