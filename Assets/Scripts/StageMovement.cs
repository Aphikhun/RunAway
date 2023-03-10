using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    public float speed = 3f;
    private int speedCard;
    private float ex_speed = 6f;
    private bool useCard;
    private float time;
    private float duration = 10f;

    [SerializeField] private float max_x;
    [SerializeField] private float min_x;

    void Awake()
    {
        time = 0;
        useCard = false;
    }

    // Update is called once per frame
    void Update()
    {
        speedCard = PlayerInventory.instance.GetCardAmount("speed");

        if(Input.GetKeyDown(KeyCode.Alpha1) && speedCard > 0)
        {
            useCard = true;
        }

        if(useCard)
        {
            MoveScene(ex_speed);

            time += Time.deltaTime;
            if(time >= duration)
            {
                useCard= false;
                time = 0;
            }
        }
        else
        {
            MoveScene(speed);
        }
        
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
