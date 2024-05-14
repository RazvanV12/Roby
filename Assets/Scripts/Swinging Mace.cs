using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingMace : MonoBehaviour
{
    [SerializeField] private float angle = 45f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float currentAngle;
    
    [SerializeField] private bool swingingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(swingingRight)
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.forward), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            if (currentAngle >= angle && currentAngle < 180)
                swingingRight = false;
        }
        else
        {
            //var currentAngle = Quaternion.Angle(Quaternion.Euler(Vector3.back), transform.rotation);
            currentAngle = transform.eulerAngles.z;
            transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
            if (currentAngle <= 360 - angle && currentAngle > 180)
                swingingRight = true;
        }
    }
}
