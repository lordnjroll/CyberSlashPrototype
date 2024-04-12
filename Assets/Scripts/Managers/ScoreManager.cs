using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private int DesiredScore;
    public static int score;
    public int bounsscore;
    string score1;

    public float Timer;
    public TMP_Text Timertxt;
    public static bool isRunning = false;

    public GameObject GameUILayer;
    public GameObject scoreBoradLayer;
    public TMP_Text scoretxt;

    public static int mission_point;
    static public int deathcount;
    public int totalscore;
    public TMP_Text scoreborad;


    // Update is called once per frame
    void Update()
    {
        scoretxt.text = "Score : " + score;
        
        ShowScoreBorad();
        CountDownTimer();
        scorecontroll();
    }

    void scorecontroll()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            score += 1000;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            score -= 1000;
        }
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

    public void ShowScoreBorad()
    {
        float scoreCounter;
        
        

        scoreCounter = score + mission_point + (600 - (int)Timer) * 10;

        if (deathcount == 0)
        {
            score1 = (scoreCounter + bounsscore).ToString();
        }
        else if (deathcount >= 1 && deathcount <= 3)
        {
            score1 = scoreCounter + (bounsscore - (1000 * deathcount)).ToString();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Time.timeScale = 0;
            GameUILayer.SetActive(false);
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
