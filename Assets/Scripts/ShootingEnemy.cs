using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform bulletPos;
    private float timeBtwShots;
    private Renderer _renderer;
    
    [SerializeField] private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBtwShots += Time.deltaTime;
        if (timeBtwShots > 1.5f)
        {
            timeBtwShots = 0;
            shoot();
        }
    }

    void shoot()
    {
        if(_renderer.isVisible)
            audioManager.PlaySfx(audioManager.ShootingBulletClip);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
