using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCubeScript : MonoBehaviour
{
    public Transform playerTrans;
    private Vector3 playervector3;
    public GameObject DroneHead;
    // Start is called before the first frame update
    private void Awake()
    {
        playervector3 = playerTrans.position;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, playervector3, 2 * Time.deltaTime);
    }

    public void GetPlayerLocation(Vector3 playerPOS)
    {
        playervector3 = playerPOS;
    }
}
