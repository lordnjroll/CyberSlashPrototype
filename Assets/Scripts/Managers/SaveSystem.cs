using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public int savehp;
    public int savescore;
    public int keep_health;
    public int keep_exp;
    public int keep_power;

    void Save()
    {
        savehp = hp.Hp;
        savescore = ScoreManager.score;
        keep_health = Store.healthpotion;
        keep_exp = Store.bonuspotion;
        keep_power = Store.powerup;
    }

    private void Start()
    {
        hp.Hp += savehp;
        ScoreManager.score += savescore;
        Store.healthpotion += keep_health;
        Store.bonuspotion += keep_exp;
        Store.powerup += keep_power;
    }
}
