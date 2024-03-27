using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text m_WallJump;
    [SerializeField] private TMP_Text m_Dash;

    [SerializeField] private GameObject tipsLayer;

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
            Debug.Log("Start Tutorial");
            tipsLayer.SetActive(true);
            m_WallJump.text = "Do Wall Jump";
            m_Dash.text = "Do Dash";
        }
    }


    void DoWallJump(Collision collision)
    {
        if (collision.gameObject.layer.Equals("Wall") && Input.GetKeyDown(KeyCode.Space))
        {
            m_WallJump.text = "Wall Jump Done";
        }
    }

    void DoDash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Dash.text = "Dash Done";
        }
    }
}
