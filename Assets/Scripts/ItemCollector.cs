using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int _coins = 0;
    [SerializeField] private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Coin")) return;
        _coins++; // Increase the coin count
        audioManager.PlaySfx(audioManager.CoinCollectClip);
        Destroy(other.gameObject); // Remove the coin from the scene
        UpdateCoinUI(); // Optional: Update the UI to reflect the new coin count
    }

    private void UpdateCoinUI()
    {
        // Update your UI here, for example:
        Debug.Log("Coins: " + _coins);
    }
    // Getter for _coins
    public int GetCoins()
    {
        return _coins;
    }
}
