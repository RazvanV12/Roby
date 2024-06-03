using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDropper : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    [SerializeField] private Transform firstSpikePos;
    [SerializeField] private Transform secondSpikePos;
    [SerializeField] private Transform thirdSpikePos;

    [SerializeField] private float dropCooldown = 2f;
    [SerializeField] private float nextDropTime;
    [SerializeField] private float dropSpeedMultiplier = 1f;

    [SerializeField] private GameObject firstSpike;
    [SerializeField] private GameObject secondSpike;
    [SerializeField] private GameObject thirdSpike;
    
    [SerializeField] private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        if(other.gameObject.CompareTag("Player") && Time.time > nextDropTime)
        {
            DropSpikes();
            nextDropTime = Time.time + dropCooldown;
        }
    }

    private void DropSpikes()
    {
        audioManager.PlaySfx(audioManager.SpikeDropperClip);
        firstSpike = Instantiate(spike, firstSpikePos.position, Quaternion.identity);
        secondSpike = Instantiate(spike, secondSpikePos.position, Quaternion.identity);
        thirdSpike = Instantiate(spike, thirdSpikePos.position, Quaternion.identity);
        SetDropSpeed(firstSpike, dropSpeedMultiplier);
        SetDropSpeed(secondSpike, dropSpeedMultiplier);
        SetDropSpeed(thirdSpike, dropSpeedMultiplier);
    }
    
    private void SetDropSpeed(GameObject droppedSpike, float speedMultiplier)
    {
        var rb = droppedSpike.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale *= speedMultiplier;
        }
    }
}
