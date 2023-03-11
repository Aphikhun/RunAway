using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalBox;
    [SerializeField] private GameObject specialBox;

    [SerializeField] private Transform[] spawnPoints;

    private List<GameObject> boxs = new();
    private List<GameObject> randomBoxs = new();
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            boxs.Add(normalBox);
        }
        boxs.Add(specialBox);

        randomBoxs = boxs.OrderBy(x => Random.value).ToList();

        int j = 0;
        foreach(var box in randomBoxs)
        {
            Instantiate(box, spawnPoints[j]);
            j++;
        }
    }
}
