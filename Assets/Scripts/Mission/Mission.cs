using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private TMP_Text missiontxt;
    private int m_killcount;
    private int m_totalkillcount = 30;
    bool Clearkill = false;

    private int m_collectcount;
    private int m_totalcollectcount = 3;
    bool Clearcollect = false;

    ScoreManager ScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FinishMission();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "StartMission1")
        {
            missiontxt.text = "Collect Items " + m_collectcount + " / " + m_totalcollectcount;
        }

        if(other.gameObject.name == "StartMission2")
        {
            missiontxt.text = "Kill Shooters " + m_killcount + " / " + m_totalkillcount;
        }

        if(other.gameObject.name == "CheckPoint")
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectitem")
        {
            Debug.Log("Hit Collision");
            m_collectcount++;
        }
    }

    void FinishMission()
    {
        if(m_totalkillcount >= m_killcount)
        {
            Clearkill = true;
            if (Clearkill)
            {
                ScoreManager.mission_point += 300;
            }
        }

        if(m_totalcollectcount >= m_collectcount)
        {
            Clearcollect = true;
            if (Clearcollect)
            {
                ScoreManager.mission_point += 300;
            }
        }
    }
}
