using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FlyerBossAI : MonoBehaviour
{
    public GameObject ThePlayer;

    public Transform playerLocation;
    public Transform laserOrigin;
    public LineRenderer laserLine;

    public LayerMask PlayerLayer;

    public int FlyerAttackRange;
    public int FlyerMoveSpeed;

    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;

    public ParticleSystem FireEffect, HitEffect;

    RaycastHit PlayerHit;
    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Start()
    {
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        AttackCD = false;
    }

    public void FlyerChasingPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, FlyerMoveSpeed * Time.deltaTime);
        transform.LookAt(playerLocation);
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        playerInAttackRange = Physics.CheckSphere(transform.position, FlyerAttackRange, PlayerLayer);

        //To Look at the player when winding up an attack
        if (AttackWindingUp)
        {
            transform.LookAt(playerLocation);
            laserLine.SetPosition(0, laserOrigin.position);
            laserLine.SetPosition(1, playerLocation.position);
            //Vector3 rayOrigin = playerLocation.position;
        }

        if (!playerInAttackRange && !IsAttacking || AttackCD)
        {
            FlyerChasingPlayer();
        }

        if (playerInAttackRange && !IsAttacking && !AttackCD)
        {
            AttackMode();
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
        yield return new WaitForSeconds(.7f);

        //Debug.Log("Flyer fire");

        IsAttacking = false;
        AttackCD = true;

        //Laser effect
        Instantiate(FireEffect, laserLine.GetPosition(0), Quaternion.identity);
        Destroy(FireEffect, 1f);

        Instantiate(FireEffect, laserLine.GetPosition(1), Quaternion.identity);
        Destroy(FireEffect, 1f);

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
}
