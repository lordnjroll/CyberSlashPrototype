using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private GrabPlayer grabPlayer;
    private MuscleAI muscleAI;
    public Transform FistGrabPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && MuscleAI.smashPlayer)
        {
            //hp.Hp = hp.Hp - 2;
            Debug.Log("Fist Hit");
            MuscleAI.smashPlayer = false;
            MuscleAI.resetAttack = true;
        }

        if (collision.gameObject.tag == "Player" && MuscleAI.hugPlayer)
        {
            if (grabPlayer == null)
            {
                grabPlayer.HugPlayer(FistGrabPoint);
            }
            else
            {
                grabPlayer.DropPlayer();
                grabPlayer = null;
            }
            Debug.Log("Grab");
            MuscleAI.hugPlayer = false;
            MuscleAI.resetAttack = true;
        }
    }
}
