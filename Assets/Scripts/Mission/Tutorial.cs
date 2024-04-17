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
    ScoreManager ScoreManager;

    bool T_trigger = false;
    bool Wall_Jumped = false;

    static bool Clear_Dash = false;
    static bool Clear_walljump = false;
    static bool Clear_skilldash = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "StartTutorial")
        {
            timetxt.SetActive(true);
            ScoreManager.isRunning = true;

            Debug.Log("Start Tutorial");
            tipsLayer.SetActive(true);
            m_WallJump.text = "Do Wall Jump";
            m_Dash.text = "Do Dash";
            m_SkillDash.text = "Do Skill Dash";
            T_trigger = true;

            if (T_trigger == true)
            {
                DoDash();

                if (Wall_Jumped)
                {
                    m_WallJump.text = "Wall Jump Done";
                    Clear_walljump = true;
                }
                
            }

            if(Clear_Dash == true && Clear_walljump == true)
            {
                //try kill enemy
            }
        }

        if (other.gameObject.name == "FinishTutorial")
        {
            tipsLayer.SetActive(false);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals("Wall") && Input.GetKeyDown(KeyCode.Space))
        {
            Wall_Jumped = true;
        }
    }


    void DoDash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Dash.text = "Dash Done";
            Clear_Dash = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_SkillDash.text = "Skill Dash Donw";
            Clear_skilldash = true;
        }
    }
}
