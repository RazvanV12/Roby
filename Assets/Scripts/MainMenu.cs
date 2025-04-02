using System;
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
        totalScoreText.GetComponent<TextMeshProUGUI>().text = "Total Score:" + UserSession.totalScore;
        levelsCompletedText.GetComponent<TextMeshProUGUI>().text = "Levels Completed:" + UserSession.levelsCompleted;
    }

    public void NewGame()
    {
        UserSession.ResetSession();
        DbRepository.UpdateUserStats();
        totalScoreText.GetComponent<TextMeshProUGUI>().text = "Total Score:" + UserSession.totalScore;
        levelsCompletedText.GetComponent<TextMeshProUGUI>().text = "Levels Completed:" + UserSession.levelsCompleted;
        levelsMenu.ReloadLevelsPanel();
    }
    public void Logout()
    {
        DbRepository.UpdateUserStats();
        UserSession.ClearSession();
        SceneManager.LoadScene("LoginMenu");
    }

    public void OptionsMenu(){
        Debug.Log("Options menu coming soon");
    }
}
