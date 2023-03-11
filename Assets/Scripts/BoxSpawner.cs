using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalBox;
    [SerializeField] private GameObject specialBox;

    [SerializeField] private Transform[] spawnPoints;

    private GameObject[] boxs;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();



        for(int i = 0; i < spawnPoints.Length; i++)
        {
            
        }
    }
}
