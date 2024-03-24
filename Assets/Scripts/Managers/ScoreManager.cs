using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private int DesiredScore;
    public Text scorecount;
    public int score;
    string score1;

    public float scoreTime;
    public GameObject scoreBoradLayer;
    public int mission_point;
    static public int deathcount;
    public int totalscore;
    public TMP_Text scoreborad;

    bool isRunning = false;
    float stopTime;


    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        ShowScoreBorad();
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
        scoreTime += Time.deltaTime;
        
        
        //scorecount.text = "Score: " + score.ToString();

        
    }

    public void GetScore()
    {
        DesiredScore += 200;
    }

    public void ShowScoreBorad()
    {
        float scoreCounter;
        
        

        scoreCounter = score + mission_point * 5 + (int)scoreTime * 10;

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
            Debug.Log("teleport");
            isRunning = false;
            stopTime = Time.time;

            scoreBoradLayer.SetActive(true);
            scoreborad.text = "This Level you got \n"
                                            +
                              "\nScore " + score + " \n"
                                            +
                              "\nMission Point " + mission_point + " \n"
                                            +
                              "\nUse Time " + (int)scoreTime + " \n"
                                            +
                              "\nTotal Death " + deathcount + " \n"
                                            +
                              "\nTotal Score " + score1;
        }
    }

    
}
