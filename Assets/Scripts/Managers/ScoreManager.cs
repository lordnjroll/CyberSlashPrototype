using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private Mission mission;
    private int DesiredScore;
    public static int score = 0;
    public int bounsscore;
    public static int score1;

    public float Timer;
    public TMP_Text Timertxt;
    public static bool isRunning = false;

    public GameObject GameUILayer;
    public GameObject scoreBoradLayer;
    public TMP_Text scoretxt;

    public static int mission_point = 0;
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
    private spwanenimy SpawnScript;
    private GameObject[] SpawnObject;

    [Header("Level Bar Setting")]
    [SerializeField] private Image LevelBarSprite;
    [Range(0, 6)]
    [SerializeField] private int LevelBarLevel = 0;
    [SerializeField] private float LevelBarCurrentValue;
    [SerializeField] private float LevelBarDevideValue = 20f;
    [SerializeField] private int LevelBarMin = 0;
    [SerializeField] private int LevelBarMax = 120;
    [SerializeField] private Gradient LevelBarGradient;

    [Header("Skill Bar Setting")]
    [SerializeField] private Image SkillBarSprite;
    [Range(0, 6)]
    [SerializeField] private int SkillBarLevel = 0;
    [SerializeField] private float SkillBarCurrentValue;
    [SerializeField] private float SkillBarDevideValue = 20f;
    [SerializeField] private int SkillBarMin = 0;
    [SerializeField] private int SkillBarMax = 120;

    private void Start()
    {
        InvokeRepeating("HypeLevelSelector", 0, 0.07f);
        InvokeRepeating("HypeLevelController", 0, 0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        scoretxt.text = "Score : " + (score + mission_point);
        
        ShowScoreBorad();
        CountDownTimer();
        scorecontroll();
        LevelBarFill();
        SkillBarFill();
        
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
        int scoreCounter;
        
        

        scoreCounter = score + mission_point + ((int)Timer * 10);

        if (deathcount == 0)
        {
            score1 = (scoreCounter + bounsscore);
        }
        else if (deathcount >= 1 && deathcount <= 3)
        {
            score1 = scoreCounter + (bounsscore - (1000 * deathcount));
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            GameUILayer.SetActive(false);
            isRunning = false;
            Debug.Log(SaveSystem.savescore);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            scoreBoradLayer.SetActive(true);
            scoreborad.text = "This Level you got \n"
                                            +
                              "\nScore " + score + " \n"
                                            +
                              "\nMission Point " + mission_point + " \n"
                                            +
                              "\nUse Time " + (600 - (int)Timer) + " \n"
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
            Time.timeScale = 0;
        }
    }
    void HypeLevelController()
    {
        //LevelBarLevel = HypeLevel;
        Debug.Log("CurrentKS " + CurrentKS);
        if (CurrentKS <= 5)
        {
            HypeLevel = 1;
            
        }
        else if (CurrentKS >= 6 & CurrentKS <= 9)
        {
            HypeLevel = 2;
        }
        else if (CurrentKS >= 10 & CurrentKS <= 14)
        {
            HypeLevel = 3;
        }
        else if (CurrentKS >= 15 & CurrentKS <= 19)
        {
            HypeLevel = 4;
        }
        else if (CurrentKS >= 20 & CurrentKS <= 24)
        {
            HypeLevel = 5;
        }
        else if (CurrentKS >= 25)
        {
            HypeLevel = 6;
        }
        LevelBarLevel = HypeLevel;
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
        DesiredScore += 50;
    }

    public void KillStreakCountDown()
    {

    }

    //public void LevelBarFill(float LevelBarCurrentValue, float LevelBarMin, float LevelBarMax)
    //{
    //    _LevelBarSprite.fillAmount = Mathf.Clamp(LevelBarCurrentValue, LevelBarMin, LevelBarMax);
    //}

    public void LevelBarFill()
    {
        LevelBarCurrentValue = (float)LevelBarLevel * LevelBarDevideValue;
        LevelBarCurrentValue = Mathf.Clamp(LevelBarCurrentValue, LevelBarMin, LevelBarMax);
        LevelBarSprite.fillAmount = LevelBarCurrentValue / LevelBarMax;
        LevelBarSprite.color = LevelBarGradient.Evaluate(LevelBarSprite.fillAmount);
    }

    public void SkillBarFill()
    {
        SkillBarCurrentValue = (float)SkillBarLevel * SkillBarDevideValue;
        SkillBarCurrentValue = Mathf.Clamp(SkillBarCurrentValue, SkillBarMin, SkillBarMax);
        SkillBarSprite.fillAmount = SkillBarCurrentValue / SkillBarMax;
    }
}
