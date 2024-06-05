using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.0025f; // The height of the float effect
    [SerializeField] private float floatFrequency = 2f;
    private Vector3 originalPosition;
    public bool isCollected = false;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        var animator = GetComponent<Animator>();
        var state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        originalPosition = transform.localPosition; 
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        // Calculate the floating offset
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // Apply the offset to the original position
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isCollected == false){
            isCollected = true;
            gameManager.AddCollectedCoin();
            audioManager.PlaySfx(audioManager.CoinCollectClip);
            Destroy(this.gameObject);
        }
    }
    
}
