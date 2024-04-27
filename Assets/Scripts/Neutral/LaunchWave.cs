using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchWave : MonoBehaviour
{
    public Transform playerTrans;
    public float launchForce;
    public ParticleSystem StompEffect;

    private FodderAI fodderScript;
    private ShooterAI shooterScript;
    // Start is called before the first frame update
    void Start()
    {
        #region Inistialize references
        
        
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayerIniatedLaunch()
    {
        Instantiate(StompEffect,new Vector3(this.transform.position.x, this.transform.position.y -0.5f, this.transform.position.z), this.transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(playerTrans.position, 7f);
        foreach (Collider c in colliders)
        {
            if (c.transform.gameObject.layer == 8)
            {
                if(c.transform.gameObject.tag == "FodderTag")
                {
                    fodderScript = c.GetComponentInParent<FodderAI>();
                    fodderScript.GotLaunched(launchForce);
                    yield return null;
                }
                /*if(c.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled == true)
                {
                    c.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    c.attachedRigidbody.velocity = new Vector3(0, launchForce, 0);
                    yield return null;
                }*/
                
            }
        }
    }

    void ExplosionLaunch()
    {

    }
}
