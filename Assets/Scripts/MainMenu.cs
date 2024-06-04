using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelsMenu levelsMenu;
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
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
