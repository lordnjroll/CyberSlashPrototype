using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Skill_Katana : MonoBehaviour
{
    [HideInInspector]

    [Header("Attack setting")]
    public Transform PlayerAttackStartPoint;
    public float AttackRange;

    [Header("Dash settings")]
    public float dashSpeed;
    public float SkillDashSpeed;
    public float DashDuration;
    public float DashCutThreshold; //how close can the player get before dashing into the enemy
    public float SkillDashStoppingDistance; //how close will the player get to the enemy after skill dashing
    public float SkillDashRange; //How far can the player skill dash into the enemy
    public float DashCutRange; //How far can the player dash into the enemy
    private RaycastHit DashCutDetector;
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

    private List<GameObject> MarkedTargetes = new List<GameObject>();
    private int TotalEnemiesMarked = 0;

    [Header("References")]
    public Rigidbody playerRB;
    public Transform PlayerTrans; //pog
    private mesh_destroy DismentleScript;
    private GameObject Enemy;
    private RaycastHit RayEnemyAimedAt; // the enemy that the player is looking at
    private GameObject EnemyAimedAt;
    private ScoreManager ScoreScript;
    private FastMovementScript MoveScript;
    private hp hpScript;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        Debug.Log("looking at " + EnemyAimedAt);

        ScoreScript = GetComponent<ScoreManager>();
        MoveScript = GetComponent<FastMovementScript>();
        hpScript = GetComponent<hp>();

        Enemy = GameObject.FindWithTag("EnemyTag").gameObject;


    }

    void Update()
    {
        Debug.Log("Looking at " + EnemyAimedAt); 
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
        if(MarkedTargetes != null)
        {
            if(Physics.Raycast(PlayerTrans.position, mainCamera.transform.forward, out RayEnemyAimedAt, SkillDashRange))
            {
                EnemyAimedAt = RayEnemyAimedAt.transform.gameObject;
            }
        }

        if (Input.GetKeyDown(secondarySkillbtn))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit knifehit;

            Debug.Log("throw knife");
            if (Physics.Raycast(ray, out knifehit))
            {
                GameObject RayCastHitObject = knifehit.transform.gameObject;
                //Debug.Log("knife hit");
                if (RayCastHitObject.layer == 8)
                {                    
                    //Debug.Log("Object marked: " + RayCastHitObject.name);
                    MarkedTargetes.Add(RayCastHitObject);
                }

            }
        }
    }

    void DashInput()
    {
        Transform forwardT = PlayerTrans;

        Vector3 dashDirection = GetDirection(forwardT);

        //Physics.Raycast(PlayerTrans.position, mainCamera.transform.forward, out RayEnemyAimedAt, DashCutRange);

        if (Input.GetKeyDown(MovementBtn) && !MoveScript.isWallRunning && !isDashing)
        {
            if (MarkedTargetes.Contains(EnemyAimedAt))
            {
                Vector3 EnemyLocation = EnemyAimedAt.transform.position;
                isDashing = true;
                SkillDash(EnemyLocation);
            }
            else
            {
                StartCoroutine("NormalDash", dashDirection);
            }
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

    IEnumerator NormalDash(Vector3 dashDirection)
    {
        bool grounded = MoveScript.grounded;
        if (grounded)
        {
            isDashing = true;
            MoveScript.groundDrag = 0;
            playerRB.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(.2f);
            isDashing = false;
            MoveScript.groundDrag = MoveScript.OriginalDrag;
        }
        else
        {
            playerRB.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
        }
    }

    void SkillDash(Vector3 EnemyLocation)
    {
        Vector3 EnemyDistance = EnemyLocation - PlayerTrans.position;
        Vector3 EnemyDirection = (EnemyLocation - PlayerTrans.position).normalized;

        Debug.Log(EnemyDistance.magnitude);

        if(EnemyDistance.magnitude > SkillDashStoppingDistance)
        {
            playerRB.AddForce(EnemyDirection * dashSpeed, ForceMode.Impulse);
            
        }
        playerRB.velocity = new Vector3(0, 0, 0);
        playerRB.AddForce(EnemyDirection * 2f + playerRB.transform.up * 3f);
    }
}
