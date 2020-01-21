using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Qte : MonoBehaviour
{
    public Image qteBar;
    private float startBarHealth = 100;
    private float barHealth;
    private int buttonNumber;

    public GameObject miles;
    public Sprite sleepingPuma;
    // Start is called before the first frame update
    void Start()
    {
        miles = GameObject.Find("MilesNewWorking");
        buttonNumber = Random.Range(0, 3);
        barHealth = startBarHealth;
    }

    // Update is called once per frame
    void Update()
    {
        qteBar.fillAmount = barHealth / startBarHealth;

        barHealth--;
    }
}
