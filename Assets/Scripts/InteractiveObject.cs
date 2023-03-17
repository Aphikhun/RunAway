using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private int random;
    [SerializeField] private int loop;
    [SerializeField] private GameObject effect;

    private void RandomItem()
    {
        int randomType = Random.Range(1, 101);
        if (randomType > 30)
        {
            int randomItem = Random.Range(1, 101);
            if (randomItem > 10)
            {
                random = Random.Range(1, 3);
            }
            else
            {
                random = 3;
            }
        }
        else
        {
            random = Random.Range(4, 7);
        }
        PlayerInventory.instance.GetCard(random);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for(int i = 0;i < loop; i++)
            {
                RandomItem();
            }
            VFXManager.instance.Play("PickUp");
               
            Destroy(gameObject);
        }
    }
}
