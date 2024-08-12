using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject gameUILayer;
    public GameObject SettingLayer;
    public GameObject DisplayLayer;
    public GameObject VolumeLayer;
    public GameObject ClearLayer;
    public GameObject deathLayer;
    public GameObject Player;
    public Transform tpchet;

    private bool menuSwitch = false;

    private hp PlayerHP;
    public AudioSource gameover;
    public AudioMixerSnapshot whenPaused, whenStarted;
    public List<Transform> respawnPoint = new List<Transform>();

    public bool Godmode = false;

    public GameObject HealthHp3Bar;
    public GameObject HealthHp2Bar;
    public GameObject HealthHp1Bar;
    [SerializeField] private GameObject DeathLayer;
    [SerializeField] private GameObject GameUILayer;
    private Mission mission;
    private int DesiredScore;
    public static int score = 0;
    public int bounsscore;
    public static int score1;

    public float Timer;
    public TMP_Text Timertxt;
    public static bool isRunning = false;

    public GameObject GameUI;
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
    public static bool fin = false;
    public static int Hp = 3;

    [SerializeField] private TMP_Text Tipstxt;
    [SerializeField] private TMP_Text hp_KeepList;
    [SerializeField] private TMP_Text exp_KeepList;
    [SerializeField] private TMP_Text power_KeepList;
    private Weapon_Skill_Katana WSK;

    [SerializeField] private float PowerHoldTime;
    [SerializeField] private int PowerBuff;
    // Update is called once per frame
    void Update()
    {
        OpenEscMenu();
        PlayerDead();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.transform.position = tpchet.position;
        }

        SmoothHP();

        //Debug.Log("The player has " + Hp);

        if (Input.GetKeyDown(KeyCode.O))
        {
            Hp -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Hp += 2;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Godmode = true;
        }

        scoretxt.text = "Score : " + (score + mission_point);

        ShowScoreBorad();
        CountDownTimer();
        scorecontroll();
        LevelBarFill();
        SkillBarFill();
        DisplayKeepList();
        UseHealthPotion();
        UseBonusPotion();
        UsePowerUp();

        if (fin)
        {
            GameUI.SetActive(false);
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
            fin = false;
        }
    }

    void DisplayKeepList()
    {
        hp_KeepList.text = Store.healthpotion.ToString();

        exp_KeepList.text = Store.bonuspotion.ToString();

        power_KeepList.text = Store.powerup.ToString();
    }

    void UseHealthPotion()
    {
        if (Input.GetKeyDown(KeyCode.H) && Store.healthpotion > 0)
        {
            hp.Hp += 1;
            Store.healthpotion--;
        }
        else if (Input.GetKeyDown(KeyCode.H) && Store.healthpotion == 0)
        {
            Tipstxt.text = "You dont have any health potion.";
            StartCoroutine(ClearText());
        }
    }

    void UseBonusPotion()
    {
        if (Input.GetKeyDown(KeyCode.B) && Store.bonuspotion > 0)
        {
            ScoreManager.score += 2500;
            Store.bonuspotion--;
        }
        else if (Input.GetKeyDown(KeyCode.B) && Store.bonuspotion == 0)
        {
            Tipstxt.text = "You dont have any bonus potion.";
            StartCoroutine(ClearText());
        }
    }

    void UsePowerUp()
    {
        if (Input.GetKeyDown(KeyCode.V) && Store.powerup > 0)
        {
            WSK.PlayerDamage += PowerBuff;
            if (PowerHoldTime > 0)
            {
                PowerHoldTime -= Time.deltaTime;
            }
            else
            {
                PowerHoldTime = 0;
                WSK.PlayerDamage -= PowerBuff;
            }
            Store.powerup--;
        }
        else if (Input.GetKeyDown(KeyCode.V) && Store.powerup == 0)
        {
            Tipstxt.text = "You cant power up.";
            StartCoroutine(ClearText());
        }
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        Tipstxt.text = " ";
    }

    void Start()
    {
        Hp = 3;
        InvokeRepeating("HypeLevelSelector", 0, 0.07f);
        InvokeRepeating("HypeLevelController", 0, 0.07f);
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
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(Timer / 60);
                float seconds = Mathf.FloorToInt(Timer % 60);
                if (seconds < 10)
                {
                    Timertxt.text = minutes.ToString() + ":0" + seconds.ToString();
                }
                else
                {
                    Timertxt.text = minutes.ToString() + ":" + seconds.ToString();
                }
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
            fin = true;
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
    void SmoothHP()
    {
        switch (Hp)
        {
            case 3:
                HealthHp3Bar.SetActive(true);
                HealthHp2Bar.SetActive(true);
                HealthHp1Bar.SetActive(true);
                break;
            case 2:
                HealthHp3Bar.SetActive(false);
                HealthHp2Bar.SetActive(true);
                HealthHp1Bar.SetActive(true);
                break;
            case 1:
                HealthHp3Bar.SetActive(false);
                HealthHp2Bar.SetActive(false);
                HealthHp1Bar.SetActive(true);
                break;
        }
    }

    public void ISdashing()
    {

    }

    public void HealthLost()
    {
        if (!Godmode)
        {
            Hp--;
            StartCoroutine("GodmodeTimer");
            Godmode = true;
        }

    }

    IEnumerator GodmodeTimer()
    {
        yield return new WaitForSeconds(1f);
        Godmode = false;
    }

    void OpenEscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == false)
        {
            Time.timeScale = 0f;
            gameUILayer.SetActive(false);
            SettingLayer.SetActive(false);
            escMenuLayer.SetActive(true);
            menuSwitch = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("EscMenu has open");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == true)
        {
            Time.timeScale = 1f;
            escMenuLayer.SetActive(false);
            SettingLayer.SetActive(false);
            gameUILayer.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuSwitch = false;
            Debug.Log("EscMenu has close");
        }
    }

    public void PlayerDead()
    {
        if(hp.Hp <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            escMenuLayer.SetActive(false);
            gameUILayer.SetActive(false);
            SettingLayer.SetActive(false);
            deathLayer.SetActive(true);
        }
    }

    public void ContinueBtnOnClick()
    {
        Time.timeScale = 1f;
        escMenuLayer.SetActive(false);
        SettingLayer.SetActive(false);
        gameUILayer.SetActive(true);
        menuSwitch = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SettingBtnOnClick()
    {
        escMenuLayer.SetActive(false);
        gameUILayer.SetActive(false);
        SettingLayer.SetActive(true);
    }

    public void BackESCMenu()
    {
        SettingLayer.SetActive(false);
        escMenuLayer.SetActive(true);
        
    }

    public void NextStage()
    {
        SceneManager.LoadScene("Upgrade Station");
        Time.timeScale = 1f;
    }

    public void QuitBtnOnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RetryBtnOnClick()
    {
        Time.timeScale = 1f;
        Respawn();
        hp.Hp = 3;
        deathLayer.SetActive(false);
        gameUILayer.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Retry");
    }

    public void ReturnBtnOnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("stage 1");
    }

    public void DisplayBtnOnClick()
    {
        VolumeLayer.SetActive(false);
        DisplayLayer.SetActive(true);
    }

    public void VolumeBtnOnClick()
    {
        DisplayLayer.SetActive(false);
        VolumeLayer.SetActive(true);
    }

    void Respawn()
    {
        if (Mission.plat1 == true)
        {
            Player.transform.position = respawnPoint[0].transform.position;
        }
        else if (Mission.plat2 == true)
        {
            Player.transform.position = respawnPoint[1].transform.position;
        }
        else if (Mission.plat3 == true)
        {
            Player.transform.position = respawnPoint[2].transform.position;
        }
    }
}
