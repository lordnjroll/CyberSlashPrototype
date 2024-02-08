using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hp : MonoBehaviour
{
    [SerializeField] private GameObject HP1;
    [SerializeField] private GameObject HP2;
    [SerializeField] private GameObject HP3;
    [SerializeField] private GameObject DeathLayer;
    [SerializeField] private GameObject GameUILayer;

    public static int Hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text();
        UpdateHpbar();
        EndGame();
    }
    public void text()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Hp--;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Hp++;
        }
    }

    void UpdateHpbar()
    {
        switch (Hp)
        {
            case 1:
                HP1.SetActive(true);
                HP2.SetActive(false);
                HP3.SetActive(false);
                break;
            case 2:
                HP1.SetActive(true);
                HP2.SetActive(true);
                HP3.SetActive(false);
                break;
            case 3:
                HP1.SetActive(true);
                HP2.SetActive(true);
                HP3.SetActive(true);
                break;
            default:
                HP1.SetActive(false);
                HP2.SetActive(false);
                HP3.SetActive(false);
                break;
        }
    }


    void EndGame()
    {
        if(Hp == 0)
        {
            Time.timeScale = 0f;
            GameUILayer.SetActive(false);
            DeathLayer.SetActive(true);
        }
    }

    //public void hit()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        ModifyHp(2);
    //        Debug.Log("+1");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        ModifyHp(-2);
    //        Debug.Log("-1");
    //    }


    //    ModifyHp(-2);

    //    Debug.Log("HP LOST");
    //}
}
