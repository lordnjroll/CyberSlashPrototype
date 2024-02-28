using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Transform PlayerLocation;
    public GameObject Player;
    public Transform RespawnPoint;
    public ScoreManager scoreManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            Player.transform.position = RespawnPoint.transform.position;
        }

        ScoreManager.deathcount++;
        //Debug.Log("you dead" + scoreManager.deathcount);
    }
}
