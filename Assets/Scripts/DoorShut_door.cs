using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShut_door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BreakPoint"))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
