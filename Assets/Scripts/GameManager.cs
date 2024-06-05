using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeSinceGameStarted;
    [SerializeField] private int coinsCollected = 0;

    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI uiCoins;

    [SerializeField] private GameObject DiedMenuUI;
    [SerializeField] private GameObject FinishedLevelUI;
    [SerializeField] private GameObject LevelClearPanel;
    [SerializeField] private Transform playerPosition;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private FinishFlag finishFlag;

    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    
    private Image star1Image;
    private Image star2Image;
    private Image star3Image;
    private TextMeshProUGUI scoreText;
    private bool levelEnded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelEnded)
        {
            timeSinceGameStarted += Time.deltaTime;
            uiTimer.text = "Timer: " + timeSinceGameStarted.ToString("F2");
            uiCoins.text = "Coins: " + coinsCollected;
        }

        if(playerPosition.position.y < -3f)
        {
            DiedMenuUI.SetActive(true);
            levelEnded = true;
            audioManager.StopBGM();
        }
    }
    
    public void FinishedLevel()
    {
        levelEnded = true;
        UnlockNewLevel();
        SaveCollectedCoins();
        ModifyLevelClearPanel();
        LevelClearPanel.SetActive(true);
        audioManager.StopBGM();
    }

    private void ModifyLevelClearPanel()
    {
        star1Image = LevelClearPanel.transform.GetChild(2).GetComponent<Image>();
        star2Image = LevelClearPanel.transform.GetChild(3).GetComponent<Image>();
        star3Image = LevelClearPanel.transform.GetChild(4).GetComponent<Image>();
        scoreText = LevelClearPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        
        star1Image.sprite = emptyStar;
        star2Image.sprite = emptyStar;
        star3Image.sprite = emptyStar;

        switch (coinsCollected)
        {
            case 1:
                star1Image.sprite = fullStar;
                break;
            case 2:
                star1Image.sprite = fullStar;
                star2Image.sprite = fullStar;
                break;
            case 3:
                star1Image.sprite = fullStar;
                star2Image.sprite = fullStar;
                star3Image.sprite = fullStar;
                break;
        }

        scoreText.text = "Score: " + CalculateScore().ToString("F2");
    }

    private float CalculateScore()
    {
        return timeSinceGameStarted - 5 * coinsCollected;
    }
    
    private void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void SaveCollectedCoins()
    {
        if(coinsCollected > PlayerPrefs.GetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex - 1), 0))
        {
            PlayerPrefs.SetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex - 1), coinsCollected);
            PlayerPrefs.Save();
        }
    }
    // getter and setter for coinsCollected
    public void AddCollectedCoin()
    {
        coinsCollected++;
        UpdateCoinUI();
    }
    private void UpdateCoinUI()
    {
        Debug.Log("Coins: " + coinsCollected);
    }
}
