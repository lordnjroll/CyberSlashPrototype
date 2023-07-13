using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowingKnife_RayCast : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab; // The knife to attach to objects
    [SerializeField] private float knifeSpeed = 50f; // The speed at which the knife is thrown
    [SerializeField] private float maxDistance = 100f; // The maximum distance the raycast can travel
    [SerializeField] private LayerMask attachLayer; // The layer that the knife can attach to

    public Camera mainCamera;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1));
        {
            ShootRaycast();
        }
    }

    private void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxDistance))
        {
            if (((1 << hit.collider.gameObject.layer) & attachLayer) != 0)
            {
                GameObject knifeInstance = Instantiate(knifePrefab, transform.position + mainCamera.transform.forward, Quaternion.identity);
                Rigidbody knifeRigidbody = knifeInstance.GetComponent<Rigidbody>();
                knifeRigidbody.AddForce(mainCamera.transform.forward * knifeSpeed, ForceMode.Impulse);
                knifeInstance.transform.LookAt(hit.point);
                knifeInstance.GetComponent<Kunai>().AttachTo(hit.collider.gameObject, hit.point);
            }
        }
    }
}

