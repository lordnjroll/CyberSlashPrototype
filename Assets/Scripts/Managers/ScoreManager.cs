using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private Mission mission;
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
    public TMP_Text Clearcollect;
    public TMP_Text Clearkill;
    public TMP_Text ClearCP;
    public TMP_Text Clearsur;
    public TMP_Text Clearmini;
    public TMP_Text Clearboss;

    [Header("Hype Settings")]
    public int HypeLevel;
    public float HypeTimer;
    public int HypeLevelThreshold;
    public int CurrentKS = 0;
    public float KSDuration = 10;

    [Header("Level Bar Setting")]
    [SerializeField] private Image _LevelBarSprite;
    public float LevelBarValue = 120;
    public float LevelBarMin = 0;
    public float LevelBarMax = 120;

    private void Start()
    {
        InvokeRepeating("HypeLevelSelector", 0, 0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        scoretxt.text = "Score : " + (score + mission_point);
        
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
            //Debug.Log("Start CountDown");
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

            if (Mission.Clearcollect)
            {
                Clearcollect.text = "<color=yellow>Collection</color>";
            }
            else
            {
                Clearcollect.text = "<color=red>Collection</color>";
            }

            if (Mission.Clearkill)
            {
                Clearkill.text = "<color=yellow>Kill</color>";
            }
            else
            {
                Clearkill.text = "<color=red>Kill</color>";
            }

            if (Mission.ClearCP)
            {
                ClearCP.text = "<color=yellow>Check Point</color>";
            }
            else
            {
                ClearCP.text = "<color=red>Check Point</color>";
            }

            if (Mission.Clearminigame)
            {
                Clearmini.text = "<color=yellow>Mini Game</color>";
            }
            else
            {
                Clearmini.text = "<color=red>Mini Game</color>";
            }

            if (Mission.Clearsur)
            {
                Clearsur.text = "<color=yellow>Survive</color>";
            }
            else
            {
                Clearsur.text = "<color=red>Survive</color>";
            }

            if (Mission.Clearboss)
            {
                Clearboss.text = "<color=yellow>Boss</color>";
            }
            else
            {
                Clearboss.text = "<color=red>Boss</color>";
            }
        }
    }

    public void HypeLevelSelector()
    {
        switch (HypeLevel)
        {
            case 1:
                break;

        }
    }

    public void OnEnemyKilled()
    {
        CurrentKS += 1;
    }

    public void KillStreakCountDown()
    {

    }

    public void LevelBarFill(float LevelBarCurrentValue, float LevelBarMin, float LevelBarMax)
    {
        _LevelBarSprite.fillAmount = Mathf.Clamp(LevelBarCurrentValue, LevelBarMin, LevelBarMax);
    } 
}
