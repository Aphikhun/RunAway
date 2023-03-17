using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.SetPlayerDie();
        }
        else if(collision.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
        }
    }
}
