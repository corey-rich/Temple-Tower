using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Qte : MonoBehaviour
{
    public Image qteBar;
    public GameObject buttonSpawn;
    private float startBarHealth = 100;
    private float barHealth;
    private int buttonNumber;

    public GameObject miles;
    public Sprite sleepingPuma;
    // Start is called before the first frame update
    void Start()
    {
        miles = GameObject.Find("MilesNewWorking");
        barHealth = startBarHealth;
        buttonNumber = Random.Range(0, 3);

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
    }
}
