using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public static Action OnPlayerDeath;
    private void OnEnable()
    {
        OnPlayerDeath += KillPlayer;
    }
    
    private void OnDisable()
    {
        OnPlayerDeath -= KillPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void KillPlayer()
    {
        Debug.Log("Player killed");
        audioManager.PlaySfx(audioManager.PlayerHitClip);
        gameManager.GetDiedMenuUI.SetActive(true);
        audioManager.StopBGM();
        //audioManager.StopBGM();
        //gameManager.PlayerDiedPanel.SetActive(true);
    }
}
