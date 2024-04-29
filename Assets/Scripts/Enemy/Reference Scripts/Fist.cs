using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private MuscleAI muscleAI;
    [SerializeField] private Transform grabPlayer;

    private Rigidbody objrb;

    private void Start()
    {
        objrb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hp.Hp = hp.Hp - 2;
            Debug.Log("Fist Hit");
        }

        if(other.gameObject.tag == "Player" && muscleAI.hugPlayer == true)
        {
            HugPlayer(grabPlayer);
            Debug.Log("Grab");
        }
    }

    void HugPlayer(Transform objectGrab)
    {
        this.grabPlayer = objectGrab;
    }

}
