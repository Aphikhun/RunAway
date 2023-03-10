using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform checkGround;
    [SerializeField] private bool isGround;

    [SerializeField] private GameObject sliderBar;
    [SerializeField] private Slider slider;

    [SerializeField] private LayerMask groundLayer;
    public float jumpForce = 5f;
    private float time;
    private float fly_count;
    private float fly_dur = 5f;
    private float dash_dur = 0.2f;
    private float dash_speed = 12f;
    private bool isDash;
    private bool isFly;

    private int jumpCard;
    private int dashCard;
    private int flyCard;

    private float slow_speed = 2f;
    private bool canDo;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        time = 0;
        fly_count = fly_dur;
        isDash = false;
        isFly = false;
        canDo= true;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapBox(checkGround.position, new Vector2(1, 1), 0, groundLayer);
        jumpCard = PlayerInventory.instance.GetCardAmount("jump");
        dashCard = PlayerInventory.instance.GetCardAmount("dash");
        flyCard = PlayerInventory.instance.GetCardAmount("fly");

        MovePlayer();
        Dash();

        if(!isFly)
        {
            sliderBar.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space) && canDo)
            {
                if (isGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (jumpCard > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    PlayerInventory.instance.UseCard("jump");
                }
            }
        }
        else
        {
            sliderBar.SetActive(true);
            Slider();

            fly_count -= Time.deltaTime;
            if(fly_count <= 0)
            {
                fly_count = fly_dur;
                isFly = false;
                rb.gravityScale = 1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.gravityScale = -1f;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                rb.gravityScale = 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && dashCard > 0 && canDo)
        {
            PlayerInventory.instance.UseCard("dash");

            isDash = true;
        }

        if(Input.GetKeyDown(KeyCode.Alpha4) && flyCard > 0 && canDo)
        {
            PlayerInventory.instance.UseCard("fly");

            isFly = true;   
        }

    }
    private void MovePlayer()
    {
        Vector2 a = transform.position;
        Vector2 b = new Vector2(-4f, transform.position.y);
        if (!isDash && isGround && transform.position.x != -4f)
        {
            canDo = false;
            transform.position = Vector2.MoveTowards(a, b, slow_speed * Time.deltaTime);
        }
        else if (transform.position.x == -4f)
        {
            canDo = true;
        }
    }

    private void Dash()
    {
        if (isDash)
        {
            rb.velocity = new Vector2(dash_speed, transform.position.y);
            time += Time.deltaTime;
            if (time > dash_dur)
            {
                isDash = false;
                time = 0;
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Slider()
    {
        slider.maxValue = fly_dur;
        slider.value = fly_count;
    }
}
