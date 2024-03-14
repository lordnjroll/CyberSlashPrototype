using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FodderAI : MonoBehaviour
{
    private Vector3 PlayerLocation;
    //public Transform ShooterLocation;
    //public Transform ShootPoint;
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
    //public LineRenderer laserLine;

    RaycastHit PlayerHit;

    //[SerializeField] private float ememyHP = 10f;
    private void Awake()
    {
        
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;

        //Starting the chase
        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = Player.transform.position;

        if (AttackWindingUp)
        {
            transform.LookAt(PlayerLocation);
            //laserLine.SetPosition(0, laserOrigin.position);
            //laserLine.SetPosition(1, PlayerLocation.position);
            //Vector3 rayOrigin = playerLocation.position;
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (!playerInAttackRange && !IsAttacking || AttackCD)
        {
            //ChasePlayer();
            transform.LookAt(PlayerLocation);
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = PlayerLocation;
        }
        if (playerInAttackRange && !IsAttacking && !AttackCD)
        {
            AttackMode();
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
        rb.AddForce(rb.transform.forward * chargeSpeed, ForceMode.Impulse);

        IsAttacking = false;
        AttackCD = true;

        //Laser effect
      /*Instantiate(FireEffect, laserLine.GetPosition(0), Quaternion.identity);
        Destroy(FireEffect, 1f);

        Instantiate(FireEffect, laserLine.GetPosition(1), Quaternion.identity);
        Destroy(FireEffect, 1f);*/

        //laserLine.enabled = false;

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
}

