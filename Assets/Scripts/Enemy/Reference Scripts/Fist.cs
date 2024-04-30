using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private GrabPlayer grabPlayer;
    private MuscleAI muscleAI;
    public Transform FistGrabPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && muscleAI.smashPlayer)
        {
            //hp.Hp = hp.Hp - 2;
            Debug.Log("Fist Hit");
            muscleAI.smashPlayer = false;
            muscleAI.resetAttack = true;
        }

        if (other.gameObject.tag == "Player" && muscleAI.hugPlayer)
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
            muscleAI.hugPlayer = false;
            muscleAI.resetAttack = true;
        }
    }
}
