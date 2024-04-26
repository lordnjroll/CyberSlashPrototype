using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public bool Godmode = false;

    public Image HealthHp3Bar;
    public Image HealthHp2Bar;
    public Image HealthHp1Bar;
    [SerializeField] private GameObject DeathLayer;
    [SerializeField] private GameObject GameUILayer;

    public float Health;
    public float MaxHealth;
    public static int Hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        Hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        SmoothHP();

        //Debug.Log("The player has " + Hp);

        if (Input.GetKeyDown(KeyCode.O))
        {
            Hp--;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Hp++;
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
            case 2:
                HealthHp3Bar.fillAmount = Mathf.Clamp(Health - MaxHealth, 0, 1);
                break;
            case 1:
                HealthHp2Bar.fillAmount = Mathf.Clamp(Health - MaxHealth, 0, 1);
                break;
            case 0:
                HealthHp1Bar.fillAmount = Mathf.Clamp(Health - MaxHealth, 0, 1);
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
