using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanagerZone : MonoBehaviour
{
    public float Timer;
    public static bool outOfZone = false;
    public static bool inzone = true;

    private void Update()
    {
        if (outOfZone && !inzone)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                
            }
            else if (Timer < 0)
            {
                hp.Hp--;
                Timer += 2;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outOfZone = true;
            inzone = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outOfZone = false;
            inzone = true;
            Timer = 2;
        }
    }
}
