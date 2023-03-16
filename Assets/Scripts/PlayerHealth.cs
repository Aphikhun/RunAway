using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    private int maxHp = 1;
    public bool isDie;
    [SerializeField] private bool canDamage;
    private float delayTime = 1f;
    private float time;
    private int hpCard;
    private int shieldCard;
    private bool isShield;
    [SerializeField] private ParticleSystem dieEffect;
    [SerializeField] private GameObject shield;
    private GameManager gameManager;
    private PlayerMovement playerMove;

    public static PlayerHealth instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        hp = maxHp;
        time = delayTime;
        isDie = false;
        isShield = false;

        gameManager = FindAnyObjectByType<GameManager>();
        playerMove = FindAnyObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        hpCard = PlayerInventory.instance.GetCardAmount("hp");
        shieldCard = PlayerInventory.instance.GetCardAmount("shield");
        CanGetDamage();

        if(Input.GetKeyDown(KeyCode.Alpha5) && hpCard > 0)
        {
            PlayerInventory.instance.UseCard("hp");
            GetHeal();
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && shieldCard > 0)
        {
            PlayerInventory.instance.UseCard("shield");
            shield.SetActive(true);
            isShield = true;
        }
    }

    public void GetHeal()
    {
        if(hp < maxHp)
        {
            hp += 1;
            if(hp >= maxHp) { hp = maxHp; }
        }
    }

    public void GetDamage()
    {
        if (hp > 0 && !isShield && canDamage)
        {
            Debug.Log(hp);
            hp -= 1;
            time = 0;
            if(hp <= 0)
            {
                SetPlayerDie();
            }
        }
        isShield = false;
        //shield.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damageable"))
        {
            if (!playerMove.isDash)
            {
                GetDamage();
                if (!isDie)
                {
                    Destroy(collision.collider.gameObject);
                }
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(),collision.collider);
            }

        }
    }
    private void CanGetDamage()
    {
        if(time < delayTime)
        {
            time += Time.deltaTime;
            canDamage = false;
        }
        else
        {
            canDamage =  true;
        }
    }
    public void SetPlayerDie()
    {
        StartCoroutine(PlayerDie());
    }

    IEnumerator PlayerDie()
    {
        Time.timeScale = 0.5f;
        PlayerAnimation.instance.DieAnim();
        isDie = true;
        //yield return new WaitForSeconds(0.1f);
        //dieEffect.Play();
        yield return new WaitForSeconds(1.5f);
        gameManager.GameOver();
    }
}
