using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public Transform[] GroundedSpawnPoints;
    public Transform[] TurretSpawnPoint;

    /*[Header("Enemy Objects")]
    public GameObject Shooterenemy;
    public GameObject FodderEnemy;
    public GameObject Shieldenemy;*/
    
    public float ShieldenemyCount;

    private int GroundedRandomSpawnNumber;
    private int TurretRandomSpawnNumber;


    private float SpawnCoolDown;

    [Header("Spawn Manager Credit Settings")]
    public float ManagerCredit;
    public float ManagerSpawnRate;
    public float maxManagerCredit;
    private float HypeDifficultyLevel;
    private float PresetSelector;
    string[] GroundedEnemyTags = { "Shooter", "FodderTag", "MuscleTag" };
    string[] SpecialEnemyTags = { "TurretTag", "DroneTag" };
    //private int SpawnPresets;
    [SerializeField] private GroupEnemy[] EnemySet;
    //[SerializeField] private AudioClip SpawnAudio;
    //[SerializeField] private AudioSource Source;

    private GameObject player;
    private ScoreManager ScoreScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //ScoreScript = player.GetComponent<ScoreManager>();
    }

    void Start()
    {
        maxManagerCredit = 30;
        ManagerCredit = maxManagerCredit;

        InvokeRepeating("CreditBuildUp", 0 ,2);
        StartCoroutine("MaxCreditBuildUp");
        //InvokeRepeating("enemySpwan", 0, 0.5f);
    }
    
    // Update is called once per frame
    void Update()
    {
        DevInputs();
        

        //EnemyCount = GameObject.FindGameObjectsWithTag("EnemyTag").Length;
        //EnemyCount = GameObject.FindGameObjectsWithTag("Shooter").Length + GameObject.FindGameObjectsWithTag("FodderTag").Length + GameObject.FindGameObjectsWithTag("Turret").Length;
        ShieldenemyCount = GameObject.FindGameObjectsWithTag("Shield").Length;  

        if(SpawnCoolDown != 0)
        {
            SpawnCoolDown -= Time.deltaTime;
        }

        
    }

    public void enemySpwan()
    {
        if (ManagerCredit > 20 && SpawnCoolDown <= 0 && EnemyCount < 30)
        {
            GroundedRandomSpawnNumber = Random.Range(0, GroundedSpawnPoints.Length); //select the spawn position
            TurretRandomSpawnNumber = Random.Range(0, TurretSpawnPoint.Length);
            PresetSelector = Random.Range(0, 65) + HypeDifficultyLevel;
            //Debug.Log("The difficulty is " + PresetSelector);
            if (PresetSelector <=20)
            {
                if(ManagerCredit >= 15)
                {
                    ManagerCredit -= 15;
                    //Easy preset
                    for (int i = 0; i < 3; i++)
                    {
                        if(GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            /*for(int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }*/
                            Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[i].transform.position, new Quaternion(0, 90, 0, 0));

                        } else if (SpecialEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[0].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                                                
                    }
                    SpawnCoolDown = 2; //set spawn cooldown;
                    return;
                }
                    
            }
            if(PresetSelector <=40 || PresetSelector >20)
            {
                if(ManagerCredit >= 16)
                {
                    ManagerCredit -= 16;
                    //Easy preset 2
                    for (int i = 0; i < 3; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[1].EnemySetGroup[i].gameObject.tag))
                        {
                            /*for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[2], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }*/
                            Instantiate(EnemySet[1].EnemySetGroup[i], GroundedSpawnPoints[i].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[1].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[1].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;   
                    return;
                }
                            
            }
            if(PresetSelector <=65 || PresetSelector >40)
            {
                if (ManagerCredit >= 18)
                {
                    ManagerCredit -= 18;
                    //Easy preset 3
                    for (int i = 0; i < 3; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[2].EnemySetGroup[i].gameObject.tag))
                        {
                            /*for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[3], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }*/
                            Instantiate(EnemySet[2].EnemySetGroup[i], GroundedSpawnPoints[i].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[2].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[2].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
                    return;
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
                        if (GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }


                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[3].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[3].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
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
                        if (GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }


                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[4].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[4].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
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
                        if (GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }


                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[5].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[5].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
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
                        if (GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }


                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[6].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[6].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
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
                        if (GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            for (int GP = 0; GP < GroundedSpawnPoints.Length; GP++)
                            {
                                Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GP].transform.position, new Quaternion(0, 90, 0, 0));
                            }


                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[7].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[7].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 2;
                }
                
            } 
            
        }

        Debug.Log(PresetSelector);

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
            ManagerCredit += 1000;
        }
    }

    void DevInputs()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Invoke("enemySpwan", 0);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Shooter" + "FodderTag" + "TurretTag");
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}

        