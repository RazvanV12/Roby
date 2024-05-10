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

    [SerializeField] private Vector2 boxCastSize = new Vector2(1f, 10f);
    
    [SerializeField] private LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextDropTime)
        {
            // Casts a box against colliders in the scene, returning all colliders that contact or overlap the box
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCastSize, 0, Vector2.down, 1f, playerLayer);
            if (hit.collider is not null && hit.collider.CompareTag("Player"))
            {
                DropSpikes();
                nextDropTime = Time.time + dropCooldown;
            }
        }
    }

    private void DropSpikes()
    {
        Instantiate(spike, firstSpikePos.position, Quaternion.identity);
        Instantiate(spike, secondSpikePos.position, Quaternion.identity);
        Instantiate(spike, thirdSpikePos.position, Quaternion.identity);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draws a wire cube with the given position, rotation, and scale
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(boxCastSize.x, boxCastSize.y, 1));
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 1, 1));
    }
}
