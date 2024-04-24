using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FodderAI : MonoBehaviour
{
    private Vector3 PlayerLocation;
    private GameObject Player;
    public LayerMask PlayerLayer;

    public Rigidbody rb;    

    [Header("Fodder Stats")]
    public float AttackRange = 5;
    public GameObject ShooterProjectile;
    public int FodderMoveSpeed;
    public float ProjectileSpeed;
    public float chargeSpeed;
    public GameObject FodderHead;
    public GameObject FodderModel;

    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    public bool isLaunched = false;
    public bool isDead = false;

    [Header("References")]
    public hp HPscript;

    RaycastHit PlayerHit;
    public Animator enemyANIM;
    //public GameObject Fodder;
    public Animator FodderANIM;

    private void Awake()
    {
        
    }

    void Start()
    {
        setRigidbodyState(true);
        setColliderState(true);
        Player = GameObject.FindWithTag("Player").gameObject;

        HPscript = Player.GetComponent<hp>();
        FodderANIM = GetComponentInChildren<Animator>();
        //enemyANIM = Fodder.GetComponent<Animator>();
        if (this.gameObject.tag == "FodderTag")
        {

        }

        //Starting the chase
        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        //enemyANIM.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Assets/Animation/BaseEnemyAnimation/BaseIdle", typeof(RuntimeAnimatorController));
        PlayerLocation = Player.transform.position;

        if (AttackWindingUp && !isLaunched && !isDead)
        {
            FodderHead.transform.LookAt(PlayerLocation);
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (!playerInAttackRange && !IsAttacking || AttackCD && !isLaunched && !isDead)
        {
            FodderANIM.SetBool("isRunning", true);
            //ChasePlayer();
            transform.LookAt(PlayerLocation);
            GetComponent<NavMeshAgent>().destination = PlayerLocation;
        }
        if (playerInAttackRange && !IsAttacking && !AttackCD && !isLaunched && !isDead)
        {
            FodderANIM.SetBool("isRunning", false);
            FodderANIM.SetBool("isAttacking", true);
            AttackMode();
        }

        if(rb.velocity.y > 5)
        {
            //Debug.Log("fodder launched");
            isLaunched = true;
        }

        
    }

    IEnumerator ChasePlayer()
    {
        if (!isDead)
        {
            GetComponent<NavMeshAgent>().destination = PlayerLocation;
            yield return null;
        }
    }

    public void AttackMode()
    {
        IsAttacking = true;
        StartCoroutine("AttackWindUp");
        AttackWindingUp = true;
    }

    IEnumerator AttackWindUp()
    {
        if (!isDead)
        {
            //Starts Charging
            yield return new WaitForSeconds(.5f);

            //Stop Looking at the player
            AttackWindingUp = false;
            yield return new WaitForSeconds(.1f);

            //Charge at the Player
            rb.AddForce(rb.transform.forward * chargeSpeed, ForceMode.VelocityChange);
            //Debug.Log("charged");


            IsAttacking = false;
            AttackCD = true;


            StartCoroutine("AttackCoolDown");
            StopCoroutine("AttackWindUp");
        }
    }

    //Flyer attack cool down
    IEnumerator AttackCoolDown()
    {
        
        yield return new WaitForSeconds(.5f);
        FodderANIM.SetBool("isAttacking", false);
        AttackCD = false;
    }

    public void Killed()
    {
        /*if(ememyHP == 0)
        {
            Destroy(this.gameObject);
        }*/
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.layer == 7 && IsAttacking)
        {
            HPscript.HealthLost();
        }

        if(collision.transform.gameObject.layer == 3 && isLaunched)
        {
            this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            isLaunched = false;
        }

        if(collision.transform.gameObject.layer == 13)
        {
            Destroy(this);
        }
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = FodderModel.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    public void OnDeath()
    {
        setRigidbodyState(false);
        setColliderState(true);
        isDead = true;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponentInChildren<BoxCollider>().enabled = false;
        FodderModel.GetComponent<Animator>().enabled = false;
        FodderModel.GetComponentInChildren<Rigidbody>().AddForce((transform.up * 50) + (transform.right * Random.Range(-50, 50)), ForceMode.VelocityChange);

        Destroy(gameObject, 2f);
    }
}

