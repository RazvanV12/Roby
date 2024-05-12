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
    [SerializeField] private float distance = 0.5f;

    [SerializeField] private Vector2 boxCastSize = new Vector2(1f, 13f);
    
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
            // The distance determines how far the box is cast ( making it 1f, the entire boxCast will be cast downwards)
            // If we want the cast to be as the drawing, we need to set the distance to 0.5f
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, distance, playerLayer);
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
        Vector3 castPoint = transform.position + Vector3.down * (0.5f); // Adjust this based on your BoxCast settings
        Vector3 castSize = new Vector3(boxCastSize.x, boxCastSize.y, 1);
        Gizmos.DrawWireCube(castPoint, castSize);
    }

}
