using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update ()
    {
        transform.position = player.transform.position + new Vector3(0, 1.75f, -1);
    }
}
