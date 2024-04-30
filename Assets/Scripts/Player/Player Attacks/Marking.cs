using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Marking : MonoBehaviour
{
    public GameObject MarkSprite;
    public GameObject player;
    public Camera playerCam;
    // Start is called before the first frame update

    private void Awake()
    {
        StartCoroutine("MarkAnimation");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        //playerCam = player.GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        MarkSprite.transform.LookAt(playerCam.transform.position, Vector3.up);
    }

    IEnumerator MarkAnimation()
    {
        transform.DORotate(new Vector3(transform.rotation.x , transform.rotation.y , transform.rotation.z + 360f), 2.5f, RotateMode.Fast);
        yield return null;
    }
}
