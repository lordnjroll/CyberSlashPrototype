using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text m_WallJump;
    [SerializeField] private TMP_Text m_Dash;
    [SerializeField] private TMP_Text m_SkillDash;

    [SerializeField] private GameObject tipsLayer;
    [SerializeField] private GameObject timetxt;
    [SerializeField] private GameObject StartTutor;
    [SerializeField] private GameObject EndTutor;
    [SerializeField] private GameObject Fail;
    ScoreManager ScoreManager;

    bool T_trigger = false;
    bool Wall_Jumped = false;
    public static int killcount;
    public int Totalkill = 20;

    public static bool Clear_Dash = false;
    public static bool Clear_walljump = false;
    public static bool Clear_skilldash = false;
    private spwanenimy spwanenimy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (T_trigger == true)
        {
            DoDash();

            if (Wall_Jumped)
            {
                m_WallJump.text = "<color=yellow>" + "Wall Run Done" + "</color>";
                Clear_walljump = true;
            }

        }

        if(Clear_Dash && Clear_walljump)
        {
            m_Dash.text = "Try kill enemy" + killcount + " / " + Totalkill;
        }

        if(killcount == Totalkill)
        {
            m_Dash.text = "<color=yellow>" + "Tutorial Finish" + "</color>";
            Clear_Dash = false;
            Clear_walljump = false;
            EndTutor.SetActive(true);
            Fail.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "StartTutorial")
        {
            timetxt.SetActive(true);
            ScoreManager.isRunning = true;
            Debug.Log("Start Tutorial");
            tipsLayer.SetActive(true);
            m_WallJump.text = "Do Wall Run";
            m_Dash.text = "Do Dash(LS)";
            m_SkillDash.text = "Do Skill Dash(LS + RMB)";
            T_trigger = true;
            StartTutor.SetActive(false);
        }

        if (other.gameObject.name == "FinishTutorial")
        {
            tipsLayer.SetActive(false);
            Destroy(this);
        }

        if(other.gameObject.tag == "Fail")
        {
            m_WallJump.text = " ";
            m_Dash.text = " ";
            m_SkillDash.text = " ";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Wall_Jumped = true;
            ScoreManager.mission_point += 100;
        }
    }


    void DoDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_Dash.text = "<color=yellow>" + "Dash Done" + "</color>";
            Clear_Dash = true;
            ScoreManager.mission_point += 100;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_SkillDash.text = "<color=yellow>" + "Skill Dash Done" + "</color>";
            Clear_skilldash = true;
            ScoreManager.mission_point += 100;
        }
    }
}
