using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hp : MonoBehaviour
{
    public static int Hp = 10;

    [SerializeField] GameObject Hpbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //you can set timeing the player can heal, type:
        ModifyHp(1);
        //you can set when player get hit , type:
        ModifyHp(-1);
        UpdateHpbar();
        //hit();
    }

    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if(Hp < 0)
        {
            Hp = 0;
        }
    }

    void UpdateHpbar()
    {

        /*for(int i = 0; i < Hpbar.transform.childCount; i += 2 )
        {
            if(Hp > i)
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }*/

        if(Hp <= 10 && Hp >= 8)
        {
            Hpbar.transform.GetChild(4).gameObject.SetActive(true);
            Hpbar.transform.GetChild(3).gameObject.SetActive(false);
            Hpbar.transform.GetChild(2).gameObject.SetActive(false);
            Hpbar.transform.GetChild(1).gameObject.SetActive(false);
            Hpbar.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        if(Hp <= 7 && Hp > 5)
        {
            Hpbar.transform.GetChild(3).gameObject.SetActive(true);
            Hpbar.transform.GetChild(4).gameObject.SetActive(false);
            Hpbar.transform.GetChild(2).gameObject.SetActive(false);
            Hpbar.transform.GetChild(1).gameObject.SetActive(false);
            Hpbar.transform.GetChild(0).gameObject.SetActive(false);
        }

        if(Hp <= 5 && Hp > 3)
        {
            Hpbar.transform.GetChild(2).gameObject.SetActive(true);
            Hpbar.transform.GetChild(3).gameObject.SetActive(false);
            Hpbar.transform.GetChild(4).gameObject.SetActive(false);
            Hpbar.transform.GetChild(1).gameObject.SetActive(false);
            Hpbar.transform.GetChild(0).gameObject.SetActive(false);
        }

        if(Hp <= 3 && Hp > 1)
        {
            Hpbar.transform.GetChild(1).gameObject.SetActive(true);
            Hpbar.transform.GetChild(3).gameObject.SetActive(false);
            Hpbar.transform.GetChild(2).gameObject.SetActive(false);
            Hpbar.transform.GetChild(4).gameObject.SetActive(false);
            Hpbar.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Hp == 1)
        {
            Hpbar.transform.GetChild(0).gameObject.SetActive(true);
            Hpbar.transform.GetChild(3).gameObject.SetActive(false);
            Hpbar.transform.GetChild(2).gameObject.SetActive(false);
            Hpbar.transform.GetChild(1).gameObject.SetActive(false);
            Hpbar.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if(Hp == 0)
        {
            Hpbar.transform.GetChild(4).gameObject.SetActive(false);
            Hpbar.transform.GetChild(3).gameObject.SetActive(false);
            Hpbar.transform.GetChild(2).gameObject.SetActive(false);
            Hpbar.transform.GetChild(1).gameObject.SetActive(false);
            Hpbar.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    void EndGame()
    {
        if(Hp == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void hit()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ModifyHp(2);
            Debug.Log("+1");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ModifyHp(-2);
            Debug.Log("-1");
        }


        ModifyHp(-2);

        Debug.Log("HP LOST");
    }
}
