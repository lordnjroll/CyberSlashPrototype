using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public bool Godmode = false;

    public GameObject HealthHp3Bar;
    public GameObject HealthHp2Bar;
    public GameObject HealthHp1Bar;
    [SerializeField] private GameObject DeathLayer;
    [SerializeField] private GameObject GameUILayer;

    public static int Hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        SmoothHP();

        //Debug.Log("The player has " + Hp);

        if (Input.GetKeyDown(KeyCode.O))
        {
            Hp -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Hp += 2;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Godmode = true;
        }
    }


    void SmoothHP()
    {
        switch (Hp)
        {
            case 3:
                HealthHp3Bar.SetActive(true);
                HealthHp2Bar.SetActive(true);
                HealthHp1Bar.SetActive(true);
                break;
            case 2:
                HealthHp3Bar.SetActive(false);
                HealthHp2Bar.SetActive(true);
                HealthHp1Bar.SetActive(true);
                break;
            case 1:
                HealthHp3Bar.SetActive(false);
                HealthHp2Bar.SetActive(false);
                HealthHp1Bar.SetActive(true);
                break;
        }
    }

    public void ISdashing()
    {

    }

    public void HealthLost()
    {
        if (!Godmode)
        {
            Hp--;
            StartCoroutine("GodmodeTimer");
            Godmode = true;
        }
        
    }

    IEnumerator GodmodeTimer()
    {
        yield return new WaitForSeconds(1f);
        Godmode = false;
    }
}
