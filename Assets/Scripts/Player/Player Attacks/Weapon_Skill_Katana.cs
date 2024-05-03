using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Skill_Katana : MonoBehaviour
{
    [HideInInspector]

    [Header("Attack setting")]
    public Transform PlayerAttackStartPoint;
    public float AttackRange;
    public int PlayerDamage;
    public float AttackCD = 1f;
    public bool isAttackCD = false;

    [Header("Dash settings")]
    public float dashSpeed;
    public float SkillDashSpeed;
    public float DashCutThreshold; //how close can the player get before dashing into the enemy
    public float SkillDashStoppingDistance; //how close will the player get to the enemy after skill dashing
    public float SkillDashRange; //How far can the player skill dash into the enemy
    public float DashCutRange; //How far can the player dash into the enemy
    private RaycastHit DashCutDetector;
    public bool isDashing = false;// normal dash
    public bool isSkillDashing = false;// skill dash
    public bool DashingCD = false;
    public bool isStrongAttackOn = false;
    public KeyCode MovementBtn = KeyCode.LeftShift;

    [Header("Camera Effects")]
    public float DefaultFOV;
    public float DashingFOV;
    public float SkillDashFOV;
    public float wallrunFOV;

    [Header("Knife throwing settings")]
    //public GameObject Kunai;
    public Camera mainCamera;
    public KeyCode secondarySkillbtn = KeyCode.Mouse1;
    public float secondarySkillCD = 0.5F;
    public bool isSecondarySkillCD = false;

    [Header("Enemy Settings")]
    public int BaseEnemyHP;
    public int ShieldEnemyHP;
    public int MuscleEnemyHP;
    
    private List<GameObject> MarkedTargetes = new List<GameObject>();
    private int TotalEnemiesMarked = 0;

    [Header("References")]
    public Rigidbody playerRB;
    public Transform PlayerTrans; //pog
    public Transform CamHolder;
    public Image markImg;
    public Canvas markerCanvas;
    public GameObject markerParent;
    public LayerMask EnemyHitboxLayer;
    public GameObject MarkingObject;

    [Header("Enemy Refernences")]
    private GameObject Enemy;
    public GameObject FodderEnemy;
    public GameObject ShieldEnemy;
    public GameObject MuscleEnemy;
    public GameObject TurretEnemy;
    public GameObject DroneEnemy;
    private RaycastHit RayEnemyAimedAt; // the enemy that the player is looking at
    private GameObject EnemyAimedAt;
    private Vector3 SkillDashTarget;

    [Header("Audio")]
    public AudioClip Sword_Swing;
    public AudioClip Kunai_Throw;
    public AudioClip Target_Marked;
    public AudioSource Source;

    [Header("Animation")]
    public GameObject katanaModel;
    public Animator katanaAnimator;
    public GameObject SwordTrail;
    public GameObject DaggerObject;

    //Script references
    private mesh_destroy DismentleScript;
    private PlayerAiming camScript;
    private ShooterProjectile ReflectScript;
    private ScoreManager ScoreScript;
    private FastMovementScript MoveScript;
    private hp hpScript;
    private FodderAI fodderScript;
    private ShooterAI shooterScript;
    private TurretAI turretScript;
    private DroneAI droneScript;
    private KillManager KillScript;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        //Debug.Log("looking at " + EnemyAimedAt);

        #region Inistialize references
        katanaAnimator = katanaModel.GetComponent<Animator>();
        ScoreScript = GetComponent<ScoreManager>();
        MoveScript = GetComponent<FastMovementScript>();
        hpScript = GetComponent<hp>();
        camScript = CamHolder.GetComponent<PlayerAiming>();
        //ReflectScript = GetComponent<ShooterProjectile>();

        fodderScript = FodderEnemy.GetComponent<FodderAI>();
        #endregion
        //Enemy = GameObject.FindWithTag("EnemyTag").gameObject;

        MarkedTargetes.Clear();

        DefaultFOV = MoveScript.DefaultFOV;
        DashingFOV = DefaultFOV - 1f;
        SkillDashFOV = DefaultFOV + 20f;
        wallrunFOV = DefaultFOV + 5f;
    }

    void Update()
    {
        //Debug.DrawLine(EnemyAimedAt);
        //Debug.Log("Looking at " + EnemyAimedAt);
        //TotalEnemiesMarked = MarkedTargetes.Count;
        //DashInput();
        PlayerInput();
        PlayerAttack();
        
    }
    private void FixedUpdate()
    {
        
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttackCD)
        {
            if (!isStrongAttackOn)
            {
                Source.PlayOneShot(Sword_Swing);
                //Debug.Log("clicked");
                RaycastHit meleehit;
                isAttackCD = true;
                SwordTrail.SetActive(true);
                katanaAnimator.SetBool("isAttacking", true);
                StartCoroutine("AttackCDCountDown");
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out meleehit, AttackRange))
                {
                    Source.PlayOneShot(Sword_Swing);
                    if (meleehit.transform.tag == "FodderTag")
                    {
                        Enemy = meleehit.transform.gameObject;
                        fodderScript = Enemy.GetComponent<FodderAI>();
                        fodderScript.OnDeath();


                        //ScoreScript.GetScore();
                    }
                    if (meleehit.transform.tag == "Shooter")
                    {
                        Enemy = meleehit.transform.gameObject;
                        shooterScript = Enemy.GetComponentInParent<ShooterAI>();
                        shooterScript.OnDeath();

                        //ScoreScript.GetScore();
                    }

                    if (meleehit.transform.tag == "ShieldEnemy")
                    {

                        ShieldEnemyHP = ShieldEnemyHP - PlayerDamage;
                        DismentleScript = ShieldEnemy.GetComponent<mesh_destroy>();
                        //Debug.Log("Hit Shield");

                    }

                    if (meleehit.transform.tag == "MuscleEnemy")
                    {
                        MuscleEnemyHP = MuscleEnemyHP - PlayerDamage;
                        DismentleScript = MuscleEnemy.GetComponent<mesh_destroy>();
                    }

                    if (meleehit.transform.tag == "TurretTag")
                    {
                        Enemy = meleehit.transform.gameObject;
                        turretScript = Enemy.GetComponentInParent<TurretAI>();
                        turretScript.OnDeath();
                    }

                    if (meleehit.transform.tag == "EnemyProjectileTag")
                    {
                        katanaAnimator.SetBool("isParrying", true);
                        StartCoroutine("HitStop");
                        GameObject EnemyProjectile = meleehit.transform.gameObject;
                        ReflectScript = EnemyProjectile.GetComponent<ShooterProjectile>();
                        ReflectScript.OnPlayerParry(mainCamera.transform.forward);

                    }
                }
                
            }
            else if(isStrongAttackOn)
            {
                Source.PlayOneShot(Sword_Swing);
                RaycastHit Strongmeleehit;
                isAttackCD = true;
                SwordTrail.SetActive(true);
                katanaAnimator.SetBool("isParrying", true);
                StartCoroutine("AttackCDCountDown");

                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out Strongmeleehit, AttackRange))
                {
                    if (Strongmeleehit.transform.gameObject.layer == 8)
                    {
                        StartCoroutine("HitStop");
                        Collider[] colliders = Physics.OverlapSphere(Strongmeleehit.transform.position, 3f, 1<<8);
                        foreach (Collider c in colliders)
                        {
                            KillScript = Strongmeleehit.transform.GetComponentInParent<KillManager>();
                            KillScript.StrongAttack(c.transform.gameObject);            

                        }

                    }
                        
                }
                
            }

        }
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(MovementBtn) && !MoveScript.isWallRunning && !isDashing && !isSkillDashing && !DashingCD)
        {
            Dash();
            //Debug.Log("dash function called");
        }


        if (Input.GetKeyDown(secondarySkillbtn) && !isSecondarySkillCD)
        {
            secondarySkill();
        }

        if (MarkedTargetes != null)
        {
            if (Physics.Raycast(PlayerTrans.position, mainCamera.transform.forward, out RayEnemyAimedAt, SkillDashRange))
            {
                if(RayEnemyAimedAt.transform.gameObject.layer == 8)
                {
                    EnemyAimedAt = RayEnemyAimedAt.transform.gameObject;
                }
                else 
                {
                    EnemyAimedAt = null;
                }

            }
            else
            {
                EnemyAimedAt = null;
            }
        }
    }

    void secondarySkill()
    {
        Source.PlayOneShot(Kunai_Throw);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit knifehit;

        var projectile = Instantiate(DaggerObject, PlayerTrans.position, mainCamera.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = mainCamera.transform.forward * 75f;

        isSecondarySkillCD = true;
        StartCoroutine("SkillCDCountDown");
        //Debug.Log("throw knife");
        if (Physics.Raycast(ray, out knifehit))
         {
             GameObject RayCastHitObject = knifehit.transform.gameObject;
             //Debug.Log("knife hit");
             if (RayCastHitObject.layer == 8)
             {
                //Debug.Log("Object marked: " + RayCastHitObject.name);
                MarkedTargetes.Add(RayCastHitObject);
                Source.PlayOneShot(Target_Marked);
                Instantiate(MarkingObject, new Vector3(0, 1, 0), Quaternion.identity, RayCastHitObject.transform);
                //RayCastHitObject.AddComponent<GameObject>(MarkingObject);
            }

         }
        
    }

    void Dash()
    {
        
        Transform forwardT = PlayerTrans;

        Vector3 dashDirection = GetDirection(forwardT);

        //Physics.Raycast(PlayerTrans.position, mainCamera.transform.forward, out RayEnemyAimedAt, DashCutRange);

        
        if (MarkedTargetes.Contains(EnemyAimedAt))
        {
            SkillDashTarget = EnemyAimedAt.transform.position;
            StartCoroutine("SkillDash");
        }
        else
        {
            StartCoroutine("NormalDash", dashDirection);

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
        DashingCD = true;
        if (grounded)
        {
            camScript.DoFOVQuick(DashingFOV);
            isDashing = true;
            MoveScript.groundDrag = 0;
            playerRB.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(.2f);
            isDashing = false;
            MoveScript.groundDrag = MoveScript.OriginalDrag;
            camScript.DoFOVQuick(DefaultFOV);
            StartCoroutine("DashCDCounter");
        }
        else
        {
            camScript.DoFOVQuick(DashingFOV);
            isDashing = true;
            playerRB.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(.2f);
            isDashing = false;
            camScript.DoFOVQuick(DefaultFOV);
            StartCoroutine("DashCDCounter");
        }
    }

    IEnumerator SkillDash()
    {
        isSkillDashing = true;
        //Debug.Log("Skill dash function called");
        float travelTime = 0.5f;
        float elapsedTime = 0f;
        Vector3 EnemyDistance = SkillDashTarget - PlayerTrans.position;
        Vector3 EnemyDirection = (SkillDashTarget - PlayerTrans.position).normalized;

        elapsedTime += Time.deltaTime;
        float t = Mathf.SmoothStep(0.2f, 1f, (elapsedTime / travelTime));

        //Debug.Log(EnemyDistance.magnitude);
        //StartCoroutine("SkillStartCameraEffects");
        camScript.DoFOV(SkillDashFOV);
        while (EnemyDistance.magnitude > SkillDashStoppingDistance)
        {
            EnemyDistance = SkillDashTarget - PlayerTrans.position;
            //Debug.Log("skill dashing");
            playerRB.useGravity = false;
            PlayerTrans.position = Vector3.Lerp(PlayerTrans.transform.position, SkillDashTarget, t);
            //Debug.Log("skill dashing");

            yield return new WaitForSeconds(0.01f);
            

        }
        //Debug.Log(" skill dash end");
        StartCoroutine("ToggleStrongAttack");

        camScript.DoFOV(DefaultFOV);

        //small upward force after reaching the target
        playerRB.velocity = new Vector3(0, 0, 0);
        playerRB.AddForce(EnemyDirection * 2f + playerRB.transform.up * 8f, ForceMode.Impulse);

        //reset all settings to normal
        MoveScript.readyToJump = true;
        MoveScript.remainingJump = MoveScript.Maxjumps;
        elapsedTime = 0;
        playerRB.useGravity = true;
        isSkillDashing = false;
        
        //Debug.Log("exit skill dash");
        yield return null;

        //CancelInvoke("SkillDash");


    }

    IEnumerator DashCDCounter()
    {
        yield return new WaitForSeconds(.5f);
        DashingCD = false;
    }

    IEnumerator HitStop()
    {
        bool waiting = false;
        if (!waiting)
        {
            waiting = true;
            Time.timeScale = 0.01f;
            yield return new WaitForSeconds(0.0035f);
            Time.timeScale = 1f;
            waiting = false;
        }
        katanaAnimator.SetBool("isParrying", false);
    }

    IEnumerator AttackCDCountDown()
    {
        yield return new WaitForSeconds(0.01f);
        katanaAnimator.SetBool("isAttacking", false);
        katanaAnimator.SetBool("isParrying", false);
        yield return new WaitForSeconds(AttackCD);
        StartCoroutine("SwordSwingAnimation2");
        SwordTrail.SetActive(false);
        isAttackCD = false;
        
    }

    IEnumerator SwordSwingAnimation2()
    {
        katanaAnimator.SetBool("HasAttacked", true);
        yield return new WaitForSeconds(0.7f);
        katanaAnimator.SetBool("HasAttacked", false);
    }

    IEnumerator SkillCDCountDown()
    {
        yield return new WaitForSeconds(secondarySkillCD);
        isSecondarySkillCD = false;
    }

    IEnumerator ToggleStrongAttack()
    {
        isStrongAttackOn = true;
        yield return new WaitForSeconds(0.5f);
        isStrongAttackOn = false;
    }
}
