using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomanExpansion : MonoBehaviour
{

    public GameObject DomainSphere;
    public ParticleSystem DomainParticleEffect;
    public float DomainParticleDuration = 0.75f;

    public LayerMask EnemyMask;

    public float ChargeUpSpeed = 2f;
    public float expandSpeed = 5f;
    public float shrinkSpeed = 5; //speed the domain shrink after fully expanded

    public float ChargeUpMaxScale = 10f;
    public float minScale = 0.1f;
    public float maxScale = 30f;

    public float ChargeUpTimer = 0f;
    public float TimeToCharge = 5f;

    private float CoolDownTimer = 5f;
    public float CoolDownTime = 5f;

    private bool OnCoolDown = false;
    private bool FullyExpanded = false;

    public KeyCode expandButton = KeyCode.Q;

    private bool isExpanding = false;
    private Vector3 initialScale;

    //Script references
    public ScoreManager Score;
    public mesh_destroy dismemberScript;

    // Start is called before the first frame update
    void Start()
    {
        //set the domain as the minimum scale at the beginning
        DomainSphere.transform.localScale = new Vector3(minScale, minScale, minScale);
        initialScale = new Vector3(minScale, minScale, minScale);

        Score = GetComponent<ScoreManager>();
        dismemberScript = GetComponent<mesh_destroy>();

    }

    // Update is called once per frame
    void Update()
    {
        InputManager();

        DomainManager();
    }

    void InputManager()
    {
        if(ChargeUpTimer < TimeToCharge)
        {
            //for when the charge time is lower then the target time   
            if (Input.GetKey(expandButton) && !OnCoolDown && !FullyExpanded)
            {
                //start the domain charging when holding the button
                startCharge();
                //Debug.Log("fuck");
            }
            else if (ChargeUpTimer > 0 && !(Input.GetKey(expandButton)) && !FullyExpanded)
            {
                //decrease the domain charging when not holding the button
                DecreaseCharge();
                
            }
            else if (ChargeUpTimer < 0)
            {
                //to make sure the charge up timer doesn't go below 0
                ChargeUpTimer = 0;
                DomainSphere.transform.localScale = new Vector3(minScale, minScale, minScale);
                //Debug.Log("you");
            }
        }
        else if(ChargeUpTimer >= TimeToCharge && !OnCoolDown)
        {
            //domain expansion
            FullExpansion();
        }


    }

    void DomainManager()
    {
        
    }

    void startCharge()
    {
        //increase the timer
        ChargeUpTimer += Time.deltaTime;
        isExpanding = true;

        //Slowly increase the size
        Vector3 chargeTargetScale = new Vector3(maxScale * .1f, maxScale * .1f, maxScale * .1f);
        DomainSphere.transform.localScale = Vector3.Lerp(DomainSphere.transform.localScale, chargeTargetScale, ChargeUpSpeed * Time.deltaTime);
    }

    void DecreaseCharge()
    {
        //decrease the timer
        ChargeUpTimer -= Time.deltaTime;
        isExpanding = false;

        ////Slowly decrease the size
        DomainSphere.transform.localScale = Vector3.Lerp(DomainSphere.transform.localScale, initialScale, ChargeUpSpeed * Time.deltaTime * 2);
    }

    void FullExpansion()
    {
        isExpanding = true;
        FullyExpanded = true;

        //quickly expand the domain barrier

        Vector3 targetScale = new Vector3(maxScale, maxScale, maxScale);
        DomainSphere.transform.localScale = Vector3.Lerp(DomainSphere.transform.localScale, targetScale, expandSpeed * Time.deltaTime);

        if (Input.GetKeyUp(expandButton))
        {
            OnCoolDown = true;
            //Debug.Log("Domain Activate");
            DomainParticleEffect.Play();
            FullyExpanded = false;

            InvokeRepeating("QuickDecreaseCharge", DomainParticleDuration, 0.1f);

            //domain effects here, also still missing the collision check
            Collider[] trappedTargets = Physics.OverlapSphere(DomainSphere.transform.position, maxScale, EnemyMask);
            foreach(Collider hit in trappedTargets)
            {
                //Debug.Log("slashed");

                dismemberScript = hit.GetComponent<mesh_destroy>();

                dismemberScript.hit = true;

                //ScoreManager.score += 150;
            }

            Invoke("DomainCD",CoolDownTime);
          
        }
    }

    void DomainCD()
    {
        OnCoolDown = false;
        FullyExpanded = false;
        CancelInvoke("QuickDecreaseCharge");
    }

    void QuickDecreaseCharge()
    {
        ChargeUpTimer = 0f;

        ////Slowly decrease the size
        DomainSphere.transform.localScale = Vector3.Lerp(DomainSphere.transform.localScale, initialScale, shrinkSpeed * Time.deltaTime * 2);
    }
}
