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
        // currentAngle = transform.eulerAngles.z;
        // if(_renderer.isVisible && audioManager.SfxEnabled && (Mathf.Approximately(currentAngle, 60) || Mathf.Approximately(currentAngle, 300)))
        //     //swingingClip.SetActive(true);
        //     audioManager.PlaySfx(audioManager.SwingingMaceClip);
        // else
        // {
        //     swingingClip.SetActive(false);
        // }
        if(swingingRight)
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.forward), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            if (currentAngle >= angle && currentAngle < 180)
            {
                swingingRight = false;
                if(_renderer.isVisible && audioManager.SfxEnabled)
                    audioManager.PlaySfx(audioManager.SwingingMaceClip);
            }
        }
        else
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.back), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
            if (currentAngle <= 360 - angle && currentAngle > 180)
            {
                swingingRight = true;
                if(_renderer.isVisible && audioManager.SfxEnabled)
                    audioManager.PlaySfx(audioManager.SwingingMaceClip);
            }
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
