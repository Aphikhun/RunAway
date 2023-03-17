using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("DamageNoDestroy") || collision.CompareTag("Wall") || collision.CompareTag("Rock"))
        {
            VFXManager.instance.Play("Crash");
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
