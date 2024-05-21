using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingSaw : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float distance = 6f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(transform.position, -transform.right, distance);
        if (hit.collider is not null && hit.collider.CompareTag("Player"))
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }
    }
}
