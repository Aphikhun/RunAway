using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private float speed;

    private PlayerMovement playerMovement;
    private SpawnObstacle spawnObstacle;

    [SerializeField] private GameObject check;

    [SerializeField] private float destroyPos;
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        spawnObstacle= FindAnyObjectByType<SpawnObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerHealth.instance.isDie)
        {
            speed = playerMovement.stageSpeed;

            MoveObstacle(speed);
            CheckSpawn();
        }
    }
    private void MoveObstacle(float useSpeed)
    {
        transform.position = new Vector2(transform.position.x - useSpeed * Time.deltaTime, transform.position.y);

        if (transform.position.x <= destroyPos)
        {
            Destroy(gameObject);
            //spawnObstacle.DecreaseObstacle();
        }
        
    }
    private void CheckSpawn()
    {
        if(check!= null)
        {
            if (check.transform.position.x <= 0)
            {
                Destroy(check.gameObject);
                spawnObstacle.DecreaseObstacle();
            }
        }
    }
}