using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMovement;
    // Start is called before the first frame update
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
        //if()
    }
}
