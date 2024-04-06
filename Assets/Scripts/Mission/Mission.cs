using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private TMP_Text missiontxt;


    private int m_killcount;
    private int m_totalkillcount = 30;
    bool Startkill = false;
    bool Clearkill = false;

    private int m_collectcount;
    private int m_totalcollectcount = 5;
    bool Startcollect = false;
    bool Clearcollect = false;

    bool StartCP = false;
    bool ClearCP = false;

    bool Startboss = false;
    bool Clearboos = false;

    bool Startminigame = false;
    bool Clearminigame = false;

    bool Startsur = false;
    bool Clearsur = false;
    ScoreManager ScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if()
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Collect Mission
        if (other.gameObject.name == "StartCollectMission")
        {
            missiontxt.text = "Collect Items " + m_collectcount + " / " + m_totalcollectcount;
        }

        if (other.gameObject.name == "EndCollectMission")
        {

        }
        #endregion

        #region Kill Mission
        if (other.gameObject.name == "StartKillMission")
        {
            missiontxt.text = "Kill Shooters " + m_killcount + " / " + m_totalkillcount;
        }

        if (other.gameObject.name == "EndKillMission")
        {

        }
        #endregion

        #region CheckPoint Mission
        if (other.gameObject.name == "CheckPoint")
        {

        }

        if (other.gameObject.name == "EndCheckPoint")
        {

        }
        #endregion

        #region Fight Boss
        if (other.gameObject.name == "FightBoss")
        {

        }
        #endregion
        //hacking mini game
        //survie
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
        if(m_killcount >= m_totalkillcount)
        {
            Clearkill = true;
            if (Clearkill)
            {
                ScoreManager.mission_point += 600;
            }
        }

        if(m_collectcount >= m_totalcollectcount)
        {
            Clearcollect = true;
            if (Clearcollect)
            {
                ScoreManager.mission_point += 600;
            }
        }
    }

    void FinishCollectMission()
    {
        if (m_collectcount >= m_totalcollectcount)
        {
            Clearcollect = true;
            if (Clearcollect)
            {
                ScoreManager.mission_point += 600;
            }
        }
    }
}
