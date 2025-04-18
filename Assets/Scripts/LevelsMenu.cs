using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public Button[] buttons;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    private Image star1Image;
    private Image star2Image;
    private Image star3Image;
    private Image lockedImage;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject aheadButton;
    private int currentLevelsSet = 0;
    
    private void Awake()
    {
        ReloadLevelsPanel();
    }

    public void ReloadLevelsPanel()
    {
        var unlockedLevel = UserSession.levelsCompleted == 0 ? 1 : UserSession.levelsCompleted + 1;
        foreach (var button in buttons)
        {
            button.interactable = false;
            star1Image = button.GameObject().transform.GetChild(1).GetComponent<Image>();
            star2Image = button.GameObject().transform.GetChild(2).GetComponent<Image>();
            star3Image = button.GameObject().transform.GetChild(3).GetComponent<Image>();
            lockedImage = button.GameObject().transform.GetChild(4).GetComponent<Image>();
            star1Image.sprite = emptyStar;
            star2Image.sprite = emptyStar;
            star3Image.sprite = emptyStar;
            lockedImage.gameObject.SetActive(true);
        }

        var loopLimit = (unlockedLevel > (9 * (currentLevelsSet + 1)) ? (9 * (currentLevelsSet + 1)) : unlockedLevel);
        for (var i = 9 * currentLevelsSet; i < loopLimit; i++)
        {
            var buttonNumber = i % 9;
            buttons[buttonNumber].interactable = true;
            star1Image = buttons[buttonNumber].GameObject().transform.GetChild(1).GetComponent<Image>();
            star2Image = buttons[buttonNumber].GameObject().transform.GetChild(2).GetComponent<Image>();
            star3Image = buttons[buttonNumber].GameObject().transform.GetChild(3).GetComponent<Image>();
            lockedImage = buttons[buttonNumber].GameObject().transform.GetChild(4).GetComponent<Image>();
            if(i < UserSession.maxStars.Count)
                switch(UserSession.maxStars[i])
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
            lockedImage.gameObject.SetActive(false);
        }
    }

    public void LoadLevel(int levelId)
    {
        var levelToLoad = levelId + 9 * currentLevelsSet;
        if(levelToLoad < 10){
            var levelName = "Level " + levelId;
            SceneManager.LoadScene(levelName);
        }

        LevelLoader.levelToLoad = levelToLoad;
        SceneManager.LoadScene("Level 10");
    }

    public void ResetLevelSets()
    {
        currentLevelsSet = 0;
        backButton.SetActive(false);
        if(UserSession.levelsCompleted < 9)
            aheadButton.SetActive(false);
        ReloadLevelsPanel();
    }
    
    public void NextLevelSet()
    {
        currentLevelsSet++;
        if(UserSession.levelsCompleted < 9 * (currentLevelsSet + 1))
            aheadButton.SetActive(false);
        backButton.SetActive(true);
        ReloadLevelsPanel();
    }
    
    public void PreviousLevelSet()
    {
        currentLevelsSet--;
        if(currentLevelsSet == 0)
            backButton.SetActive(false);
        aheadButton.SetActive(true);
        ReloadLevelsPanel();
    }
}
