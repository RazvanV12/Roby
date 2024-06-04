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
    
    private void Awake()
    {
        ReloadLevelsPanel();
    }

    public void ReloadLevelsPanel()
    {
        var unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
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

        for (var i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            star1Image = buttons[i].GameObject().transform.GetChild(1).GetComponent<Image>();
            star2Image = buttons[i].GameObject().transform.GetChild(2).GetComponent<Image>();
            star3Image = buttons[i].GameObject().transform.GetChild(3).GetComponent<Image>();
            lockedImage = buttons[i].GameObject().transform.GetChild(4).GetComponent<Image>();
            switch(PlayerPrefs.GetInt("CollectedCoins_Level " + (i + 1), 0))
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
        var levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
