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

    }

    public void StrongAttack(GameObject Target)
    {
        switch (Target.tag)
        {
            case "FodderTag":
                fodderScript = Target.GetComponent<FodderAI>();
                fodderScript.OnDeath();
                break;
            case "Shooter":
                shooterScript = Target.GetComponent<ShooterAI>();
                break;
        }
    }
}
