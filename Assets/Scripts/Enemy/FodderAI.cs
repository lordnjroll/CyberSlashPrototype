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

    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    public bool isLaunched = false;

    [Header("References")]
    public hp HPscript;

    RaycastHit PlayerHit;
    private void Awake()
    {
        
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;

        HPscript = Player.GetComponent<hp>();

        //Starting the chase
        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = Player.transform.position;

        if (AttackWindingUp && !isLaunched)
        {
            transform.LookAt(PlayerLocation);
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (!playerInAttackRange && !IsAttacking || AttackCD && !isLaunched)
        {
            //ChasePlayer();
            transform.LookAt(PlayerLocation);
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = PlayerLocation;
        }
        if (playerInAttackRange && !IsAttacking && !AttackCD && !isLaunched)
        {
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
        
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = PlayerLocation;
        yield return null;
    }

    public void AttackMode()
    {
        IsAttacking = true;
        StartCoroutine("AttackWindUp");
        AttackWindingUp = true;
    }

    IEnumerator AttackWindUp()
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

    //Flyer attack cool down
    IEnumerator AttackCoolDown()
    {
        
        yield return new WaitForSeconds(.5f);
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
}

