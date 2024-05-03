using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemKeeper : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private TMP_Text Tipstxt;
    [SerializeField] private TMP_Text KeepList;
    private Weapon_Skill_Katana WSK;

    [SerializeField] private float PowerHoldTime;
    [SerializeField] private int PowerBuff;


    private void Update()
    {
        DisplayKeepList();
        UseHealthPotion();
        UseBonusPotion();
        UsePowerUp();
        UnlockSkill1();
    }

    void DisplayKeepList()
    {
        KeepList.text = "Health Potion : " + Store.healthpotion.ToString() + "\n"
                                                    +
                        "\nBonus Potion : " + Store.bonuspotion.ToString() + "\n" 
                                                    +
                        "\nPower Buff : " + Store.powerup.ToString() + "\n";
    }

    void UseHealthPotion()
    {
        if(Input.GetKeyDown(KeyCode.H) && Store.healthpotion > 0)
        {
            hp.Hp += 1;
            Store.healthpotion--;
        }
        else if(Input.GetKeyDown(KeyCode.H) && Store.healthpotion == 0)
        {
            Tipstxt.text = "You dont have any health potion.";
            StartCoroutine(ClearText());
        }
    }

    void UseBonusPotion()
    {
        if(Input.GetKeyDown(KeyCode.B) && Store.bonuspotion > 0)
        {
            ScoreManager.score += 2500;
        }
        else if(Input.GetKeyDown(KeyCode.B) && Store.bonuspotion == 0)
        {
            Tipstxt.text = "You dont have any bonus potion.";
            StartCoroutine(ClearText());
        }
    }

    void UsePowerUp()
    {
        if(Input.GetKeyDown(KeyCode.V) && Store.powerup > 0)
        {
            WSK.PlayerDamage += PowerBuff;
            if (PowerHoldTime > 0)
            {
                PowerHoldTime -= Time.deltaTime;
            }
            else
            {
                PowerHoldTime = 0;
                WSK.PlayerDamage -= PowerBuff;
            }
        }
        else if(Input.GetKeyDown(KeyCode.V) && Store.powerup == 0)
        {
            Tipstxt.text = "You cant power up.";
            StartCoroutine(ClearText());
        }
    }

    void UnlockSkill1()
    {
        if (Store.Skill1 == true)
        {
            Player.GetComponent<DomanExpansion>().enabled = false;
        }
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        Tipstxt.text = " ";
    }
}

