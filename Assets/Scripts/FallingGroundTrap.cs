using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGroundTrap : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbFirstTile;
    [SerializeField] Rigidbody2D rbSecondTile;

    [SerializeField] private float distance = 1f;
    [SerializeField] private Transform RaycastOrigin;
    [SerializeField] private AudioManager audioManager;
    private bool clipIsplayed = false;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rbFirstTile.bodyType = RigidbodyType2D.Static;
        rbSecondTile.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(RaycastOrigin.position, transform.up, distance);
        if (hit.collider is not null && hit.collider.CompareTag("Player"))
        {
            rbFirstTile.bodyType = RigidbodyType2D.Kinematic;
            rbSecondTile.bodyType = RigidbodyType2D.Kinematic;
            rbFirstTile.velocity = new Vector2(0, -7);
            rbSecondTile.velocity = new Vector2(0, -7);
            if (!clipIsplayed)
            {
                audioManager.PlaySfx(audioManager.FallingGroundClip);
                clipIsplayed = true;
            }

            Destroy(this, 3f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RaycastOrigin.position, RaycastOrigin.position + transform.up * distance);
    }
}
