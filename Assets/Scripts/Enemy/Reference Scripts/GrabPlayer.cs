using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlayer : MonoBehaviour
{
    private MuscleAI muscleAI;
    [SerializeField] private Transform grabPlayer;
    [SerializeField] private GameObject player;
    private Rigidbody objrb;

    private void Start()
    {
        objrb = GetComponent<Rigidbody>();
    }

    public void HugPlayer(Transform objectGrab)
    {
        this.grabPlayer = objectGrab;
        objrb.useGravity = false;
        player.GetComponent<FastMovementScript>().enabled = false;
    }

    public void DropPlayer()
    {
        this.grabPlayer = null;
        objrb.useGravity = true;
    }

    public void Update()
    {
        if (grabPlayer != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, grabPlayer.position, Time.deltaTime * lerpSpeed);
            objrb.MovePosition(newPosition);
        }
    }
}
