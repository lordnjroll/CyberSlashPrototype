using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyerBossShield : MonoBehaviour
{
    public float shieldHealth = 3;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldHealth == 0 && isDead == false)
        {
            transform.GetComponentInParent<CapsuleCollider>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.layer == 11)
        {
            shieldHealth -= 1;
        }
    }

    void ShieldDestroyed()
    {
        
    }
}
