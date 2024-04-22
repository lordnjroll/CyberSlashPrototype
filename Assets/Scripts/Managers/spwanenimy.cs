using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanenimy : MonoBehaviour
{
    [System.Serializable]
    public class GroupEnemy
    {
        public GameObject[] EnemySetGroup;
    }

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
    //private int SpawnPresets;

    [SerializeField]
    private GroupEnemy[] EnemySet;



    void Start()
    {
        maxManagerCredit = 30;
        ManagerCredit = maxManagerCredit;

        InvokeRepeating("CreditBuildUp", 0 ,2);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Mission.Startkill == true || Mission.Startsur == true)
        {
            enemySpwan();
        }
       

        //EnemyCount = GameObject.FindGameObjectsWithTag("EnemyTag").Length;
        EnemyCount = GameObject.FindGameObjectsWithTag("EnemyTag").Length;
        ShieldenemyCount = GameObject.FindGameObjectsWithTag("Shield").Length;  

        //Debug.Log(RandomSpawnNumber);

        if(SpawnCoolDown != 0)
        {
            SpawnCoolDown -= Time.deltaTime;
        }
    }

    void enemySpwan()
    {
        if (ManagerCredit > 20 && SpawnCoolDown <= 0 && EnemyCount < 30)
        {
            RandomSpawnNumber = Random.Range(0, 5); //select the spawn position
            PresetSelector = Random.Range(0, 100) + HypeDifficultyLevel;
            //Debug.Log("The difficulty is " + PresetSelector);
            if (PresetSelector <=20)
            {
                if(ManagerCredit >= 15)
                {
                    ManagerCredit -= 15;
                    //Easy preset
                    for (int i = 0; i < EnemySet[0].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[0].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15; //set spawn cooldown;
                }
                    
            }
            if(PresetSelector <=40 || PresetSelector >20)
            {
                if(ManagerCredit >= 16)
                {
                    ManagerCredit -= 16;
                    //Easy preset 2
                    for (int i = 0; i < EnemySet[1].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[1].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;   
                }
                            
            }
            if(PresetSelector <=65 || PresetSelector >40)
            {
                if (ManagerCredit >= 18)
                {
                    ManagerCredit -= 18;
                    //Easy preset 3
                    for (int i = 0; i < EnemySet[2].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[2].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;  

                }
                        
            }

            if(PresetSelector <=80 || PresetSelector > 65)
            {
                if (ManagerCredit >= 20)
                {
                    ManagerCredit -= 20;
                    //Mid preset 1
                    for (int i = 0; i < EnemySet[3].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[3].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector <=90 || PresetSelector >80)
            {
                if (ManagerCredit >= 22)
                {
                    ManagerCredit -= 22;
                    //Mid preset 2
                    for (int i = 0; i < EnemySet[4].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[4].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;
                }
                
            }    
            if(PresetSelector <=95 || PresetSelector >90)
            {
                if (ManagerCredit >= 30)
                {
                    ManagerCredit -= 30;
                    //Hard preset 1
                    for (int i = 0; i < EnemySet[5].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[5].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector <=100 || PresetSelector >95)
            {
                if (ManagerCredit >= 30)
                {
                    ManagerCredit -= 30;
                    //Hard preset 2
                    for (int i = 0; i < EnemySet[6].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[6].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector > 100)
            {
                if (ManagerCredit >= 40)
                {
                    ManagerCredit -= 40;
                    //Hard preset 3
                    for (int i = 0; i < EnemySet[7].EnemySetGroup.Length; i++)
                    {
                        Instantiate(EnemySet[7].EnemySetGroup[i], SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                    }
                    SpawnCoolDown = 15;
                }
                
            } 
            
        }

    }
    
    IEnumerator MaxCreditBuildUp()
    {
        yield return new WaitForSeconds(10f);
        maxManagerCredit += 1;
    }

    void CreditBuildUp()
    {
        while(ManagerCredit <= maxManagerCredit)
        {
            ManagerCredit += 1;
        }
    }
}

        