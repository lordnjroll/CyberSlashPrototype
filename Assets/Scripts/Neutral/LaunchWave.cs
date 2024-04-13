using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchWave : MonoBehaviour
{
    public Transform playerTrans;
    public float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerIniatedLaunch()
    {
        Collider[] colliders = Physics.OverlapSphere(playerTrans.position, 3f);
        foreach (Collider c in colliders)
        {
            if (c.transform.gameObject.layer == 8)
            {
                
                c.attachedRigidbody.AddForce(c.transform.up * 10f, ForceMode.VelocityChange);
            }
        }
    }

    void ExplosionLaunch()
    {

    }
}
