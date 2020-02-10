using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedHealthBar : MonoBehaviour
{
    Image RedHealth;
    public bool isDead = false;
    public GameObject[] MilesHeads;
    public float maxHealth = 100;
    public float curHealth = 100;
    public GameObject player;
    public Movement movement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Movement>();
        RedHealth = GetComponent<Image>();
    }

    public void AdjustCurrentHealth(int adj)
    {
    
        curHealth -= adj;
    
        if (curHealth <= 0)
        {
            curHealth = 0;
            movement.isDead = true;
        }

        if (curHealth > maxHealth)
            curHealth = maxHealth;
    
        if (maxHealth <100)
            maxHealth = 100;

        RedHealth.fillAmount = (curHealth) / 100;

        //Debug.Log(RedHealth.fillAmount);

        if ((curHealth <= 65) && (curHealth >= 34))
        {
            foreach (GameObject image in MilesHeads)
            {
                image.SetActive(false);
            }

            MilesHeads[1].SetActive(true);
        }
        else if (curHealth <= 33)
        {
            foreach (GameObject image in MilesHeads)
            {
                image.SetActive(false);
            }

            MilesHeads[2].SetActive(true);
        }
        else if (curHealth >= 66)
        {
            foreach (GameObject image in MilesHeads)
            {
                image.SetActive(false);
            }

            MilesHeads[0].SetActive(true);             
        }
    }
}
