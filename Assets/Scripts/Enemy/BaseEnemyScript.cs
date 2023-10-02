using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyScript : MonoBehaviour
{
    public float enemyHp = 1f;
    public float enemyMoveSpeedX = 1f;
    public float enemyMoveSpeedY = 1f;
    public Transform target;
    public float test = 1f;

    public float attankRange;
    private float pathUpdateDeadline;
    private EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        attankRange = enemyManager.navMeshAgent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //Run to target(Player)
            bool inRange = Vector3.Distance(transform.position, target.position) <= attankRange;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
        }
    }

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void LookAtTarget()
    {
        //Find Player
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    public void UpdatePath()
    {
        navMeshagent.SetDestination(target.position);\

        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + enemyManager.pathUpdateDelay;
            enemyManager.navMeshAgent.SetDestination(target.position);
        }
    }

    

    void attank()
    {

    }

    void Move()
    {

    }

    void Death()
    {
        if (enemyHp == 0)
        {

        }
    }
}
