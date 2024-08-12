using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private GameObject Boss;
    [SerializeField] private Transform BossSpawn;

    [SerializeField] private TMP_Text killmissiontxt;
    [SerializeField] private TMP_Text collectmissiontxt;
    [SerializeField] private TMP_Text CPmissiontxt;
    [SerializeField] private TMP_Text minimissiontxt;
    [SerializeField] private TMP_Text surmissiontxt;
    [SerializeField] private TMP_Text bossmissiontxt;
    [SerializeField] private TMP_Text tipstxt;

    [SerializeField] private GameObject GameUILayer;
    [SerializeField] private GameObject ClearLayer;
    [SerializeField] private GameObject miniLayer;
    [SerializeField] private GameObject Start_collect;
    [SerializeField] private GameObject Start_kill;
    [SerializeField] private GameObject Start_CP;
    [SerializeField] private GameObject Start_mini;
    [SerializeField] private GameObject Start_sur;
    [SerializeField] private GameObject Start_boss;
    [SerializeField] private GameObject Endcollect;
    [SerializeField] private GameObject Endkill;
    [SerializeField] private GameObject EndCP;
    [SerializeField] private GameObject Endmini;
    [SerializeField] private GameObject Endsur;
    [SerializeField] private GameObject failMission;
    [SerializeField] private GameObject failMission2;
    [SerializeField] private GameObject failMission3;
    [SerializeField] private GameObject set2, set3;
    [SerializeField] private GameObject danagerzone;
    [SerializeField] private GameObject danagerzone2;
    [SerializeField] private GameObject danagerzone3;


    public static bool plat1, plat2, plat3;
    [SerializeField] private List<Transform> checkPoint = new List<Transform>();
    [SerializeField] private List<Transform> miniGame = new List<Transform>();
    [SerializeField] private List<Transform> letter = new List<Transform>();
    [SerializeField] private GameObject obj_checkPoint, obj_mini, obj_Letter;


    public static int m_killcount;
    private int m_totalkillcount = 10;
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
    private int m_totalminicount = 2;
    public bool Startminigame;
    public static bool Clearminigame;

    public bool timecount = false;
    [SerializeField] private TMP_Text Timertxt;
    public float Timer;
    public static bool Startsur;
    public static bool Clearsur;

    

    UIManager uIManager;

    [SerializeField] private TMP_Text m_WallJump;
    [SerializeField] private TMP_Text m_Dash;
    [SerializeField] private TMP_Text m_SkillDash;

    [SerializeField] private GameObject tipsLayer;
    [SerializeField] private GameObject timetxt;
    [SerializeField] private GameObject StartTutor;
    [SerializeField] private GameObject EndTutor;
    [SerializeField] private GameObject Fail;
    [SerializeField] private GameObject set1;

    bool T_trigger = false;
    bool Wall_Jumped = false;
    public static int killcount;
    public int Totalkill = 10;

    public static bool Clear_Dash = false;
    public static bool Clear_walljump = false;
    public static bool Clear_skilldash = false;
    private spwanenimy spwanenimy;
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

        if (Startsur)
        {
            timecount = true;
            FinishSurvive();
        }

        if (Startboss)
        {
            FinishBoss();
        }
        if (T_trigger == true)
        {
            DoDash();

            if (Wall_Jumped)
            {
                m_WallJump.text = "<color=yellow>" + "Wall Run Done" + "</color>";
                Clear_walljump = true;
            }

        }

        if (Clear_Dash && Clear_walljump)
        {
            set1.GetComponent<spwanenimy>().enabled = true;
            m_Dash.text = "Try kill enemy" + killcount + " / " + Totalkill;
        }

        if (killcount == Totalkill)
        {
            set1.GetComponent<spwanenimy>().enabled = false;
            m_Dash.text = "<color=yellow>" + "Tutorial Finish" + "</color>";
            Clear_Dash = false;
            Clear_walljump = false;
            EndTutor.SetActive(true);
            Fail.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (T_trigger)
            {
                Wall_Jumped = true;
                UIManager.mission_point += 100;
            }

        }
    }


    void DoDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_Dash.text = "<color=yellow>" + "Dash Done" + "</color>";
            Clear_Dash = true;
            UIManager.mission_point += 100;
        }

        if (Weapon_Skill_Katana.T_isSkillDashing)
        {
            m_SkillDash.text = "<color=yellow>" + "Skill Dash Done" + "</color>";
            Clear_skilldash = true;
            UIManager.mission_point += 100;
            Weapon_Skill_Katana.T_isSkillDashing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StartTutorial")
        {
            timetxt.SetActive(true);
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
            set1.GetComponent<spwanenimy>().enabled = false;
            tipsLayer.SetActive(false);
            T_trigger = false;
            Clear_Dash = false;
            Clear_walljump = false;
        }

        if (other.gameObject.tag == "Fail")
        {
            set1.GetComponent<spwanenimy>().enabled = false;
            tipsLayer.SetActive(false);
            T_trigger = false;
            Debug.Log("Fail");
            Clear_Dash = false;
            Clear_walljump = false;
        }

        #region Collect Mission
        if (other.gameObject.name == "StartCollectMission")
        {
            Instantiate(obj_Letter, letter[0].position, Quaternion.identity);
            Instantiate(obj_Letter, letter[1].position, Quaternion.identity);
            Instantiate(obj_Letter, letter[2].position, Quaternion.identity);
            Instantiate(obj_Letter, letter[3].position, Quaternion.identity);
            Instantiate(obj_Letter, letter[4].position, Quaternion.identity);
            Startcollect = true;
            Start_collect.SetActive(false);
        }

        if (other.gameObject.name == "EndCollectMission")
        {
            Destroy(GameObject.FindWithTag("CollectItem"));
            collectmissiontxt.text = " ";
        }
        #endregion

        #region Kill Mission
        if (other.gameObject.name == "StartKillMission")
        {
            Startkill = true;
            set2.GetComponent<spwanenimy>().enabled = true;
            Start_kill.SetActive(false);
        }

        if (other.gameObject.name == "EndKillMission")
        {
            killmissiontxt.text = " ";
        }
        #endregion

        #region CheckPoint Mission

        if (other.gameObject.name == "StartCheckPoint")
        {
            Instantiate(obj_checkPoint, checkPoint[0].position, Quaternion.identity);
            Instantiate(obj_checkPoint, checkPoint[1].position, Quaternion.identity);
            Instantiate(obj_checkPoint, checkPoint[2].position, Quaternion.identity);
            Start_CP.SetActive(false);
            StartCP = true;
            set3.GetComponent<spwanenimy>().enabled = true;
        }

        if (other.gameObject.name == "EndCheckPoint")
        {
            Destroy(GameObject.FindWithTag("CheckPoint"));
            StartCP = false;
            CPmissiontxt.text = " ";
        }
        #endregion

        #region Fight Boss
        if (other.gameObject.name == "FightBoss")
        {
            Instantiate(Boss, BossSpawn.position, Quaternion.identity);
            Start_boss.SetActive(false);
            Startboss = true;
        }
        #endregion

        #region Mini Game
        if (other.gameObject.name == "Startmini")
        {
            minimissiontxt.text = "Finish Hacking Mini Game " + m_minicount + " / " + m_totalminicount;
            Instantiate(obj_mini, miniGame[0].position, Quaternion.identity);
            Instantiate(obj_mini, miniGame[1].position, Quaternion.identity);
            Start_mini.SetActive(false);
            Startminigame = true;
        }

        if (other.gameObject.name == "Endmini")
        {
            Destroy(GameObject.FindWithTag("CheckPoint"));
            Startminigame = false;
            minimissiontxt.text = " ";
        }
        #endregion

        #region Survive
        if (other.gameObject.name == "Startsur")
        {
            Start_sur.SetActive(false);
            Startsur = true;
        }

        if (other.gameObject.name == "EndSur")
        {
            timecount = false;
            Startsur = false;
            surmissiontxt.text = " ";
        }
        #endregion

        #region Fail
        if (other.gameObject.tag == "Fail")
        {
            Startcollect = false;
            collectmissiontxt.text = " ";
            Startkill = false;
            killmissiontxt.text = " ";
            StartCP = false;
            CPmissiontxt.text = " ";
            Startminigame = false;
            minimissiontxt.text = " ";
            Startsur = false;
            surmissiontxt.text = " ";
            set3.GetComponent<spwanenimy>().enabled = false;
            set2.GetComponent<spwanenimy>().enabled = false;
            
            for(int i = 0; i < 50; i++)
            {
                Destroy(GameObject.FindWithTag("FodderTag"));
                Destroy(GameObject.FindWithTag("Shooter"));
                Destroy(GameObject.FindWithTag("Shield"));
                Destroy(GameObject.FindWithTag("TurretTag"));
                i++;
                Debug.Log(i);
            }
            
        }
        #endregion

        if (other.gameObject.tag == "Plat1")
        {
            UIManager.isRunning = true;
            danagerzone.SetActive(true);
            danagerzone2.SetActive(false);
            danagerzone3.SetActive(false);
            plat1 = true;
            plat2 = false;
            plat3 = false;
            Debug.Log(plat1);
            for (int i = 0; i < 50; i++)
            {
                Destroy(GameObject.FindWithTag("FodderTag"));
                Destroy(GameObject.FindWithTag("Shooter"));
                Destroy(GameObject.FindWithTag("Shield"));
                Destroy(GameObject.FindWithTag("TurretTag"));
                i++;
                Debug.Log(i);
            }
        }

        if (other.gameObject.tag == "Plat2")
        {
            danagerzone.SetActive(false);
            danagerzone2.SetActive(true);
            danagerzone3.SetActive(false);
            DanagerZone.outOfZone = false;
            DanagerZone.inzone = true;
            plat1 = false;
            plat2 = true;
            plat3 = false;
            Debug.Log(plat2);
            for (int i = 0; i < 50; i++)
            {
                Destroy(GameObject.FindWithTag("FodderTag"));
                Destroy(GameObject.FindWithTag("Shooter"));
                Destroy(GameObject.FindWithTag("Shield"));
                Destroy(GameObject.FindWithTag("TurretTag"));
                i++;
            }
        }

        if(other.gameObject.tag == "Plat3")
        {
            danagerzone.SetActive(false);
            danagerzone2.SetActive(false);
            danagerzone3.SetActive(true);
            DanagerZone.outOfZone = false;
            DanagerZone.inzone = true;
            plat1 = false;
            plat2 = false;
            plat3 = true;
            Debug.Log(plat3);
            for (int i = 0; i < 50; i++)
            {
                Destroy(GameObject.FindWithTag("FodderTag"));
                Destroy(GameObject.FindWithTag("Shooter"));
                Destroy(GameObject.FindWithTag("Shield"));
                Destroy(GameObject.FindWithTag("TurretTag"));
                i++;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollectItem")
        {
            Debug.Log("collect item");
            Destroy(collision.gameObject);
            m_collectcount++;
        }

        if(collision.gameObject.tag == "MiniGame")
        {
            if (Startminigame)
            {
                FinishMini();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameUILayer.SetActive(false);
                miniLayer.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if(collision.gameObject.tag == "CheckPoint")
        {
            m_CPcount += 1;
            Destroy(collision.gameObject);
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
                set2.GetComponent<spwanenimy>().enabled = false;
                killmissiontxt.text = "Kill Shooters " + "<color=yellow>" + m_killcount + " / " + m_totalkillcount + "</color>";
                UIManager.mission_point += 600;
                Startkill = false;
                failMission.SetActive(false);
                failMission2.SetActive(false);
                failMission3.SetActive(false);
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
                UIManager.mission_point += 600;
                Startcollect = false;
                failMission.SetActive(false);
                failMission2.SetActive(false);
                failMission3.SetActive(false);
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
            UIManager.mission_point += 600;
            StartCP = false;
            failMission.SetActive(false);
            failMission2.SetActive(false);
            failMission3.SetActive(false);
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
                UIManager.mission_point += 600;
                Startminigame = false;
                failMission.SetActive(false);
                failMission2.SetActive(false);
                failMission3.SetActive(false);
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
                if(seconds < 10)
                {
                    Timertxt.text = minutes.ToString() + ":0" + seconds.ToString();
                }
                else
                {
                    Timertxt.text = minutes.ToString() + ":" + seconds.ToString();
                }
                if(Timer > 0 && Timer <= 31)
                {
                    Timertxt.text = "<color=red>" + minutes.ToString() + ":" + seconds.ToString() + "</color>";
                }
            }
            else
            {
                Timertxt.text = " ";
                Clearsur = true;
                Timer = 0;
                timecount = false;
                set3.GetComponent<spwanenimy>().enabled = false;
                UIManager.mission_point += 600;
                surmissiontxt.text = "<color=yellow>" + "Survive" + "</color>";
                Startsur = false;
                failMission.SetActive(false);
                failMission2.SetActive(false);
                failMission3.SetActive(false);
                Endsur.SetActive(true);
            }
        }
    }

    void FinishBoss()
    {
        bossmissiontxt.text = "Fight Boss";
        if (Weapon_Skill_Katana.bossDead)
        {
            bossmissiontxt.text = "<color=yellow>" + "Boss" + "</color>";
            UIManager.mission_point += 600;
            Startboss = false;
            Clearboss = true;
            UIManager.fin = true;
            failMission.SetActive(false);
            failMission2.SetActive(false);
            
        }
    }

}
