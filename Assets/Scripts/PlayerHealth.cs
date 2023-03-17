using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    private int maxHp = 100;
    public bool isDie;
    [SerializeField] private bool canDamage;
    private float delayTime = 1f;
    private float time;
    private int hpCard;
    private int shieldCard;
    private bool isShield;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject boxEffect;
    [SerializeField] private GameObject doorEffect;
    [SerializeField] private GameObject wallEffect;
    [SerializeField] private GameObject rockEffect;
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
            VFXManager.instance.Play("Heal");
            PlayerInventory.instance.UseCard("hp");
            GetHeal();
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && shieldCard > 0)
        {
            //VFXManager.instance.Play("Shield");
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
            //Debug.Log(hp);
            hp -= 1;
            time = 0;
            StartCoroutine(PlayerHurt());
            if(hp <= 0)
            {
                SetPlayerDie();
            }
        }
        isShield = false;
        //shield.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageNoDestroy"))
        {
            if (!playerMove.isDash)
            {
                GetDamage();
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), collision);
            }
        }
        else if (collision.CompareTag("Rock"))
        {
            if (!playerMove.isDash)
            {
                GetDamage();
                if (!isDie)
                {
                    Destroy(collision.gameObject);
                    GameObject rockObj = Instantiate(rockEffect, collision.transform.position, collision.transform.rotation);
                    VFXManager.instance.Play("Crash");
                    StartCoroutine(DestroyEffect(rockObj));
                }
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), collision);
            }
        }
        else if(collision.CompareTag("Box"))
        {
            GameObject boxObj = Instantiate(boxEffect, collision.transform.position, collision.transform.rotation);
            StartCoroutine(DestroyEffect(boxObj));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if (!playerMove.isDash)
            {
                GetDamage();
                if (!isDie)
                {
                    Destroy(collision.collider.gameObject);
                    GameObject wallObj = Instantiate(wallEffect, collision.transform.position, collision.transform.rotation);
                    VFXManager.instance.Play("Crash");
                    StartCoroutine(DestroyEffect(wallObj));
                }
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(),collision.collider);
            }
        }
        else if (collision.collider.CompareTag("Door"))
        {
            if (!playerMove.isDash)
            {
                GetDamage();
                if (!isDie)
                {
                    Destroy(collision.collider.gameObject);
                    GameObject doorObj = Instantiate(doorEffect, collision.transform.position, collision.transform.rotation);
                    VFXManager.instance.Play("Crash");
                    StartCoroutine(DestroyEffect(doorObj));
                }
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), collision.collider);
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
    IEnumerator PlayerHurt()
    {
        Color color = sr.color;
        color.a = 0.5f;
        sr.color= color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        sr.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        sr.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        sr.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        sr.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        sr.color = color;
    }

    IEnumerator PlayerDie()
    {
        Time.timeScale = 0.5f;
        PlayerAnimation.instance.DieAnim();
        VFXManager.instance.Play("Dead");
        isDie = true;
        //yield return new WaitForSeconds(0.1f);
        //dieEffect.Play();
        yield return new WaitForSeconds(1.5f);
        gameManager.GameOver();
    }

    IEnumerator DestroyEffect(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        if(obj != null)
        {
            Destroy(obj);
        }
    }
}
