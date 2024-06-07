using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedControls : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private float duration = 5f;
    // Start is called before the first frame update
    
    [SerializeField] private AudioManager audioManager;
    
    private bool isCollected;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found!");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            InvertControls();
            isCollected = true;
            audioManager.PlaySfx(audioManager.InvertedControlsClip);
        }
    }
    
    private void InvertControls()
    {
        if (playerMovement != null)
        {
            playerMovement.InvertedControls = true;
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(RestoreNormalControls());
        }
    }

    private IEnumerator RestoreNormalControls()
    {
        yield return new WaitForSeconds(duration);
        playerMovement.InvertedControls = false;
        Destroy(gameObject);
    }
}
