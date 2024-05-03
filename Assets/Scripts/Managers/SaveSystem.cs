using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    public static int savehp;
    public static int savescore;
    public static int keep_health;
    public static int keep_exp;
    public static int keep_power;

    void Save()
    {
        savehp = hp.Hp;
        savescore = ScoreManager.score1;
        keep_health = Store.healthpotion;
        keep_exp = Store.bonuspotion;
        keep_power = Store.powerup;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        savehp = hp.Hp;
        savescore = ScoreManager.score1;
        keep_health = Store.healthpotion;
        keep_exp = Store.bonuspotion;
        keep_power = Store.powerup;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad()
    {
        hp.Hp += savehp;
        ScoreManager.score1 += savescore;
        Store.healthpotion += keep_health;
        Store.bonuspotion += keep_exp;
        Store.powerup += keep_power;
        Debug.Log(ScoreManager.score);
    }

    private void Update()
    {
        Save();
        Debug.Log(savescore);
        Debug.Log(savehp);
    }
}
