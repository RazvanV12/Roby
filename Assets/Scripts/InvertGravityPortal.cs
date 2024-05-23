using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravityPortal : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private bool playerIsUpsideDown;

    [SerializeField] private CapsuleCollider2D capsuleCollider2d;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerTransform = playerMovement.GetComponentInParent<Transform>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerIsUpsideDown)
            {
                playerMovement.Rigidbody.gravityScale *= -1;
                playerMovement.InvertedVerticals = true;
                playerTransform.eulerAngles = new Vector3(180f, 0, 0);
                capsuleCollider2d.enabled = false;
            }
            else
            {
                playerMovement.Rigidbody.gravityScale *= -1;
                playerMovement.InvertedVerticals = false;
                playerTransform.eulerAngles = new Vector3(0, 0, 0);
                capsuleCollider2d.enabled = false;
            }
        }
    }
}
