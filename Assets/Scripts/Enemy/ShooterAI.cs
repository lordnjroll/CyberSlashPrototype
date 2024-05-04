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
    public GameObject Marking;
    public ParticleSystem DeathEffect;

    [Header("Shooter Stats")]
    public float ShootRange = 15;
    public GameObject ShooterProjectile;
    public int ShooterMoveSpeed;
    public float ProjectileSpeed;
    public GameObject ShooterModel;
    public GameObject ShooterHead;
    public GameObject HitboxObject;

    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    public bool isLaunched = false;
    public bool isDead = false;
    public Rigidbody shooterRB;

    RaycastHit PlayerHit;
    public Animator enemyANIM;
    [SerializeField] private AudioClip ShootAudio;
    [SerializeField] private AudioSource Source;

    [SerializeField] private float ememyHP = 10f;
    private void Awake()
    {
        // laserLine = GetComponent<LineRenderer>();
    }

    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);

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
        if(!isLaunched || !isDead)
        {
            if (AttackWindingUp)
            {
                ShooterHead.transform.LookAt(PlayerLocation);
                //laserLine.SetPosition(0, laserOrigin.position);
                //laserLine.SetPosition(1, PlayerLocation.position);
                //Vector3 rayOrigin = playerLocation.position;
            }

            playerInAttackRange = Physics.CheckSphere(transform.position, ShootRange, PlayerLayer);

            if (!playerInAttackRange && !IsAttacking || AttackCD  && GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
            {
                enemyANIM.SetTrigger("isRunning");
                //ChasePlayer();
                ShooterHead.transform.LookAt(PlayerLocation);
                transform.LookAt(new Vector3(PlayerLocation.x, transform.position.y, PlayerLocation.z));
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = PlayerLocation;
            }
            if (playerInAttackRange && !IsAttacking && !AttackCD )
            {
                enemyANIM.SetTrigger("isAttacking");
                AttackMode();
            }
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
        
        yield return new WaitForSeconds(2f);

        AttackWindingUp = false;
        yield return new WaitForSeconds(.1f);

        //Shooting the projectile
        var projectile = Instantiate(ShooterProjectile, ShootPoint.position, ShootPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = (PlayerLocation - transform.position ).normalized * ProjectileSpeed;

        IsAttacking = false;
        AttackCD = true;
        Source.PlayOneShot(ShootAudio);

        StartCoroutine("AttackCoolDown");
        StopCoroutine("AttackWindUp");
    }

    //Flyer attack cool down
    IEnumerator AttackCoolDown()
    {
        
        yield return new WaitForSeconds(3f);
        AttackCD = false;
    }

    public void OnDeath()
    {
        Tutorial.killcount++;
        Debug.Log("dead");
        HitboxObject.GetComponent<Collider>().isTrigger = true;
        isDead = true;
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
        isDead = true;
        setRigidbodyState(false);
        setColliderState(true);
        transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        transform.GetComponentInChildren<Collider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        ShooterModel.GetComponentInChildren<Rigidbody>().AddForce((transform.up * 10) + (transform.right * Random.Range(-50, 50) + (transform.forward * 50f)), ForceMode.VelocityChange);

        Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation);
        Destroy(gameObject, 0.2f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 3 && isLaunched)
        {
            this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            transform.gameObject.GetComponent<Animator>().enabled = true;
            shooterRB.isKinematic = true;
            isLaunched = false;
        }

        if (collision.transform.gameObject.layer == 13)
        {
            Destroy(this);
        }
    }

    public void GotLaunched(float launchForce)
    {
        Debug.Log("got launched");
        isLaunched = true;
        shooterRB.isKinematic = false;
        transform.gameObject.GetComponent<Animator>().enabled = false;
        transform.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        shooterRB.velocity = new Vector3(0, launchForce, 0);

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

        //GetComponent<Collider>().enabled = !state;
    }
}
