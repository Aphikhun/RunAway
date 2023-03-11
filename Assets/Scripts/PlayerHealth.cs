using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    private int maxHp = 3;
    public bool isDie;
    [SerializeField] private bool canDamage;
    private float delayTime = 1f;
    private float time;
    private int hpCard;
    private int shieldCard;
    private bool isShield;
    [SerializeField] private GameObject shield;

    public static PlayerHealth instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        hp = maxHp;
        time = delayTime;
        isDie = false;
        isShield = false;
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
        if (hp > 0 && !isShield)
        {
            Debug.Log(hp);
            hp -= 1;
            time = 0;
            if(hp <= 0)
            {
                isDie = true;
            }
        }
        isShield = false;
        //shield.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damageable") && canDamage)
        {
            GetDamage();
            if(!isDie)
            {
                Destroy(collision.gameObject);
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
        isDie = true;
    }
}
