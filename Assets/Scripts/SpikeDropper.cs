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
        Instantiate(spike, firstSpikePos.position, Quaternion.identity);
        Instantiate(spike, secondSpikePos.position, Quaternion.identity);
        Instantiate(spike, thirdSpikePos.position, Quaternion.identity);
    }
}
