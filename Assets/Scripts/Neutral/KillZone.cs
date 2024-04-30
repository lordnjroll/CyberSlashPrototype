using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private hp HealthScript;
    private GameObject Player;
    public bool isOutside;
    // Start is called before the first frame update

    private void Awake()
    {
        InvokeRepeating("DamageTick", 0, 1);
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HealthScript = Player.GetComponent<hp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DamageTick()
    {

        if (isOutside)
        {
            Player.GetComponent<hp>().HealthLost();
        }
    }
}
