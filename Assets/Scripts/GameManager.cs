using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeSinceGameStarted;
    [SerializeField] private float coinsCollected;
    [SerializeField] private ItemCollector itemCollector;

    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI uiCoins;

    [SerializeField] private GameObject DiedMenuUI;
    [SerializeField] private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceGameStarted += Time.deltaTime;
        uiTimer.text = "Timer: " + timeSinceGameStarted.ToString("F2");
        coinsCollected = itemCollector.GetCoins();
        uiCoins.text = "Coins: " + coinsCollected;
        if(playerPosition.position.y < -3f)
        {
            DiedMenuUI.SetActive(true);
        }
    }
}
