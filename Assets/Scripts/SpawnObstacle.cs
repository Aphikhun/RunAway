using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    private int num;
    private int obstacle;
    private int totalObstacle;
    
    
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform spawnPosition;
    
    void Awake()
    {
        num = 0;
        obstacle = 1;
        totalObstacle = 1;
    }

    
    void Update()
    {
        if(obstacle < totalObstacle && !PlayerHealth.instance.isDie)
        {
            if (num < 3)
            {
                Instantiate(obstaclePrefabs[Random.Range(1, obstaclePrefabs.Length)], spawnPosition);
                num++;
                obstacle++;
            }
            else
            {
                Instantiate(obstaclePrefabs[0], spawnPosition);
                num = 0;
                obstacle++;
            }
        }
    }
    public void DecreaseObstacle()
    {
        obstacle--;
    }
}
