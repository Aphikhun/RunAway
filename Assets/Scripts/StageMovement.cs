using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    private float speed;

    private PlayerMovement playerMovement;

    [SerializeField] private float max_x;
    [SerializeField] private float min_x;

    void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerHealth.instance.isDie)
        {
            speed = playerMovement.stageSpeed;

            MoveScene(speed);
        }
    }
    private void MoveScene(float useSpeed)
    {
        transform.position = new Vector2(transform.position.x - useSpeed * Time.deltaTime, transform.position.y);
        
        if(transform.position.x <= min_x)
        {
            transform.position = new Vector2(max_x, transform.position.y);
        }
    }
}
