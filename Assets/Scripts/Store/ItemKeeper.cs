using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemKeeper : MonoBehaviour
{
    hp hp;

    [SerializeField] private TMP_Text Tipstxt;
    [SerializeField] private TMP_Text KeepList;

    private void Update()
    {
        UseHealthPotion();
        UseBonusPotion();
        UsePowerUp();
        UseSkill1();
        UseSkill2();
        DisplayKeepList();
    }

    void DisplayKeepList()
    {
        KeepList.text = "Health Potion : " + Store.healthpotion.ToString() + "\n"
                                                    +
                        "\nBonus Potion : " + Store.bonuspotion.ToString() + "\n";
    }

    void UseHealthPotion()
    {
        if(Store.healthpotion > 0)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                hp.Hp += 1;
            }
        }
        else
        {
            Tipstxt.text = "You dont have any health potion.";
        }
    }

    void UseBonusPotion()
    {
        if(Store.bonuspotion > 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ScoreManager.score += 2500;
            }
        }
        else
        {
            Tipstxt.text = "You dont have any bonus potion.";
        }
    }

    void UsePowerUp()
    {
        
    }

    void UseSkill1()
    {
        if (Store.Skill1 > 0)
        {

        }
    }

    void UseSkill2()
    {
        if(Store.Skill2 > 0)
        {

        }
    }
}

