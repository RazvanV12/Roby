using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelsMenu levelsMenu;
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
