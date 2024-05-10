using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float lifeTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot the bullet in the X axis with the speed of 5 and a lifetime of 2 seconds
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
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
