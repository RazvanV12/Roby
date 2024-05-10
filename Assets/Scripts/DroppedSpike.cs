using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedSpike : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    private bool hasHitGround;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHitGround)
        {
            // Start a timer to destroy the spike after 2 seconds
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasHitGround)
        {
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            hasHitGround = true;
            rb.gravityScale = 0;
            polygonCollider2D.enabled = false;
        }
    }
}
