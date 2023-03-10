using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrap : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Transform pos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(rockPrefab,pos);
            Destroy(gameObject);
        }
    }
}
