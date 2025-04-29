using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update ()
    {
        var cameraPosition = player.transform.position + new Vector3(0, 1.75f, -1);
        transform.position = cameraPosition.y switch
        {
            >= -1f and <= 5f => cameraPosition,
            < -1f => new Vector3(cameraPosition.x, -1f, cameraPosition.z),
            > 5f => new Vector3(cameraPosition.x, 5f, cameraPosition.z),
            _ => transform.position
        };
    }
}
