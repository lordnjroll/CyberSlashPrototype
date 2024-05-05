using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Marking1 : MonoBehaviour
{
    public GameObject MarkSprite;
    public GameObject player;
    public Camera playerCam;
    // Start is called before the first frame update

    private void Awake()
    {
        StartCoroutine("MarkAnimation");
        playerCam = Camera.main;
        transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    private void Start()
    {
        playerCam = Camera.main;
        player = GameObject.FindWithTag("Player").gameObject;
        playerCam = player.GetComponentInChildren<Camera>();
    }
    private void LateUpdate()
    {
        MarkSprite.transform.LookAt(playerCam.transform.position, Vector3.up);
    }

    IEnumerator MarkAnimation()
    {
        transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x , transform.rotation.y , transform.rotation.z + 180f), 0.5f);
        yield return null;
    }
}
