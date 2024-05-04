using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FlyerBossAI : MonoBehaviour
{
    private GameObject ThePlayer;

    public Transform laserOrigin;
    public LineRenderer laserLine;

    public LayerMask PlayerLayer;

    public int FlyerAttackRange;
    public int FlyerMoveSpeed;

    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;

    public ParticleSystem FireEffect, HitEffect;

    public GameObject HexAttack;

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


    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, FlyerAttackRange, PlayerLayer);
        transform.LookAt(ThePlayer.transform);

        //To Look at the player when winding up an attack
        if (AttackWindingUp)
        {
            transform.LookAt(ThePlayer.transform);
            laserLine.SetPosition(0, laserOrigin.position);
            laserLine.SetPosition(1, ThePlayer.transform.position);
            //Vector3 rayOrigin = playerLocation.position;
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
