using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    private float speed;

    private GameManager gameManager;

    [SerializeField] private float max_x;
    [SerializeField] private float min_x;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = gameManager.stageSpeed;

        MoveScene(speed);
    }
    private void MoveScene(float useSpeed)
    {
        if (transform.position.x > min_x)
        {
            transform.position = new Vector2(transform.position.x - useSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector3(max_x, transform.position.y, transform.position.z);
        }
    }
}
