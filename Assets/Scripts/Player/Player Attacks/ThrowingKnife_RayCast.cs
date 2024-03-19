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

    public float dashDistance = 5f;
    public float acceleration = 10f;
    public float teleportDistanceThreshold = 1f;

    private bool isDashing = false;
    private bool isAccelerating = false;

    private Transform markedObject;
    private Vector3 destinationPosition;

    private void Update()
    {
        if (isDashing)
            return;

        // Check for mouse click to mark an object
        if (Input.GetMouseButtonDown(1))
        {
            // Cast a ray from the mouse position to detect objects
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("throw knife");
            if (Physics.Raycast(ray, out hit))
            {
                GameObject RayCastHitObject = hit.transform.gameObject;
                Debug.Log("knife hit");
                if(RayCastHitObject.layer == 8)
                {
                    markedObject = hit.transform;
                    Debug.Log("Object marked: " + markedObject.name);
                }
                
            }
        }

        // Check for dash input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (markedObject != null)
            {
                isAccelerating = true;
                isDashing = true;
                destinationPosition = markedObject.position - markedObject.forward * dashDistance;
            }
            else
            {
                Debug.Log("No object marked!");
            }
        }

        // Accelerate towards the marked object
        if (isAccelerating)
        {
            Vector3 direction = markedObject.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= teleportDistanceThreshold)
            {
                isAccelerating = false;
                Dash();
            }
            else
            {
                direction.Normalize();
                transform.position += direction * acceleration * Time.deltaTime;
            }
        }
    }

    private void Dash()
    {
        // Teleport the player behind the object
        transform.position = destinationPosition;

        // Reset the marked object and allow marking again
        markedObject = null;
        isDashing = false;

        Debug.Log("Dashed behind the object!");
    }
}

