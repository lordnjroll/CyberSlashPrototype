using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    
    private FodderAI fodderScript;
    private ShooterAI shooterScript;
    private TurretAI turretScript;
    private DroneAI droneScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAttacked(GameObject Target)
    {
        Debug.Log("KM function called");
        switch (Target.transform.gameObject.tag)
        {
            case "FodderTag":
                Debug.Log("FodderHit");
                fodderScript = Target.GetComponentInParent<FodderAI>();
                fodderScript.OnDeath();
                break;
            case "Shooter":
                Debug.Log("shooterHit");
                shooterScript = Target.GetComponentInParent<ShooterAI>();
                shooterScript.OnDeath();
                break;
            case "TurretTag":
                turretScript = Target.GetComponentInParent<TurretAI>();
                turretScript.OnDeath();
                break;
            default:
                Debug.Log("default " + Target);
                break;
        }
    }

    public void StrongAttack(GameObject Target)
    {
        Debug.Log("KM function called");
        switch (Target.transform.gameObject.tag)
        {
            case "FodderTag":
                Debug.Log("FodderHit");
                fodderScript = Target.GetComponentInParent<FodderAI>();
                fodderScript.StrongOnDeath();
                break;
            case "Shooter":
                Debug.Log("shooterHit");
                shooterScript = Target.GetComponentInParent<ShooterAI>();
                shooterScript.StrongOnDeath();
                break;
            case "TurretTag":
                turretScript = Target.GetComponentInParent<TurretAI>();
                turretScript.OnDeath();
                break;
            default:
                Debug.Log("default " + Target);
                break;
        }
    }
}
