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

    public GameObject Shieldenemy;
    public float ShieldenemyCount;

    private int RandomSpawnNumber;

    private float SpawnCoolDown;

    // Update is called once per frame
    void Update()
    {
        enemySpwan();

        RandomSpawnNumber = Random.Range(0, 5);

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
        if (EnemyCount < 15 && SpawnCoolDown <= 0)
        {
                                   // X,Z
            //XPosition = Random.Range(0, 0);
            //ZPosition = Random.Range(0, 0);
            //                                  X ,     Y ,     Z
            //Instantiate(enemy, new Vector3(XPosition, 0, ZPosition), new Quaternion(0, 90, 0, 0));

            Instantiate(enemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
            EnemyCount += 1;

            SpawnCoolDown = 1;
        }

        if(ShieldenemyCount < 10 && SpawnCoolDown <= 0)
        {
            Instantiate(Shieldenemy, SpawnPoints[RandomSpawnNumber].transform.position, new Quaternion(0, 90, 0, 0));
            ShieldenemyCount += 1;

            SpawnCoolDown = 1;
        }
    }
}
