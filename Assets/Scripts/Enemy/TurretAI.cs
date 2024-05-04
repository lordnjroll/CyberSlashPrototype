using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TurretAI : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject TurretBarrel;
    public Transform laserOrigin;
    public LineRenderer laserLine;
    public ParticleSystem DeathEffect;

    public LayerMask PlayerLayer;

    public int FlyerAttackRange;

    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;
    private hp HpScript;
    private bool isDead;

    [SerializeField] private AudioClip TurretAudio;
    [SerializeField] private AudioSource Source;
    //public ParticleSystem FireEffect, HitEffect;

    RaycastHit PlayerHit;
    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        HpScript = ThePlayer.GetComponent<hp>();
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        AttackCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, FlyerAttackRange, PlayerLayer);
        if (!isDead)
        {
            //To Look at the player when winding up an attack
            if (AttackWindingUp)
            {
                TurretBarrel.transform.LookAt(ThePlayer.transform.position);
                laserLine.SetPosition(0, laserOrigin.transform.position);
                laserLine.SetPosition(1, ThePlayer.transform.position);
                //Vector3 rayOrigin = playerLocation.position;
            }

            if (playerInAttackRange && !IsAttacking && !AttackCD)
            {
                AttackMode();
            }
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
        laserLine.enabled = true;
        //Debug.Log("Starts Charging");
        yield return new WaitForSeconds(3f);

        //Debug.Log("Stop Looking at the player");
        AttackWindingUp = false;
        yield return new WaitForSeconds(.3f);

        //Debug.Log("Flyer fire");
        
        //shoot raycast
        RaycastHit TurretTarget;
        if(Physics.Raycast(TurretBarrel.transform.position, TurretBarrel.transform.forward, out TurretTarget, FlyerAttackRange))
        {
            Debug.Log("turret shot " + TurretTarget.transform.gameObject);

            if(TurretTarget.transform.gameObject.layer == 7)
            {
                Source.PlayOneShot(TurretAudio);
                Debug.Log("player hit");
                //hp lost here
                HpScript.HealthLost();
            }
        }

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
        yield return new WaitForSeconds(3f);
        AttackCD = false;
    }

    public void OnDeath()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void StrongOnDeath()
    {
        isDead = true;
        Destroy(gameObject, 0.1f);
    }
}
