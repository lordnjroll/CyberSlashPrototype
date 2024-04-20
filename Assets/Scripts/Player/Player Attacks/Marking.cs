using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marking : MonoBehaviour
{
    public GameObject MarkSprite;
    public GameObject player;
    public Camera playerCam;
    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        //playerCam = player.GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        MarkSprite.transform.LookAt(playerCam.transform.position, Vector3.up);
    }
}
