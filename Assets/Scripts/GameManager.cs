using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private int score;
    private float time;

    private int speedCard;
    public float stageSpeed;
    private float ex_stageSpeed;
    private bool useCard = false;
    private float duration = 10f;
    private float speed_time = 0;

    [SerializeField] private Text hp_txt = null;
    [SerializeField] private Text score_txt = null;
    [SerializeField] private Text result_txt = null;

    private bool isPause;
    [SerializeField] private GameObject PausePanel = null;

    private bool isShowInventory = true;
    [SerializeField] private GameObject InventoryPanel = null;

    [SerializeField] private GameObject GameOverPanel = null;

    private bool isOption = false;
    [SerializeField] private GameObject OptionPanel = null;

    [SerializeField] private Text[] card_txt = null;
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        time = 0;
        stageSpeed = 3f;
        ex_stageSpeed = 6f;

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        speedCard = PlayerInventory.instance.GetCardAmount("speed");

        CountScore();
        SetCardAmount();
        PlayerDie();
        StageMove();

        if(hp_txt != null && score_txt != null)
        {
            hp_txt.text = PlayerHealth.instance.hp.ToString();
            score_txt.text = score.ToString();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !PlayerHealth.instance.isDie)
        {
            if(!isPause && !isOption)
            {
                Pause();
            }
            else if(isPause && !isOption)
            {
                Continue();
            }
            else if(isOption)
            {
                CloseOption();
            }
        }

        if(Input.GetKeyDown(KeyCode.I) && !PlayerHealth.instance.isDie && !isPause)
        {
            if (isShowInventory)
            {
                HideInventory();
            }
            else
            {
                ShowInventory();
            }
        }

        if(stageSpeed == ex_stageSpeed)
        {
            PlayerAnimation.instance.RunAnim(true);
        }
        else
        {
            PlayerAnimation.instance.RunAnim(false);
        }
    }

    private void CountScore()
    {
        if (time < 1)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            score += 1;
        }
    }
    private void SetCardAmount()
    {
        int i = 0;
        if(card_txt.Length > 0)
        {
            foreach (var cards in PlayerInventory.instance.Cards)
            {
                card_txt[i].text = cards.Value.ToString();
                i++;
            }
        }
    }
    private void StageMove()
    {
        if(card_txt.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && speedCard > 0)
            {
                useCard = true;
                PlayerInventory.instance.UseCard("speed");
            }
        }

        if (useCard)
        {
            stageSpeed = ex_stageSpeed;

            speed_time += Time.deltaTime;
            if (speed_time >= duration)
            {
                useCard = false;
                speed_time = 0;
            }
        }
        else
        {
            stageSpeed = 3f;
        }
    }
    private void PlayerDie()
    {
        if(card_txt.Length > 0)
        {
            if (PlayerHealth.instance.isDie)
            {
                Time.timeScale = 0f;
                result_txt.text = score.ToString();
                GameOverPanel.SetActive(true);
            }
        }
    }
    public void CloseOption()
    {
        OptionPanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
        PausePanel.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        isPause = false;
        PausePanel.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Option()
    {
        OptionPanel.SetActive(true);
    }
    private void ShowInventory()
    {
        isShowInventory= true;
        InventoryPanel.SetActive(true);
    }
    private void HideInventory()
    {
        isShowInventory= false;
        InventoryPanel.SetActive(false);
    }
}
