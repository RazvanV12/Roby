using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGroundTrap : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbFirstTile;
    [SerializeField] Rigidbody2D rbSecondTile;

    [SerializeField] private float distance = 1f;
    [SerializeField] private Transform RaycastOrigin;
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
            rbFirstTile.bodyType = RigidbodyType2D.Dynamic;
            rbSecondTile.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RaycastOrigin.position, RaycastOrigin.position + transform.up * distance);
    }
}
