using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyerBossHEX : MonoBehaviour
{
    private GameObject player;
    private bool isInside = false;
    private hp HPScript;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        HPScript = player.GetComponent<hp>();
        transform.DOScale(2, 0.3f);

        Invoke("CountDown", 3f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isInside);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.gameObject.layer == 7)
        {
            isInside = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 7)
        {
            isInside = false;
        }
    }

    void CountDown()
    {
        if (isInside)
        {
            HPScript.HealthLost();
        }
        Destroy(gameObject,0.1f);
    }
}
