using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _coins = 0; // To keep track of the number of coins collected

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Coin")) return;
        _coins++; // Increase the coin count
        Destroy(other.gameObject); // Remove the coin from the scene
        UpdateCoinUI(); // Optional: Update the UI to reflect the new coin count
    }

    private void UpdateCoinUI()
    {
        // Update your UI here, for example:
        Debug.Log("Coins: " + _coins);
    }
}
