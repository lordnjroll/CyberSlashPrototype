using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    private Vector3 PlayerLocation;
    public Transform ShooterLocation;
    public Transform ShootPoint;
    private GameObject Player;
    //public Transform laserOrigin;
    public LayerMask PlayerLayer;

    [Header("Shooter Stats")]
    public float ShootRange = 15;
    public GameObject ShooterProjectile;
    public int ShooterMoveSpeed;
    public float ProjectileSpeed;

    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    public bool isLaunched = false;
    public LineRenderer laserLine;
    public Rigidbody shooterRB;

    RaycastHit PlayerHit;

    [SerializeField] private float ememyHP = 10f;
    private void Awake()
    {
        // laserLine = GetComponent<LineRenderer>();
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        shooterRB = this.GetComponent<Rigidbody>();
        shooterRB.isKinematic = true;

        //Starting the chase
        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = Player.transform.position;

        if (AttackWindingUp && !isLaunched && !isLaunched)
        {
            transform.LookAt(PlayerLocation);
            //laserLine.SetPosition(0, laserOrigin.position);
            //laserLine.SetPosition(1, PlayerLocation.position);
            //Vector3 rayOrigin = playerLocation.position;
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, ShootRange, PlayerLayer);

        if (!playerInAttackRange && !IsAttacking || AttackCD && !isLaunched && GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
        {
            //ChasePlayer();
            transform.LookAt(PlayerLocation);
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = PlayerLocation;
        }
        if (playerInAttackRange && !IsAttacking && !AttackCD && !isLaunched)
        {
            AttackMode();
        }

        if (shooterRB.velocity.y > 7.5)
        {
            //Debug.Log("shooter launched");
            //this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            shooterRB.isKinematic = false;
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
        //laserLine.enabled = true;
        //Debug.Log("Starts Charging");
        yield return new WaitForSeconds(2.5f);

        //Debug.Log("Stop Looking at the player");
        AttackWindingUp = false;
        yield return new WaitForSeconds(.1f);

        //Debug.Log("Shooter fire");
        //Shooting the projectile
        var projectile = Instantiate(ShooterProjectile, ShootPoint.position, ShootPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = ShootPoint.forward * ProjectileSpeed;


        IsAttacking = false;
        AttackCD = true;

        //Laser effect
      /*Instantiate(FireEffect, laserLine.GetPosition(0), Quaternion.identity);
        Destroy(FireEffect, 1f);

        Instantiate(FireEffect, laserLine.GetPosition(1), Quaternion.identity);
        Destroy(FireEffect, 1f);*/

        laserLine.enabled = false;

        StartCoroutine("AttackCoolDown");
        StopCoroutine("AttackWindUp");
    }

    //Flyer attack cool down
    IEnumerator AttackCoolDown()
    {
        
        yield return new WaitForSeconds(4f);
        AttackCD = false;
    }

    public void Killed()
    {
        if(ememyHP == 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 3 && isLaunched)
        {
            this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            shooterRB.isKinematic = true;
            isLaunched = false;
        }

        if (collision.transform.gameObject.layer == 13)
        {
            Destroy(this);
        }
    }
}
