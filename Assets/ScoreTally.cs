using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ScoreTally : MonoBehaviour
{
    public int taskIncrementer = 0;
    public Movement movement;
    public timerScript timerScript;

    public TMP_Text scoreText;
    private float currentScore = 0;

    public TMP_Text scoreMid;
    private float currentScoreMid = 0;

    public TMP_Text timerText;
    private int currentTimer = 0;
    private float scoreScubract;

    public TMP_Text overallScoretext;
    private float overallScore = 0;


    private void Start()
    {
        movement = GameObject.Find("MilesNewWorking").GetComponent<Movement>();
    }
    // Update is called once per frame
    void Update()
    {
        /*int.TryParse(scoreText.text, out currentScore);
        int.TryParse(scoreMid.text, out currentScoreMid);
        int.TryParse(timerText.text, out currentTimer);
        int.TryParse(overallScoretext.text, out overallScore);*/

        if (gameObject.GetComponent<Canvas>().enabled == true)
        {
            switch (taskIncrementer)
            {
                case 0:
                    if (currentScore < movement.score)
                    {
                        currentScore += 2;
                        if (currentScore >= movement.score)
                            currentScore = movement.score;
                        ScoreDisplay(currentScore, scoreText);
                    }
                    else
                    {
                        currentScore *= 5;
                        taskIncrementer += 1;
                    }
                    break;
                case 1:
                    if (currentScoreMid < currentScore)
                    {
                        currentScoreMid += 5;
                        if (currentScoreMid > currentScore)
                            currentScoreMid = currentScore;
                        ScoreDisplay(currentScoreMid, scoreMid);
                    }
                    else
                        taskIncrementer += 1;
                    break;
                case 2:
                    if (currentTimer < timerScript.guiTime)
                    {
                        currentTimer += 2;
                        if (currentTimer > timerScript.guiTime)
                            currentTimer = Mathf.RoundToInt(timerScript.guiTime) + 1;
                        ScoreDisplay(currentTimer, timerText);
                    }
                    else
                    {
                        scoreScubract = currentScore - currentTimer;
                        taskIncrementer += 1;
                    }
                    break;
                case 3:
                    if (overallScore < scoreScubract)
                    {
                        overallScore += 5;
                        if (overallScore > scoreScubract)
                            overallScore = scoreScubract;
                        ScoreDisplay(overallScore, overallScoretext);
                    }
                    break;

            }
        } 
    }

    void ScoreDisplay(float currentInt, TMP_Text currentText)
    {
        currentText.text = "0";
        for (int i = 0; i <= 5 - currentInt.ToString().Length; i++)
        {
            currentText.text = currentText.text + '0';
        }
        currentText.text += currentInt;
    }
}
