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

    private int m_killcount;
    private int m_totalkillcount = 30;
    public static bool Startkill = false;
    bool Clearkill = false;

    private int m_collectcount;
    private int m_totalcollectcount = 5;
    bool Startcollect = false;
    bool Clearcollect = false;

    private int m_CPcount;
    private int m_totalCPcount = 3;
    bool StartCP = false;
    bool ClearCP = false;

    bool Startboss = false;
    bool Clearboos = false;

    public static int m_minicount;
    private int m_totalminicount = 5;
    bool Startminigame = false;
    bool Clearminigame = false;

    bool timecount = false;
    [SerializeField] private TMP_Text Timertxt;
    public float Timer;
    public static bool Startsur = false;
    bool Clearsur = false;

    int bosshp;
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

                ScoreManager.mission_point += 600;
                Startkill = false;
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
            }
        }
    }

    void FinishCheckPoint()
    {
        CPmissiontxt.text = "Go Check Point " + m_CPcount + " / " + m_totalCPcount;
        if(m_CPcount == m_totalCPcount)
        {
            ScoreManager.mission_point += 600;
            StartCP = false;
        }
    }

    void FinishMini()
    {
        minimissiontxt.text = "Finish Hacking Mini Game " + m_minicount + " / " + m_totalminicount;
        if(m_minicount == m_totalminicount)
        {
            miniLayer.SetActive(false);
            GameUILayer.SetActive(true);
            ScoreManager.mission_point += 600;
            Startminigame = false;
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
                Timer = 0;
                timecount = false;
                ScoreManager.mission_point += 600;
                Startsur = false;
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
