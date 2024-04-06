using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EventController : MonoBehaviour
{
    private ScoreManager scoreM;
    private event EventHandler OnGoldAmountChanged;

    private int goldAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuyPotion1()
    {

    }

    void BuyPotion2()
    {

    }

    void BuyPowerUp()
    {

    }

    void BuySpeedUp()
    {

    }

    void BuyShield()
    {

    }

    

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if(ScoreManager.score >= spendGoldAmount)
        {
            goldAmount -= spendGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }
}
