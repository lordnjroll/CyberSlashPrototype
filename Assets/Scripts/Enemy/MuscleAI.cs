using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuscleAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform PlayerLocation, FistGrabPoint;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, resetAttack;
    public bool hugPlayer, smashPlayer = false;
    public Animator enemyANIM;
    public GameObject Fist;

    private GrabPlayer grabPlayer;
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
}
