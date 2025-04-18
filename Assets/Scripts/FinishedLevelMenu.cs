using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishedLevelMenu : MonoBehaviour
{
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 9)
                LevelLoader.levelToLoad = 10;
            else
            {
                LevelLoader.levelToLoad += 1;
            }
            SceneManager.LoadScene("Level 10");
        }

        Debug.Log("Next level loaded");
    }
    public void RestartLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene("Level 10");
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
