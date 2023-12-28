using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    //¬Þ©Ç
    public GameObject Shield;
    public int ShieldHP = 10;
    // Start is called before the first frame update
    void Start()
    {
        Shield.tag = "Shield";
    }

    // Update is called once per frame
    void Update()
    {
        if (ShieldHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void DestroyShield()
    {
        
    }
}
