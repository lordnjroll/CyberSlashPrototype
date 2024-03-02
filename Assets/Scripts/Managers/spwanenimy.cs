using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanenimy : MonoBehaviour
{
    public GameObject enemy;
    public float EnemyCount;
    public float XPosition;
    public float ZPosition;
    public Transform[] SpawnPoints;

    [Header("Enemy Objects")]
    public GameObject Shooterenemy;
    public GameObject FodderEnemy;
    public GameObject Shieldenemy;
    
    public float ShieldenemyCount;

    private int RandomSpawnNumber;
    
    
    private float SpawnCoolDown;

    [Header("Spawn Manager Credit Settings")]
    public float ManagerCredit;
    public float ManagerSpawnRate;
    public float maxManagerCredit;
    private float HypeDifficultyLevel;
    private float PresetSelector;
    private int SpawnPresets;
    

    void Start()
    {
        maxManagerCredit = 30;
        ManagerCredit = maxManagerCredit;
    }
    
    // Update is called once per frame
    void Update()
    {
        enemySpwan();        

        //EnemyCount = GameObject.FindGameObjectsWithTag("EnemyTag").Length;
        EnemyCount = GameObject.FindGameObjectsWithTag("Shooter").Length;
        ShieldenemyCount = GameObject.FindGameObjectsWithTag("Shield").Length;  

        //Debug.Log(RandomSpawnNumber);

        if(SpawnCoolDown != 0)
        {
            SpawnCoolDown -= Time.deltaTime;
        }
    }

    void enemySpwan()
    {
        if (ManagerCredit < 15 && SpawnCoolDown <= 0)
        {
            RandomSpawnNumber = Random.Range(0, 5); //select the spawn position
            
            PresetSelector = Random.Range(0, 100) + HypeDifficultyLevel;
            if(PresetSelector <=20)
                {
                    //Easy preset
                    Instantiate(enemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    Instantiate(enemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    

                    SpawnCoolDown = 10; //set spawn cooldown;
                }
            if(PresetSelector <=40 || PresetSelector >20)
                {
                    //Easy preset 2           
                }
            if(PresetSelector <=55 || PresetSelector >35)
                {
                    //Easy preset 3          
                }

            if(PresetSelector <=70 || PresetSelector >55)
                {
                    //Mid preset 1          
                }
            if(PresetSelector <=80 || PresetSelector >70)
                {
                    //Mid preset 2          
                }    
            if(PresetSelector <=90 || PresetSelector >80)
                {
                    //Hard preset 1            
                }
            if(PresetSelector <=100 || PresetSelector >90)
                {
                    //Hard preset 2           
                }
            if(PresetSelector > 100)
                {
                    //Hard preset 3           
                }    
            /*                       // X,Z
            //XPosition = Random.Range(0, 0);
            //ZPosition = Random.Range(0, 0);
            //                                  X ,     Y ,     Z
            //Instantiate(enemy, new Vector3(XPosition, 0, ZPosition), new Quaternion(0, 90, 0, 0));

            Instantiate(enemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
            EnemyCount += 1;

            SpawnCoolDown = 15;*/
        }

        if(ShieldenemyCount < 10 && SpawnCoolDown <= 0)
        {
            /*Instantiate(Shieldenemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
            ShieldenemyCount += 1;

            SpawnCoolDown = 1;*/
        }
    }
    
}
