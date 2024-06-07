using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingMace : MonoBehaviour
{
    [SerializeField] private float angle = 75f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float currentAngle;
    
    [SerializeField] private bool swingingRight = true;

    private Renderer _renderer;

    [SerializeField] private GameObject swingingClip;

    [SerializeField] private GameObject squareSprite;

    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = squareSprite.GetComponent<Renderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_renderer.isVisible && audioManager.SfxEnabled)
            swingingClip.SetActive(true);
        else
        {
            swingingClip.SetActive(false);
        }
        if(swingingRight)
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.forward), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            if (currentAngle >= angle && currentAngle < 180)
                swingingRight = false;
        }
        else
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.back), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
            if (currentAngle <= 360 - angle && currentAngle > 180)
                swingingRight = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }
    }
}
