using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    private GameObject Player;
    private hp PlayerHealth;
    private float DespawnTime = 3;
    

    private void Awake()
    {
        Destroy(gameObject, DespawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
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

        if(collision.collider.gameObject.tag == "Player")
        {
            //PlayerHealth.hit();
            //Debug.Log("Player Hit");

            Destroy(gameObject);
        }

        

        
    }


}
