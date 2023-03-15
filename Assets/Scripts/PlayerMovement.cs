using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Transform checkGround;
    [SerializeField] private bool isGround;

    [SerializeField] private ParticleSystem dustEffect;

    [SerializeField] private GameObject sliderBar;
    [SerializeField] private Slider slider;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dash_cd;
    private float dash_cdCount;

    public float jumpForce = 6f;
    private float time;
    private float fly_count;
    private float fly_dur = 5f;
    private float dash_dur = 0.06f;
    private float dash_speed = 25f;
    public bool isDash;
    private bool canDash;
    private bool isFly;

    private int speedCard;
    public float stageSpeed;
    private float ex_stageSpeed;
    private bool isSpeed = false;
    private float speed_dur = 2f;
    private float speed_time = 0;

    private int jumpCard;
    private int dashCard;
    private int flyCard;

    private float slow_speed = 2f;
    private bool canDo;
    [SerializeField] private bool isJumpSkill;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        time = 0;
        stageSpeed = 3f;
        ex_stageSpeed = 6f;
        dash_cdCount = 0;
        speed_time = speed_dur;
        fly_count = fly_dur;
        isDash = false;
        isFly = false;
        canDo= true;
        isJumpSkill = false;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerHealth.instance.isDie)
        {
            jumpCard = PlayerInventory.instance.GetCardAmount("jump");
            dashCard = PlayerInventory.instance.GetCardAmount("dash");
            flyCard = PlayerInventory.instance.GetCardAmount("fly");
            speedCard = PlayerInventory.instance.GetCardAmount("speed");

            CheckGround();
            MovePlayer();
            StageMove();
            Slider();
            Dash();

            PlayerAnimation.instance.CheckFall(rb.velocity.y);
            if (!isFly)
            {
                if (Input.GetKeyDown(KeyCode.Space) && canDo)
                {
                    if (isGround)
                    {
                        CreateDust();
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    }
                    else if(jumpCard > 0)
                    {
                        CreateDust();
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        PlayerInventory.instance.UseCard("jump");
                        isJumpSkill= true;
                        //PlayerAnimation.instance.JumpSkillAnim(true);
                    }
                    PlayerAnimation.instance.JumpAnim(true);
                    PlayerAnimation.instance.JumpSkillAnim(isJumpSkill);
                }
                
            }
            else
            {
                fly_count -= Time.deltaTime;
                if (fly_count <= 0)
                {
                    CancelFly();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.gravityScale = -1f;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    rb.gravityScale = 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && dashCard > 0 && canDo && canDash)
            {
                PlayerInventory.instance.UseCard("dash");
                CancelFly();
                CancelSpeed();
                canDash = false;
                isDash = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && flyCard > 0 && canDo)
            {
                PlayerInventory.instance.UseCard("fly");
                CancelSpeed();
                fly_count = fly_dur;
                isFly = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && speedCard > 0)
            {
                PlayerInventory.instance.UseCard("speed");
                CancelFly();
                speed_time = speed_dur;
                isSpeed = true;
            }

            if (stageSpeed == ex_stageSpeed)
            {
                PlayerAnimation.instance.RunAnim(true);
            }
            else
            {
                PlayerAnimation.instance.RunAnim(false);
            }
            if(!canDash)
            {
                dash_cdCount += Time.deltaTime;
                if(dash_cdCount >= dash_cd)
                {
                    canDash = true;
                    dash_cdCount = 0;
                }
            }
        }
    }
    private void StageMove()
    {
        if (isSpeed)
        {
            stageSpeed = ex_stageSpeed;

            speed_time -= Time.deltaTime;
            if (speed_time <= 0)
            {
                CancelSpeed();
            }
        }
        else
        {
            stageSpeed = 3f;
        }
    }
    private void MovePlayer()
    {
        Vector2 a = transform.position;
        Vector2 b = new(-4f, transform.position.y);
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
            rb.gravityScale = 0f;
            rb.velocity = Vector2.right * dash_speed;
            time += Time.deltaTime;
            if (time > dash_dur)
            {
                rb.gravityScale = 1f;
                isDash = false;
                time = 0;
                rb.velocity = Vector2.zero;
            }
        }
    }
    private void CheckGround()
    {
        isGround = Physics2D.OverlapBox(checkGround.position, new Vector2(0.2f, 0.3f), 0, groundLayer);
        PlayerAnimation.instance.JumpAnim(!isGround);
        isJumpSkill= false;
        PlayerAnimation.instance.JumpSkillAnim(false);

    }

    private void Slider()
    {
        if(isFly)
        {
            sliderBar.SetActive(true);
            slider.maxValue = fly_dur;
            slider.value = fly_count;
        }
        else if(isSpeed)
        {
            sliderBar.SetActive(true);
            slider.maxValue = speed_dur;
            slider.value = speed_time;
        }
        else
        {
            sliderBar.SetActive(false);
        }
        
    }
    private void CancelFly()
    {
        isFly = false;
        rb.gravityScale = 1f;
    }
    private void CancelSpeed()
    {
        isSpeed = false;
        speed_time = speed_dur;
    }
    private void CreateDust()
    {
        dustEffect.Play();
    }
}
