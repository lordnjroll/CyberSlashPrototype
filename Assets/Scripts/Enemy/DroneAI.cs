using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public GameObject ThePlayer;

    public GameObject DroneHead;
    public Transform laserOrigin;

    public GameObject Cube;
    public DroneCubeScript cubeScript;
    public Transform CubeSpawnPoint;
    public LayerMask PlayerLayer;
    public Rigidbody DroneRB;
    public LayerMask GrounpLayer;

    public int FlyerAttackRange;
    public int FlyerMoveSpeed;

    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;

    public ParticleSystem FireEffect, HitEffect;

    RaycastHit HeightRaycast;
    private void Awake()
    {
        
    }

    void Start()
    {
        cubeScript = Cube.GetComponent<DroneCubeScript>();
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        AttackCD = false;
        InvokeRepeating("KeepDroneAboveAir", 0, 0.01f);
        DroneRB = DroneHead.GetComponent<Rigidbody>();
    }

    public void FlyerChasingPlayer()
    {
        DroneHead.transform.position = Vector3.MoveTowards(DroneHead.transform.position, ThePlayer.transform.position, FlyerMoveSpeed * Time.deltaTime);
        DroneHead.transform.LookAt(ThePlayer.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        playerInAttackRange = Physics.CheckSphere(transform.position, FlyerAttackRange, PlayerLayer);

        //To Look at the player when winding up an attack
        if (AttackWindingUp)
        {
            DroneHead.transform.LookAt(ThePlayer.transform.position);
            
        }

        if ((!playerInAttackRange && !IsAttacking || AttackCD) || (DroneHead.transform.position - ThePlayer.transform.position).magnitude > 20)
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
        yield return new WaitForSeconds(1f);
        Cube.SetActive(true);
        Cube.transform.position = CubeSpawnPoint.transform.position;
        cubeScript.GetPlayerLocation(ThePlayer.transform.position);
        //Debug.Log("Stop Looking at the player");
        AttackWindingUp = false;
        yield return new WaitForSeconds(.7f);

        //Debug.Log("Flyer fire");

        IsAttacking = false;
        AttackCD = true;

        //Laser effect
        /*Instantiate(FireEffect, laserLine.GetPosition(0), Quaternion.identity);
        Destroy(FireEffect, 1f);

        Instantiate(FireEffect, laserLine.GetPosition(1), Quaternion.identity);
        Destroy(FireEffect, 1f);*/

        StartCoroutine("AttackCoolDown");
        StopCoroutine("AttackWindUp");
    }

    //Flyer attack cool down
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(5f);
        Cube.SetActive(false);
        AttackCD = false;
    }

    public void KeepDroneAboveAir()
    {
        if(Physics.Raycast(DroneHead.transform.position, -DroneHead.transform.up, 7f, GrounpLayer))
        {
            Debug.Log("Flying up");
            DroneRB.AddForce(DroneRB.transform.up * 2, ForceMode.Force);
        }
        else if((DroneHead.transform.position.y - ThePlayer.transform.position.y) > 7)
        {
            Debug.Log("dropping");
            DroneRB.AddForce(-DroneRB.transform.up * 2, ForceMode.Force);
        }
    }
}
