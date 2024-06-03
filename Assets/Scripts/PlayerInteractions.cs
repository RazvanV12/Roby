using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
    }
}
