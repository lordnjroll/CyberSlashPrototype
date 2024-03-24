using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private TMP_Text missiontxt;
    private int m_killcount;
    private int m_totalkillcount;

    private int m_collectcount;
    private int m_totalcollectcount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Debug.Log("Hit Collision");
            missiontxt.text = "";
        }
    }


}
