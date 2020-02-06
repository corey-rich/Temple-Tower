using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;

public class Qte : MonoBehaviour
{
    public Image qteBar;
    public GameObject[] buttonSpawn;
    public GameObject pumaSpawn;
    public int addValue;
    private GameObject buttonGraphic;
    private string pumaName;
    private bool qteOver;
    private bool isSafe;
    private float startBarHealth = 100;
    private int buttonPresses;
    private float barHealth;
    private int buttonNumber;
    //private Vector3 pumaCords;

    public GameObject playerHealth;
    public GameObject[] buttonPrefabs;
    public GameObject vcam;
    public GameObject miles;
    public GameObject cameraFollow;
    public GameObject puma;
    public GameObject sleepingPuma;
    // Start is called before the first frame update
    void Start()
    {
        miles = GameObject.Find("MilesNewWorking");
        cameraFollow = GameObject.Find("CameraFollowsThisObject");
        playerHealth = GameObject.Find("RedHealth");
        barHealth = startBarHealth;
        buttonNumber = Random.Range(0, 3);
        vcam = GameObject.Find("CM vcam1");
        pumaName = miles.GetComponent<Movement>().pumaName;
        puma = GameObject.Find(pumaName);
        vcam.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
        StartCoroutine(ButtonSpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        qteBar.fillAmount = barHealth / startBarHealth;

        barHealth--;

        ButtonMash(buttonNumber);
        StartCoroutine(QteEnd());

        if (buttonPresses == 10)
        {
            buttonNumber = Random.Range(0, 3);
            Destroy(buttonGraphic);
            buttonGraphic = Instantiate(buttonPrefabs[buttonNumber], buttonSpawn[buttonNumber].transform);
            buttonPresses = 0;
        }

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
            vcam.GetComponent<CinemachineVirtualCamera>().Follow = cameraFollow.transform;

            if (isSafe == true)
            {
                Destroy(puma);
                Instantiate(sleepingPuma, miles.transform.position, Quaternion.identity);
            }
            else if (isSafe == false)
            {
                puma.transform.position = pumaSpawn.transform.position;
                puma.GetComponent<PumaController>().speed = 2;
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
                {
                    barHealth += addValue;
                    buttonPresses++;
                }
                break;
            case 1:
                if (Input.GetButtonDown("Fire5") || Input.GetKeyDown("a"))
                {
                    barHealth += addValue;
                    buttonPresses++;
                }
                break;
            case 2:
                if (Input.GetButtonDown("Fire3") || Input.GetKeyDown("s"))
                {
                    barHealth += addValue;
                    buttonPresses++;
                }
                break;
            case 3:
                if (Input.GetButtonDown("Fire6") || Input.GetKeyDown("d"))
                {
                    barHealth += addValue;
                    buttonPresses++;
                }
                break;
            default:
                break;
        }
    }

    void ChangeButton()
    {

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
    IEnumerator ButtonSpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        buttonGraphic = Instantiate(buttonPrefabs[buttonNumber], buttonSpawn[buttonNumber].transform);
    }
}
