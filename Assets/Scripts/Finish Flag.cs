using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishFlag : MonoBehaviour
{
    [SerializeField] private GameObject FinishedLevelMenuUI;
    
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UnlockNewLevel();
            SaveCollectedCoins(gameManager.GetCollectedCoins());
            audioManager.StopBGM();
            FinishedLevelMenuUI.SetActive(true);
            Debug.Log("Player reached the finish line");
        }
    }
    
    private void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void SaveCollectedCoins(int collectedCoins)
    {
        if(collectedCoins > PlayerPrefs.GetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex - 1), 0))
        {
            PlayerPrefs.SetInt("CollectedCoins_Level " + (SceneManager.GetActiveScene().buildIndex - 1), collectedCoins);
            PlayerPrefs.Save();
        }
    }
}
