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


    private int m_killcount;
    private int m_totalkillcount = 30;
    bool Startkill = false;
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

    private int m_minicount;
    private int m_totalminicount = 5;
    bool Startminigame = false;
    bool Clearminigame = false;

    bool timecount = false;
    [SerializeField] private TMP_Text Timertxt;
    public float Timer;
    bool Startsur = false;
    bool Clearsur = false;
    ScoreManager ScoreManager;

    // Update is called once per frame
    void Update()
    {
        if (Startcollect)
        {
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
        if (collision.gameObject.tag == "Collectitem")
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
        }
    }

    void FinishMini()
    {
        minimissiontxt.text = "Finish Hacking Mini Game " + m_minicount + " / " + m_totalminicount;
        if(m_minicount == m_totalminicount)
        {
            ScoreManager.mission_point += 600;
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
            }
            else
            {
                Timer = 0;
                timecount = false;
                ScoreManager.mission_point += 600;
            }
        }
    }

    void FinishBoss()
    {
        //Boss = Instantiate(BossSpawn, BossSpawns).GetComponent<>();
    }


}
