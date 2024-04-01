using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private int DesiredScore;
    public int score;
    string score1;

    public float Timer;
    public TMP_Text Timertxt;
    public static bool isRunning = false;

    public GameObject scoreBoradLayer;

    public static int mission_point;
    static public int deathcount;
    public int totalscore;
    public TMP_Text scoreborad;

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        ShowScoreBorad();
        CountDownTimer();
    }

    void CountDownTimer()
    {
        if (isRunning)
        {
            Debug.Log("Start CountDown");
            if(Timer > 0)
            {
                Timer -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(Timer / 60);
                float seconds = Mathf.FloorToInt(Timer % 60);

                Timertxt.text = minutes.ToString() + ":" + seconds.ToString();
            }
            else
            {
                Timer = 0;
                isRunning = false;
            }
        }
    }

    public void UpdateScore()
    {
        
        if(DesiredScore - score > 10)
        {
            score += 1;
        }else if(DesiredScore != score)
        {
            score = DesiredScore;
        }
        
    }

    public void GetScore()
    {
        DesiredScore += 200;
    }

    public void ShowScoreBorad()
    {
        float scoreCounter;
        
        

        scoreCounter = score + mission_point * 5 + (1800 - (int)Timer) * 10;

        if (deathcount == 0)
        {
            score1 = scoreCounter.ToString();
            
        }
        else if (deathcount >= 1 && deathcount <= 40)
        {
            score1 = (scoreCounter / deathcount).ToString();
        }
        else if(deathcount >= 41 && deathcount <= 80)
        {

        }
        else if(deathcount >= 81)
        {

        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isRunning = false;
            Debug.Log("teleport");

            scoreBoradLayer.SetActive(true);
            scoreborad.text = "This Level you got \n"
                                            +
                              "\nScore " + score + " \n"
                                            +
                              "\nMission Point " + mission_point + " \n"
                                            +
                              "\nUse Time " + (1800 - (int)Timer) + " \n"
                                            +
                              "\nTotal Death " + deathcount + " \n"
                                            +
                              "\nTotal Score " + score1;
        }
    }

    
}
