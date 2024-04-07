using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemKeeper
{
    hp hp;
    Store store;
    [SerializeField] private TMP_Text Tipstxt;

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

