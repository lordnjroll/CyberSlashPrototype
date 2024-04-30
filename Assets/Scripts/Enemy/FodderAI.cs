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
    public GameObject HitboxObject;

    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    public bool isLaunched = false;
    public bool isDead = false;

    [Header("References")]
    public hp HPscript;
    public ParticleSystem DeathEffect;

    RaycastHit PlayerHit;
    //public GameObject Fodder;
    public Animator FodderANIM;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
    }

    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);

        rb = transform.gameObject.GetComponent<Rigidbody>();

        HPscript = Player.GetComponent<hp>();
        FodderANIM = transform.GetComponentInChildren<Animator>();
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
            transform.LookAt(PlayerLocation);
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (!playerInAttackRange && (!IsAttacking || AttackCD) && !isLaunched && !isDead)
        {
            FodderANIM.SetBool("isRunning", true);
            ChasePlayer();
            transform.LookAt(PlayerLocation);
            GetComponent<NavMeshAgent>().destination = PlayerLocation;
        }
        if (playerInAttackRange && !IsAttacking && !AttackCD && !isLaunched && !isDead)
        {
            
            AttackMode();
        }

        if(rb.velocity.y > 5)
        {
            //Debug.Log("fodder launched");
            //isLaunched = true;
            //transform.gameObject.GetComponent<Animator>().enabled = false;
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

            FodderANIM.SetBool("isRunning", false);
            FodderANIM.SetBool("isAttacking", true);
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
            transform.GetComponent<NavMeshAgent>().enabled = true;
            transform.gameObject.GetComponent<Animator>().enabled = true;
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
        Collider[] colliders = FodderModel.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        //GetComponent<Collider>().enabled = !state;
    }

    public void OnDeath()
    {
        Debug.Log("dead");
        HitboxObject.GetComponent<Collider>().isTrigger = true;
        setRigidbodyState(false);
        setColliderState(true);
        isDead = true;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponentInChildren<BoxCollider>().enabled = false;
        FodderModel.GetComponentInParent<Animator>().enabled = false;
        FodderModel.GetComponentInChildren<Rigidbody>().AddForce((transform.up * 10) + (transform.right * Random.Range(-50, 50) + (transform.forward * 50f)), ForceMode.VelocityChange);

        Instantiate(DeathEffect,new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation);
        Destroy(gameObject, 2f);
    }

    public void GotLaunched(float launchForce)
    {
        Debug.Log("got launched");
        isLaunched = true;
        transform.gameObject.GetComponent<Animator>().enabled = false;
        transform.GetComponentInParent<NavMeshAgent>().enabled = false;
        rb.velocity = new Vector3(0, launchForce, 0);
        
    }
}

