using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelsMenu levelsMenu;
    [SerializeField] private GameObject totalScoreText;
    [SerializeField] private GameObject levelsCompletedText;
    
    private void Start()
    {
        totalScoreText = GameObject.Find("Total Score Text");
        levelsCompletedText = GameObject.Find("Levels Completed Text");
        totalScoreText.GetComponent<TextMeshProUGUI>().text = "Total Score: " + PlayerPrefs.GetFloat("TotalScore", 0);
        levelsCompletedText.GetComponent<TextMeshProUGUI>().text = "Levels Completed: " + (PlayerPrefs.GetInt("UnlockedLevel", 1) - 1);
    }
    public void NewGame()
    {
        for(int i = 0; i < 9; i++)
        {
            PlayerPrefs.SetInt("CollectedCoins_Level " + (i + 1), 0);
        }
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
        levelsMenu.ReloadLevelsPanel();
    }
    public void QuitGame()
    {
        Debug.Log("Game quit");
    }

    public void OptionsMenu()
    {
        Debug.Log("Options menu coming soon");
    }
}
