using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    private Animator animChest;
    private bool hasPlayed;
    public treasureCounter treasures;
    public Movement player;
    public TextMeshProUGUI scoreText;
    private int counter = 0;
    private int chestValue = 300;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animChest = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if (counter != chestValue)
            {
                player.score += 1;
                ScoreDisplay();
                scoreText.text += player.score;
                counter++;

                if (counter == chestValue)
                {
                    counter = 0;
                    isOpen = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hasPlayed == false)
        {
            animChest.Play("ChestOpening");
            treasures.collectTreasure();
            isOpen = true;
            hasPlayed = true;

        }
    }

    public void ScoreDisplay()
    {
        scoreText.text = "0";
        for (int i = 0; i <= 5 - player.score.ToString().Length; i++)
        {
            scoreText.text = scoreText.text + '0';
        }
    }
}
