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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finishFlagObject;

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
        player = GameObject.FindGameObjectWithTag("Player");
        finishFlagObject = GameObject.FindGameObjectWithTag("Finish");
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            GenerateLevel.StartLevelGeneration(LevelLoader.levelToLoad);
            player.transform.position = GenerateLevel.playerStart;
            finishFlagObject.transform.position = GenerateLevel.playerEnd;
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        initialPositionOnX = player.transform.position.x;
        totalDistanceToTravel = GameObject.Find("FinishFlag").transform.position.x - initialPositionOnX;
        if (!UserSession.isBgmEnabled)
        {
            bgmIsActive = false;
            DisabledBGMImage.SetActive(true);
        }
        if (!UserSession.isSfxEnabled)
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

        progress = player.transform.position.x - initialPositionOnX;
        ProgressBar.value = progress / totalDistanceToTravel;
    }
    
    public void FinishedLevel()
    {
        if (!levelEnded)
        {
            levelEnded = true;
            UnlockNewLevel();
            SaveCollectedCoins();
            ModifyLevelClearPanel();
        }
        LevelClearPanel.SetActive(true);
        audioManager.PauseBGM();
        var playerMovement = player.transform.GetComponent<PlayerMovement>();
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
        if (SceneManager.GetActiveScene().buildIndex != 10)
        {
            if (UserSession.highScores.Count >= SceneManager.GetActiveScene().buildIndex)
            {
                if (score < UserSession.highScores[SceneManager.GetActiveScene().buildIndex - 1])
                {
                    highScoreText.text = "High Score: " + score;
                    UserSession.totalScore +=
                        score - UserSession.highScores[SceneManager.GetActiveScene().buildIndex - 1];
                    UserSession.highScores[SceneManager.GetActiveScene().buildIndex - 1] = score;
                    DbRepository.UpdateUserStats();
                }
                else
                {
                    highScoreText.text = "High Score: " +
                                         UserSession.highScores[SceneManager.GetActiveScene().buildIndex - 1];
                }
            }
            else
            {
                UserSession.highScores.Add(score);
                UserSession.totalScore += score;
                DbRepository.UpdateUserStats();
                highScoreText.text = "High Score: " + score;
            }
        }
        else
        {
            if (UserSession.highScores.Count >= LevelLoader.levelToLoad)
            {
                if (score < UserSession.highScores[LevelLoader.levelToLoad - 1])
                {
                    highScoreText.text = "High Score: " + score;
                    UserSession.totalScore +=
                        score - UserSession.highScores[LevelLoader.levelToLoad - 1];
                    UserSession.highScores[LevelLoader.levelToLoad - 1] = score;
                    DbRepository.UpdateUserStats();
                }
                else
                {
                    highScoreText.text = "High Score: " +
                                         UserSession.highScores[LevelLoader.levelToLoad - 1];
                }
            }
            else
            {
                UserSession.highScores.Add(score);
                UserSession.totalScore += score;
                DbRepository.UpdateUserStats();
                highScoreText.text = "High Score: " + score;
            }
        }
    }

    private float CalculateScore()
    {
        return timeSinceGameStarted - 5 * coinsCollected;
    }
    
    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (LevelLoader.levelToLoad != UserSession.levelsCompleted + 1) return;
            UserSession.levelsCompleted += 1;
            DbRepository.UpdateUserStats();
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != UserSession.levelsCompleted + 1) return;
            UserSession.levelsCompleted += 1;
            DbRepository.UpdateUserStats();
        }
    }

    private void SaveCollectedCoins()
    {
        if (SceneManager.GetActiveScene().buildIndex != 10)
        {
            if (UserSession.maxStars.Count >= SceneManager.GetActiveScene().buildIndex)
            {
                if (coinsCollected > UserSession.maxStars[SceneManager.GetActiveScene().buildIndex] - 1)
                {
                    UserSession.maxStars[SceneManager.GetActiveScene().buildIndex - 1] = coinsCollected;
                }
            }
            else
            {
                UserSession.maxStars.Add(coinsCollected);
            }
        }
        else
        {
            if (UserSession.maxStars.Count >= LevelLoader.levelToLoad)
            {
                if (coinsCollected > UserSession.maxStars[LevelLoader.levelToLoad] - 1)
                {
                    UserSession.maxStars[LevelLoader.levelToLoad - 1] = coinsCollected;
                }
            }
            else
            {
                UserSession.maxStars.Add(coinsCollected);
            }
        }

        DbRepository.UpdateUserStats();
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
        if (SceneManager.GetActiveScene().buildIndex < 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene("Level 10");
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
            UserSession.isBgmEnabled = false;
            DbRepository.UpdateUserAudioSettings();
        }
        else
        {
            audioManager.UnPauseBGM();
            bgmIsActive = true;
            DisabledBGMImage.SetActive(false);
            UserSession.isBgmEnabled = true;
            DbRepository.UpdateUserAudioSettings();
        }  
    }
    
    public void ActivateSfx()
    {
        if (sfxIsActive)
        {
            audioManager.DisableSFX();
            sfxIsActive = false;
            DisabledSFXImage.SetActive(true);
            UserSession.isSfxEnabled = false;
            DbRepository.UpdateUserAudioSettings();
        }
        else
        {
            audioManager.EnableSFX();
            sfxIsActive = true;
            DisabledSFXImage.SetActive(false);
            UserSession.isSfxEnabled = true;
            DbRepository.UpdateUserAudioSettings();
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

    public Transform PlayerPosition => player.transform;

    public GameObject GetDiedMenuUI => DiedMenuUI;
}
