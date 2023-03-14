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

    private LevelLoader level_loader;
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        time = 0;

        Time.timeScale = 1;

        level_loader = FindAnyObjectByType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp_txt != null && score_txt != null)
        {
            hp_txt.text = PlayerHealth.instance.hp.ToString();
            score_txt.text = "SCORE  " + score.ToString();
        }

        if (!PlayerHealth.instance.isDie) 
        {
            CountScore();
            SetCardAmount();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause && !isOption)
                {
                    Pause();
                }
                else if (isPause && !isOption)
                {
                    Continue();
                }
                else if (isOption)
                {
                    CloseOption();
                }
            }

            if (Input.GetKeyDown(KeyCode.I) && !isPause)
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
        }
    }

    private void CountScore()
    {
        if (time < 0.1)
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
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        result_txt.text = score.ToString();
        GameOverPanel.SetActive(true);
    }
    public void CloseOption()
    {
        isOption = false;
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
        level_loader.LoadNextLevel();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        level_loader.LoadSameLevel();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Option()
    {
        isOption = true;
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
