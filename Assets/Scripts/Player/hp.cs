using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public bool isdashing = false;
    [SerializeField] private GameObject DeathLayer;
    [SerializeField] private GameObject GameUILayer;


    public Image HealthHp3Bar;
    public Image HealthHp2Bar;
    public Image HealthHp1Bar;
    public float Health;
    public float MaxHealth;

    public static int Hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        
        EndGame();
        SmoothHP();
        if (Input.GetKeyDown(KeyCode.O))
        {
            Hp--;
        }
    }

    public void SmoothHP()
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

    void EndGame()
    {
        if(Hp == 0)
        {
            Time.timeScale = 0f;
            GameUILayer.SetActive(false);
            DeathLayer.SetActive(true);
        }
    }
}
