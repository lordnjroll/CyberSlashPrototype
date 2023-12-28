using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public LayerMask Enemies;
    public Transform PlayerAttackStartPoint;
    public float AttackRange;

    public Camera fpcamera;
    private GameObject Shooter;
    private ShooterAI ShooterScript;
    public ScoreManager Score;
    public GameObject Shield;
    public int ShieldHP = 10;

    private Animator anim;
    

    void Start()
    {
        Score = GetComponent<ScoreManager>();

        Shooter = GameObject.FindWithTag("Shooter").gameObject;

        ShooterScript = Shooter.GetComponent<ShooterAI>();
    }

    
    void Update()
    {
        PlayerAttack();
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //anim.SetTrigger("AttackTrigger" );
            Debug.Log("clicked");
            RaycastHit hit;
            if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, AttackRange))
            {
                
                if(hit.transform.tag == "Shooter")
                {
                    Shooter = hit.transform.gameObject;

                    ShooterScript = Shooter.GetComponent<ShooterAI>();

                    Debug.Log("Hit enemy");
                    ShooterScript.Killed();

                    Score.GetScore();
                }

                if(hit.transform.tag == "Shield")
                {
                    Shield = hit.transform.gameObject;
                    
                    ShieldHP--;

                    Debug.Log("Hit Shield");

                    if(ShieldHP == 0)
                    {
                        Destroy(this.Shield);
                    }
                }
            }
            else
            {
                //Debug.Log("missed");
            }            
            
        }
    }
}
