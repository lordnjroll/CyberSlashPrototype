using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Skill_Katana : MonoBehaviour
{
    [Header("Attack setting")]
    public Transform PlayerAttackStartPoint;
    public float AttackRange;

    [Header("Dash settings")]
    public float dashSpeed;
    public float DashDuration;
    public float DashCutThreshold; //how close can the player get before dashing into the enemy
    public bool isDashing;// Charging towards an enemy
    public bool isDodging;// Dodging to one direction
    public KeyCode MovementBtn = KeyCode.LeftShift;

    [Header("Knife throwing settings")]
    //public GameObject Kunai;
    public Camera mainCamera;
    public KeyCode secondarySkillbtn = KeyCode.Mouse1;

    [Header("Enemy Settings")]
    public GameObject Shield;
    public int ShieldHP = 10;

    private List<GameObject> MarkedTargetes;
    private int TotalEnemiesMarked = 0;

    [Header("References")]
    public Rigidbody playerRB;
    public Transform PlayerTrans; //pog
    private mesh_destroy DismentleScript;
    private GameObject Enemy;
    private ScoreManager ScoreScript;
    private FastMovementScript MoveScript;
    private hp hpScript;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        ScoreScript = GetComponent<ScoreManager>();
        MoveScript = GetComponent<FastMovementScript>();
        hpScript = GetComponent<hp>();

        Enemy = GameObject.FindWithTag("EnemyTag").gameObject;


    }

    void Update()
    {
        //TotalEnemiesMarked = MarkedTargetes.Count;
        DashInput();
        SecondaryInput();
        PlayerAttack();
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //anim.SetTrigger("AttackTrigger" );
            Debug.Log("clicked");
            RaycastHit meleehit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out meleehit, AttackRange))
            {

                if (meleehit.transform.tag == "EnemyTag")
                {
                    Enemy = meleehit.transform.gameObject;

                    DismentleScript = Enemy.GetComponent<mesh_destroy>();

                    Debug.Log("Hit enemy");

                    //EnemyScript.Killed();
                    //Destroy(Enemy);

                    DismentleScript.gothit();

                    ScoreScript.GetScore();
                }

                if (meleehit.transform.tag == "Shield")
                {
                    Shield = meleehit.transform.gameObject;

                    ShieldHP--;

                    Debug.Log("Hit Shield");

                    if (ShieldHP == 0)
                    {
                        Destroy(this.Shield);
                    }
                }
            }
            else
            {
                //Debug.Log("missed");
            }

        }
    }

    void SecondaryInput()
    {
        if (Input.GetKeyDown(secondarySkillbtn))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit knifehit;

            Debug.Log("throw knife");
            if (Physics.Raycast(ray, out knifehit))
            {
                GameObject RayCastHitObject = knifehit.transform.gameObject;
                Debug.Log("knife hit");
                if (RayCastHitObject.layer == 8)
                {                    
                    Debug.Log("Object marked: " + RayCastHitObject.name);
                    MarkedTargetes.Add(RayCastHitObject);
                }

            }
        }
    }

    void DashInput()
    {
        Transform forwardT = PlayerTrans;

        Vector3 dashDirection = GetDirection(forwardT);

        if (Input.GetKeyDown(MovementBtn))
        {
            //if (MarkedTargetes.Count <= 0)
            //{

            //}
            playerRB.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
        }           
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();
        direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;

        if(verticalInput == 00 && horizontalInput == 0)
        {
            direction = forwardT.forward;
        }
        return direction.normalized;
    }
}
