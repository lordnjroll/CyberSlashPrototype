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

    public IEnumerator PlayerIniatedLaunch()
    {
        Collider[] colliders = Physics.OverlapSphere(playerTrans.position, 10f);
        foreach (Collider c in colliders)
        {
            if (c.transform.gameObject.layer == 8)
            {
                c.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                c.attachedRigidbody.AddForce(c.transform.up * 10f, ForceMode.VelocityChange);
                yield return new WaitForSeconds(0.2f);
                c.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
        }
    }

    void ExplosionLaunch()
    {

    }
}
