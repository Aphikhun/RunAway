using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShut : MonoBehaviour
{
    [SerializeField] private Rigidbody2D doorRb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && doorRb != null)
        {
            doorRb.gravityScale = 1f;
        }
    }
}
