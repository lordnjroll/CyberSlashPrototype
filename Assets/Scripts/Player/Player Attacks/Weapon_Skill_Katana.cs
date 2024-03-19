using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Skill_Katana : MonoBehaviour
{
    [Header("Dash settings")]
    public float dashSpeed;
    public float DashCutthreshold; //how close can the player get before dashing into the enemy

    [Header("Knife throwing settings")]
    public GameObject Kunai;
    public Camera mainCamera;
    public KeyCode secondarySkillbtn = KeyCode.Mouse1;

    private List<GameObject> MarkedTargetes;
    private int TotalEnemiesMarked = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        TotalEnemiesMarked = MarkedTargetes.Length;
        PlayerInput();

    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(secondarySkillbtn))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("throw knife");
            if (Physics.Raycast(ray, out hit))
            {
                GameObject RayCastHitObject = hit.transform.gameObject;
                Debug.Log("knife hit");
                if (RayCastHitObject.layer == 8)
                {
                    
                    Debug.Log("Object marked: " + markedEnemy.name);
                }

            }
        }
    }
}
