using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class FlyerBossAI : MonoBehaviour
{
    private GameObject ThePlayer;

    public Transform Eye;
    public Transform laserOrigin;
    public LineRenderer laserLine;

    public LayerMask PlayerLayer;

    public int FlyerAttackRange;
    public int FlyerMoveSpeed;

    public bool playerInSightRange, playerInAttackRange, IsAttacking, AttackCD;
    private bool AttackWindingUp;

    public ParticleSystem FireEffect, HitEffect, DeathExplosion, shatterEffect;
    public GameObject ChargeEffect;

    public GameObject BossProjectile;
    public GameObject HexAttack;
    public bool isDead = false;
    int i;

    RaycastHit PlayerHit;
    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        ChargeEffect.SetActive(false);
    }

    void Start()
    {
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        AttackCD = false;
        InvokeRepeating("HexAOEattack", 0, 1f);
        
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
            //AttackMode();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine("DeathAnimation");
        }
    }
    void HexAOEattack()
    {
        i += 1;
        if(i >= 8)
        {
            Debug.Log("hex attack called");
            StartCoroutine("HexAOEattackSecond");
            i = 0;
        }
    }

    IEnumerator HexAOEattackSecond()
    {
        Instantiate(HexAttack, new Vector3(ThePlayer.transform.position.x, ThePlayer.transform.position.y - 3f, ThePlayer.transform.position.z), ThePlayer.transform.rotation);
        yield return new WaitForSeconds(0.7f);
        Instantiate(HexAttack, new Vector3(ThePlayer.transform.position.x, ThePlayer.transform.position.y - 3f, ThePlayer.transform.position.z), ThePlayer.transform.rotation);
        yield return new WaitForSeconds(0.7f);
        Instantiate(HexAttack, new Vector3(ThePlayer.transform.position.x, ThePlayer.transform.position.y - 3f, ThePlayer.transform.position.z), ThePlayer.transform.rotation);
        yield return new WaitForSeconds(0.7f);
    }

    public void OnDeath()
    {
        StartCoroutine("DeathAnimation");
    }

    public IEnumerator DeathAnimation()
    {
        isDead = true;
        transform.DOShakePosition(3, 5, 90, 90, true, false);
        yield return new WaitForSeconds(3);
        Instantiate(DeathExplosion, transform.position, transform.rotation);
        Instantiate(shatterEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Weapon_Skill_Katana.bossDead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something enters");
        if(other.transform.tag == "Player")
        {
            StartCoroutine("Shooting");
            Debug.Log("in range");
        }
    }

    void attackReset()
    {
        StartCoroutine("Shooting");
    }

    IEnumerator Shooting()
    {
        if (!isDead)
        {
            ChargeEffect.SetActive(true);
            yield return new WaitForSeconds(2f);

            ChargeEffect.SetActive(false);
            var projectile = Instantiate(BossProjectile, laserOrigin.position, laserOrigin.rotation);
            projectile.GetComponent<Rigidbody>().velocity = (ThePlayer.transform.position - transform.position).normalized * 25;

            Invoke("attackReset", 4f);
        }
        
    }
}
