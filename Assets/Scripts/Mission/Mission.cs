using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private GameObject Boss;
    [SerializeField] private Transform BossSpawn;
    [SerializeField] private Transform BossSpawns;

    [SerializeField] private TMP_Text killmissiontxt;
    [SerializeField] private TMP_Text collectmissiontxt;
    [SerializeField] private TMP_Text CPmissiontxt;
    [SerializeField] private TMP_Text minimissiontxt;
    [SerializeField] private TMP_Text surmissiontxt;
    [SerializeField] private TMP_Text bossmissiontxt;

    [SerializeField] private GameObject GameUILayer;
    [SerializeField] private GameObject miniLayer;
    [SerializeField] private GameObject Endcollect;
    [SerializeField] private GameObject Endkill;
    [SerializeField] private GameObject EndCP;
    [SerializeField] private GameObject Endmini;
    [SerializeField] private GameObject Endsur;
    [SerializeField] private GameObject Endboss;

    private int m_killcount;
    private int m_totalkillcount = 30;
    public static bool Startkill;
    public static bool Clearkill;

    private int m_collectcount;
    private int m_totalcollectcount = 5;
    public bool Startcollect;
    public static bool Clearcollect;

    private int m_CPcount;
    private int m_totalCPcount = 3;
    public bool StartCP;
    public static bool ClearCP;

    int bosshp;
    public bool Startboss;
    public static bool Clearboss;

    public static int m_minicount;
    private int m_totalminicount = 5;
    public bool Startminigame;
    public static bool Clearminigame;

    public bool timecount = false;
    [SerializeField] private TMP_Text Timertxt;
    public float Timer;
    public static bool Startsur;
    public static bool Clearsur;

    
    ScoreManager ScoreManager;
    UIManager uIManager;


    void Update()
    {
        if (Startcollect)
        {
            collectmissiontxt.text = "Collect Items " + m_collectcount + " / " + m_totalcollectcount;
            FinishCollectMission();
        }

        if (Startkill)
        {
            FinishKillMission();
        }

        if (StartCP)
        {
            FinishCheckPoint();
        }

        if (Startminigame)
        {
            GameUILayer.SetActive(false);
            miniLayer.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FinishMini();
        }

        if (Startsur)
        {
            timecount = true;
            FinishSurvive();
        }

        if (Startboss)
        {
            FinishBoss();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Collect Mission
        if (other.gameObject.name == "StartCollectMission")
        {
            Startcollect = true;
        }

        if (other.gameObject.name == "EndCollectMission")
        {
            collectmissiontxt.text = " ";
        }
        #endregion

        #region Kill Mission
        if (other.gameObject.name == "StartKillMission")
        {
            Startkill = true;
        }

        if (other.gameObject.name == "EndKillMission")
        {
            killmissiontxt.text = " ";
        }
        #endregion

        #region CheckPoint Mission

        if (other.gameObject.name == "StartCheckPoint")
        {
            StartCP = true;
        }

        if (other.gameObject.name == "EndCheckPoint")
        {
            StartCP = false;
            CPmissiontxt.text = " ";
        }
        #endregion

        #region Fight Boss
        if (other.gameObject.name == "FightBoss")
        {
            Startboss = true;
        }
        #endregion


        #region Mini Game
        if (other.gameObject.name == "Startmini")
        {
            Startminigame = true;
        }

        if(other.gameObject.name == "Endmini")
        {
            Startminigame = false;
            minimissiontxt.text = " ";
        }
        #endregion

        #region Survive
        if (other.gameObject.name == "Startsur")
        {
            Startsur = true;
        }

        if(other.gameObject.name == "EndSur")
        {
            timecount = false;
            Startsur = false;
            surmissiontxt.text = " ";
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollectItem")
        {
            Debug.Log("Hit Collision");
            m_collectcount++;
        }
    }

    void FinishKillMission()
    {
        killmissiontxt.text = "Kill Shooters " + m_killcount + " / " + m_totalkillcount;
        if (m_killcount >= m_totalkillcount)
        {
            Clearkill = true;
            if (Clearkill)
            {
                killmissiontxt.text = "Kill Shooters " + "<color=yellow>" + m_killcount + " / " + m_totalkillcount + "</color>";
                ScoreManager.mission_point += 600;
                Startkill = false;
                Endkill.SetActive(true);
            }
        }
    }

    void FinishCollectMission()
    {
        collectmissiontxt.text = "Collect Items " + m_collectcount + " / " + m_totalcollectcount;
        if (m_collectcount >= m_totalcollectcount)
        {
            Clearcollect = true;
            if (Clearcollect)
            {
                collectmissiontxt.text = "Collect Items " + "<color=yellow>" + m_collectcount + " / " + m_totalcollectcount + "</color>";
                ScoreManager.mission_point += 600;
                Startcollect = false;
                Endcollect.SetActive(true);
            }
        }
    }

    void FinishCheckPoint()
    {
        CPmissiontxt.text = "Go Check Point " + m_CPcount + " / " + m_totalCPcount;
        if(m_CPcount == m_totalCPcount)
        {
            CPmissiontxt.text = "Go Check Point " + "<color=yellow>" + m_CPcount + " / " + m_totalCPcount + "</color>";
            ScoreManager.mission_point += 600;
            StartCP = false;
            EndCP.SetActive(true);
        }
    }

    void FinishMini()
    {
        minimissiontxt.text = "Finish Hacking Mini Game " + m_minicount + " / " + m_totalminicount;
        if(m_minicount == m_totalminicount)
        {
            Clearminigame = true;
            if (Clearminigame)
            {
                minimissiontxt.text = "Finish Hacking Mini Game " + "<color=yellow>" + m_minicount + " / " + m_totalminicount + "</color>";
                miniLayer.SetActive(false);
                GameUILayer.SetActive(true);
                ScoreManager.mission_point += 600;
                Startminigame = false;
                Endmini.SetActive(true);
            }
        }
    }

    void FinishSurvive()
    {
        surmissiontxt.text = "Survive within time";
        if (timecount)
        {
            Debug.Log("Start CountDown");
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(Timer / 60);
                float seconds = Mathf.FloorToInt(Timer % 60);

                Timertxt.text = minutes.ToString() + ":" + seconds.ToString();
                if(Timer > 0 && Timer <= 31)
                {
                    Timertxt.text = "<color=red>" + minutes.ToString() + ":" + seconds.ToString() + "</color>";
                }
            }
            else if(Timer <= 0)
            {
                Clearsur = true;
                Timer = 0;
                timecount = false;
                ScoreManager.mission_point += 600;
                Startsur = false;
                Endsur.SetActive(true);
            }
        }
    }

    void FinishBoss()
    {
        //Boss = Instantiate(BossSpawn, BossSpawns).GetComponent<>();
        if(bosshp == 0)
        {
            uIManager.gameUILayer.SetActive(false);
            uIManager.ClearLayer.SetActive(true);
        }
    }
}
