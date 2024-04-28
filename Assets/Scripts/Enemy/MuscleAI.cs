using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuscleAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform PlayerLocation;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator enemyANIM;
    public GameObject Fist;
    private void Awake()
    {
        PlayerLocation = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyANIM = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();

        if (playerInSightRange && playerInAttackRange)
        {
            int ran = Random.Range(0, 2);

            switch (ran)
            {
                case 1: AttackPlayer();
                    break;
                case 2: HugPlayer();
                    break;
            }
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(PlayerLocation.position);
        enemyANIM.SetBool("isRunning", true);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(PlayerLocation);
        enemyANIM.SetBool("isMuscle_Trebuchet", true);
    }

    void HugPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(PlayerLocation);
        enemyANIM.SetBool("isMuscle_Hughug", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        other = Fist.GetComponent<SphereCollider>();

        if(other.gameObject.tag == "Player")
        {
            hp.Hp -= 2;
        }
    }
}
