using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishedLevelMenu : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next level loaded");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game restarted");
    }
    public void QuitGame()
    {
        Debug.Log("Game quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
