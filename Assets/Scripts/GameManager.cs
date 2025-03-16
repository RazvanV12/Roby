using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

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
    private TextMeshProUGUI highScoreText;
    private bool levelEnded = false;

    [SerializeField] private Image pauseButtonImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite unpauseSprite;
    
    [SerializeField] private Image DropDownBackground; 
    
    private bool dropDownPannelIsOpen = false;
    private bool bgmIsActive = true;
    private bool sfxIsActive = true;

    private bool isPaused;

    [SerializeField] private GameObject activateBGM;
    [SerializeField] private GameObject activateSFX;
    [SerializeField] private GameObject openMainMenu;
    
    [SerializeField] private GameObject DisabledBGMImage;
    [SerializeField] private GameObject DisabledSFXImage;
    [SerializeField] private GameObject playerDiedPanel;

    [SerializeField] private Slider ProgressBar;
    private float progress = 0f;
    private float initialPositionOnX;
    private float totalDistanceToTravel;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        initialPositionOnX = playerPosition.position.x;
        totalDistanceToTravel = GameObject.Find("FinishFlag").transform.position.x - initialPositionOnX;
        if (PlayerPrefs.GetString("BgmEnabled") == "false")
        {
            bgmIsActive = false;
            DisabledBGMImage.SetActive(true);
        }
        if (PlayerPrefs.GetString("SfxEnabled") == "false")
        {
            sfxIsActive = false;
            DisabledSFXImage.SetActive(true);
        }
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

        // if(playerPosition.position.y is < -3f or > 11f)
        // {
        //     DiedMenuUI.SetActive(true);
        //     levelEnded = true;
        //     audioManager.StopBGM();
        // }

        progress = playerPosition.position.x - initialPositionOnX;
        ProgressBar.value = progress / totalDistanceToTravel;
    }
    
    public void FinishedLevel()
    {
        levelEnded = true;
        UnlockNewLevel();
        SaveCollectedCoins();
        ModifyLevelClearPanel();
        LevelClearPanel.SetActive(true);
        audioManager.PauseBGM();
        var playerMovement = playerPosition.GetComponent<PlayerMovement>();
        playerMovement.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void ModifyLevelClearPanel()
    {
        star1Image = LevelClearPanel.transform.GetChild(2).GetComponent<Image>();
        star2Image = LevelClearPanel.transform.GetChild(3).GetComponent<Image>();
        star3Image = LevelClearPanel.transform.GetChild(4).GetComponent<Image>();
        scoreText = LevelClearPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        highScoreText = LevelClearPanel.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        
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
        var score = (float)Math.Round(CalculateScore(), 2);
        scoreText.text = "Score: " + score.ToString("F2");
        if(score < PlayerPrefs.GetFloat("HighScore_Level " + SceneManager.GetActiveScene().buildIndex, float.MaxValue))
        {
            highScoreText.text = "High Score: " + score;
            if(!Mathf.Approximately(PlayerPrefs.GetFloat("HighScore_Level " + SceneManager.GetActiveScene().buildIndex, float.MaxValue), float.MaxValue))
                PlayerPrefs.SetFloat("TotalScore", PlayerPrefs.GetFloat("TotalScore", 0f) - PlayerPrefs.GetFloat("HighScore_Level " + SceneManager.GetActiveScene().buildIndex, 0f) + score);
            else 
                PlayerPrefs.SetFloat("TotalScore", PlayerPrefs.GetFloat("TotalScore", 0f) + score);
            PlayerPrefs.SetFloat("HighScore_Level " + SceneManager.GetActiveScene().buildIndex, score);
            PlayerPrefs.Save();
        }
        else
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore_Level " + SceneManager.GetActiveScene().buildIndex, float.MaxValue);
        }
    }

    private float CalculateScore()
    {
        return timeSinceGameStarted - 5 * coinsCollected;
    }
    
    private void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("UnlockedLevel"))//PlayerPrefs.GetInt("ReachedIndex")))
        {
            //PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.Save();
        }
    }

    private void SaveCollectedCoins()
    {
        if(coinsCollected > PlayerPrefs.GetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex), 0))
        {
            PlayerPrefs.SetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex), coinsCollected);
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
    
    public void PauseGame()
    {
        if (!isPaused)
        {
            audioManager.PlaySfx(audioManager.PauseGameClip);
            Time.timeScale = 0;
            pauseButtonImage.sprite = unpauseSprite;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseButtonImage.sprite = pauseSprite;
            audioManager.PlaySfx(audioManager.UnpauseGameClip);
            isPaused = false;
        }
        AudioListener.pause = isPaused;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game restarted");
    }

    public void DropDownButton()
    {
        if (!dropDownPannelIsOpen)
        {
            DropDownBackground.enabled = true;
            activateBGM.SetActive(true);
            activateSFX.SetActive(true);
            openMainMenu.SetActive(true);
            dropDownPannelIsOpen = true;
        }
        else
        {
            DropDownBackground.enabled = false;
            activateBGM.SetActive(false);
            activateSFX.SetActive(false);
            openMainMenu.SetActive(false);
            dropDownPannelIsOpen = false;
        }
    }

    public void ActivateBGM()
    {
        if (bgmIsActive)
        {
            audioManager.PauseBGM();
            bgmIsActive = false;
            DisabledBGMImage.SetActive(true);
            PlayerPrefs.SetString("BgmEnabled", "false");
            PlayerPrefs.Save();
        }
        else
        {
            audioManager.UnPauseBGM();
            bgmIsActive = true;
            DisabledBGMImage.SetActive(false);
            PlayerPrefs.SetString("BgmEnabled", "true");
            PlayerPrefs.Save();
        }  
    }
    
    public void ActivateSfx()
    {
        if (sfxIsActive)
        {
            audioManager.DisableSFX();
            sfxIsActive = false;
            DisabledSFXImage.SetActive(true);
            PlayerPrefs.SetString("SfxEnabled", "false");
            PlayerPrefs.Save();
        }
        else
        {
            audioManager.EnableSFX();
            sfxIsActive = true;
            DisabledSFXImage.SetActive(false);
            PlayerPrefs.SetString("SfxEnabled", "true");
            PlayerPrefs.Save();
        }  
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void OpenMainMenu()
    {
        if (isPaused)
        {
            PauseGame();
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickButton()
    {
        if (audioManager.SfxEnabled == false)
        {
            audioManager.EnableSFX();
            audioManager.PlaySfx(audioManager.ButtonClickClip);
            audioManager.DisableSFX();
        }
        else
        {
            audioManager.PlaySfx(audioManager.ButtonClickClip);
        }
    }

    public void PreparePlayerDiedPanel()
    {
        if(progress/totalDistanceToTravel > 0f)
            playerDiedPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "Progress Done: " +
                (progress/totalDistanceToTravel).ToString("P");
        else
        {
            playerDiedPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "Progress Done: 0%";
        }
    }

    public void DisableTopRightButtons()
    {
        var pauseButton = GameObject.Find("Pause Button");
        var dropDownBackground = GameObject.Find("DropDown Background");
        var resetButton = GameObject.Find("Reset Button");

        pauseButton.GetComponent<Button>().interactable = false;
        dropDownBackground.transform.GetChild(0).GetComponent<Button>().interactable = false;
        resetButton.GetComponent<Button>().interactable = false;
    }
    
    public void EnableTopRightButtons()
    {
        var pauseButton = GameObject.Find("Pause Button");
        var dropDownBackground = GameObject.Find("DropDown Background");
        var resetButton = GameObject.Find("Reset Button");

        pauseButton.GetComponent<Button>().interactable = true;
        dropDownBackground.transform.GetChild(0).GetComponent<Button>().interactable = true;
        resetButton.GetComponent<Button>().interactable = true;
    }
    public void UnFreezeGame()
    {
        Time.timeScale = 1f;
    }

    public void TurnOnSfx()
    {
        audioManager.EnableSFX();
    }

    public GameObject PlayerDiedPanel => playerDiedPanel;

    public Transform PlayerPosition => playerPosition;

    public GameObject GetDiedMenuUI => DiedMenuUI;
}
