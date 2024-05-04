using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    private GameObject PlayerObject;
    private FodderAI fodderScript;
    private ShooterAI shooterScript;
    private TurretAI turretScript;
    private DroneAI droneScript;
    private ScoreManager ScoreScript;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerObject = GameObject.FindWithTag("Player").gameObject;
        ScoreScript = PlayerObject.GetComponent<ScoreManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAttacked(GameObject Target)
    {
        //Debug.Log("KM function called");
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
        ScoreScript.OnEnemyKilled();
    }

    public void StrongAttack(GameObject Target)
    {
        //Debug.Log("KM function called");
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
                turretScript.StrongOnDeath();
                break;
            default:
                Debug.Log("default " + Target);
                break;
        }
        ScoreScript.OnEnemyKilled();
    }
}
