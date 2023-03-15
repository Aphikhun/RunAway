using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    private int num;
    [SerializeField] private GameObject[] obstaclePrefabs;
    
    void Awake()
    {
        num = 0;
    }

    
    void Update()
    {
        if(!PlayerHealth.instance.isDie)
        {
            if (num < 3)
            {
                Instantiate(obstaclePrefabs[Random.Range(1, obstaclePrefabs.Length)]);
                num++;
            }
            else
            {
                Instantiate(obstaclePrefabs[0]);
                num = 0;
            }
        }
    }
}
