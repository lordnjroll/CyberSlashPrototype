using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    private GameObject Player;
    private hp PlayerHealth;
    private float DespawnTime = 3;
    private Rigidbody ProRB;
    private bool isReflected = false;
    public ParticleSystem ExplosionEffect;

    private mesh_destroy ShatterScript;

    private void Awake()
    {
        Destroy(gameObject, DespawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        ProRB = this.GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player").gameObject;

        PlayerHealth = Player.GetComponent<hp>();

        //PlayerHealth = GameObject.Find("hit").GetComponent<hp>();

    }

    // Update is called once per frame
    void Update()
    {
        DespawnTime += Time.deltaTime;

        if (DespawnTime == 5)
        {
            Destroy(this);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isReflected)
        {
            if (collision.collider.gameObject.tag == "Player")
            {
                //PlayerHealth.hit();
                //Debug.Log("Player Hit");

                PlayerHealth.HealthLost();
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }

        }
        else
        {
            Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10f, 8 | 7);
            foreach (Collider c in colliders)
            {

                if (c.transform.gameObject.layer == 8)
                {
                    ShatterScript = c.GetComponent<mesh_destroy>();
                    ShatterScript.gothit();
                    
                }

                if (c.transform.gameObject.layer == 7)
                {
                    c.attachedRigidbody.AddExplosionForce(20f, this.transform.position, 7f);
                }
            }
            Destroy(this);
        }
            
     
    }

    public void OnPlayerParry(Vector3 AimDirection)
    {
        this.gameObject.layer = 11;
        isReflected = true;
        ProRB.AddForce(AimDirection * 75f, ForceMode.VelocityChange);
        
    }

}
