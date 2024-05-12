using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeSinceGameStarted;

    [SerializeField] private TextMeshProUGUI uiTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a timer to keep track of the time since the game started
        // Show said timer on the screen
        timeSinceGameStarted += Time.deltaTime;
        uiTimer.text = "Timer: " + timeSinceGameStarted.ToString("F2");
    }
}
