using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private float duration = 5f;
    // Start is called before the first frame update
    
    [SerializeField] private AudioManager audioManager;
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
        if (other.CompareTag("Player"))
        {
            EnableDoubleJump();
            audioManager.PlaySfx(audioManager.DoubleJumpClip);
        }
    }
    
    private void EnableDoubleJump()
    {
        if (playerMovement != null)
        {
            playerMovement.MaxJumpCount = 2;
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DisableDoubleJump());
        }
    }

    private IEnumerator DisableDoubleJump()
    {
        yield return new WaitForSeconds(duration);
        playerMovement.MaxJumpCount = 1;
        Destroy(gameObject);
    }
}
