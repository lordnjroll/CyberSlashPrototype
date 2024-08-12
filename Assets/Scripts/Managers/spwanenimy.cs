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

    [Header("Enemy Objects")]
    public GameObject Shooterenemy;
    public GameObject FodderEnemy;
    public GameObject Shieldenemy;
    
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
    [SerializeField] private AudioClip SpawnAudio;
    [SerializeField] private AudioSource Source;

    private GameObject player;
    private ScoreManager ScoreScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ScoreScript = player.GetComponent<ScoreManager>();
    }

    void Start()
    {
        maxManagerCredit = 30;
        ManagerCredit = maxManagerCredit;

        InvokeRepeating("CreditBuildUp", 0 ,2);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Mission.Startkill == true || Mission.Startsur)
        {
            InvokeRepeating("enemySpwan", 0, 0.5f);
        }

        if(Mission.Clear_Dash && Mission.Clear_walljump)
        {
            InvokeRepeating("enemySpwan", 0, 0.5f);
        }

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
            PresetSelector = Random.Range(0, 100) + HypeDifficultyLevel;
            //Debug.Log("The difficulty is " + PresetSelector);
            if (PresetSelector <=20)
            {
                if(ManagerCredit >= 15)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 15;
                    //Easy preset
                    for (int i = 0; i < EnemySet[0].EnemySetGroup.Length; i++)
                    {
                        if(GroundedEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[0].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        } else if (SpecialEnemyTags.Contains(EnemySet[0].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[0].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                                                
                    }
                    SpawnCoolDown = 15; //set spawn cooldown;
                }
                    
            }
            if(PresetSelector <=40 || PresetSelector >20)
            {
                if(ManagerCredit >= 16)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 16;
                    //Easy preset 2
                    for (int i = 0; i < EnemySet[1].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[1].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[1].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[1].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[1].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;   
                }
                            
            }
            if(PresetSelector <=65 || PresetSelector >40)
            {
                if (ManagerCredit >= 18)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 18;
                    //Easy preset 3
                    for (int i = 0; i < EnemySet[2].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[2].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[2].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[2].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[2].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;  

                }
                        
            }

            if(PresetSelector <=80 || PresetSelector > 65)
            {
                if (ManagerCredit >= 20)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 20;
                    //Mid preset 1
                    for (int i = 0; i < EnemySet[3].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[3].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[3].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[3].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[3].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector <=90 || PresetSelector >80)
            {
                if (ManagerCredit >= 22)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 22;
                    //Mid preset 2
                    for (int i = 0; i < EnemySet[4].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[4].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[4].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[4].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[4].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;
                }
                
            }    
            if(PresetSelector <=95 || PresetSelector >90)
            {
                if (ManagerCredit >= 30)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 30;
                    //Hard preset 1
                    for (int i = 0; i < EnemySet[5].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[5].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[5].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[5].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[5].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector <=100 || PresetSelector >95)
            {
                if (ManagerCredit >= 30)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 30;
                    //Hard preset 2
                    for (int i = 0; i < EnemySet[6].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[6].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[6].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[6].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[6].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
                    }
                    SpawnCoolDown = 15;
                }
                
            }
            if(PresetSelector > 100)
            {
                if (ManagerCredit >= 40)
                {
                    Source.PlayOneShot(SpawnAudio);
                    ManagerCredit -= 40;
                    //Hard preset 3
                    for (int i = 0; i < EnemySet[7].EnemySetGroup.Length; i++)
                    {
                        if (GroundedEnemyTags.Contains(EnemySet[7].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[7].EnemySetGroup[i], GroundedSpawnPoints[GroundedRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));

                        }
                        else if (SpecialEnemyTags.Contains(EnemySet[7].EnemySetGroup[i].gameObject.tag))
                        {
                            Instantiate(EnemySet[7].EnemySetGroup[i], TurretSpawnPoint[TurretRandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
                        }
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

        