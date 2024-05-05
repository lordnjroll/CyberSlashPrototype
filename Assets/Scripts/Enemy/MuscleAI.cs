using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuscleAI : MonoBehaviour
{
    public int MuscleHP;
    public NavMeshAgent agent;
    public Transform PlayerLocation, FistGrabPoint;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public ParticleSystem DeathEffect;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public static bool hugPlayer, smashPlayer = false;
    public static bool resetAttack;
    public Animator enemyANIM;
    public GameObject Fist;
    public GameObject ShooterModel;
    public GameObject ShooterHead;
    public GameObject HitboxObject;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyANIM = GetComponent<Animator>();
        resetAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (!playerInSightRange && !playerInAttackRange)
        //{
        //    Destroy(this.gameObject, 2f);
        //}

        if (playerInSightRange && !playerInAttackRange) 
        {
            Debug.Log("Chasing Player");
            agent.SetDestination(PlayerLocation.position);
            enemyANIM.SetBool("isRunning", true);
            enemyANIM.SetBool("isMuscle_Trebuchet", false);
            enemyANIM.SetBool("isMuscle_Hughug", false);
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Debug.Log("Finded Player");
            resetAttack = false;
            int ran = Random.Range(0, 2);

            switch (ran)
            {
                case 0: AttackPlayer();
                    break;
                case 1: HugPlayer();
                    break;
            }
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(PlayerLocation.position);
        enemyANIM.SetBool("isRunning", false);
        enemyANIM.SetBool("isMuscle_Trebuchet", true);
        enemyANIM.SetBool("isMuscle_Hughug", false);
        smashPlayer = true;
    }

    void HugPlayer()
    {
        agent.SetDestination(PlayerLocation.position);
        enemyANIM.SetBool("isRunning", false);
        enemyANIM.SetBool("isMuscle_Trebuchet", false);
        enemyANIM.SetBool("isMuscle_Hughug", true);
        hugPlayer = true;
    }

    public void OnDeath()
    {
        Tutorial.killcount++;
        ScoreManager.score += 50;
        Debug.Log("dead");
        HitboxObject.GetComponent<Collider>().isTrigger = true;
        setRigidbodyState(false);
        setColliderState(true);
        transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        transform.GetComponentInChildren<Collider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        ShooterModel.GetComponentInChildren<Rigidbody>().AddForce((transform.up * 10) + (transform.right * Random.Range(-50, 50) + (transform.forward * 50f)), ForceMode.VelocityChange);

        Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation);
        Destroy(gameObject, 2f);

    }

    public void StrongOnDeath()
    {
        Debug.Log("dead");
        HitboxObject.GetComponent<Collider>().isTrigger = true;
        setRigidbodyState(false);
        setColliderState(true);
        transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        transform.GetComponentInChildren<Collider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        ShooterModel.GetComponentInChildren<Rigidbody>().AddForce((transform.up * 10) + (transform.right * Random.Range(-50, 50) + (transform.forward * 50f)), ForceMode.VelocityChange);

        Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation);
        Destroy(gameObject, 0.2f);

    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = ShooterModel.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = ShooterModel.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
    }
}
