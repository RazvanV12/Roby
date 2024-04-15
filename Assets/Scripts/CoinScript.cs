using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.0025f; // The height of the float effect
    [SerializeField] private float floatFrequency = 2f;

    private void Start()
    {
        var animator = GetComponent<Animator>();
        var state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    private void Update()
    {
        // Mathf.Sin operates in radians. Time.time will give a full cycle every 2 * PI seconds,
        // so multiplying Time.time by floatFrequency will let you control the speed.
        var y = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // This adds the calculated offset to the original position.
        // The original Y position is stored in localPosition.y when the game starts.
        var transform1 = transform;
        var localPosition = transform1.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y + y, localPosition.z);
        transform1.localPosition = localPosition;
    }
}
