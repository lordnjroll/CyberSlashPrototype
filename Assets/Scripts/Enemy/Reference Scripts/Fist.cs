using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private MuscleAI muscleAI;
    public Transform PlayerTransform, FistTransform;
    public Rigidbody Playerrigidbody;
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Grab");
            //hp.Hp = hp.Hp - 1;
            PlayerTransform.parent = FistTransform;
            Playerrigidbody.useGravity = false;
            Player.GetComponent<FastMovementScript>().enabled = false;
            Playerrigidbody.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(DropPlayer());
        }
    }

    IEnumerator DropPlayer()
    {
        yield return new WaitForSeconds(2.5f);
        PlayerTransform.parent = null;
        Playerrigidbody.useGravity = true;
        Player.GetComponent<FastMovementScript>().enabled = true;
        Playerrigidbody.constraints = RigidbodyConstraints.None;
    }
}
