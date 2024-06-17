using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMovement;
    
    
    [SerializeField] private float boostAmount = 5f;
    [SerializeField] private float duration = 5f;
    // Start is called before the first frame update
    [SerializeField] private AudioManager audioManager;
    
    private bool isCollected;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found!");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            ApplySpeedBoost();
            isCollected = true;
            audioManager.PlaySfx(audioManager.SpeedUpClip);
        }
    }
    
    private void ApplySpeedBoost()
    {
        if (playerMovement != null)
        {
            playerMovement.MaxSpeed += boostAmount;
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(RemoveSpeedBoost());
        }
    }

    private IEnumerator RemoveSpeedBoost()
    {
        yield return new WaitForSeconds(duration);
        audioManager.PlaySfx(audioManager.ExpireBuffClip);
        playerMovement.MaxSpeed -= boostAmount;
        Destroy(gameObject);
    }
}
