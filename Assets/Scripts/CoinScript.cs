using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.0025f; // The height of the float effect
    [SerializeField] private float floatFrequency = 2f;
    private Vector3 originalPosition;
    private void Start()
    {
        var animator = GetComponent<Animator>();
        var state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        originalPosition = transform.localPosition;
    }
    void Update()
    {
        // Calculate the floating offset
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // Apply the offset to the original position
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
    }
}
