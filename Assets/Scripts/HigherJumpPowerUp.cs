using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherJumpPowerUp : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private float boostAmount = 5f;
    [SerializeField] private float duration = 5f;
    // Start is called before the first frame update
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
            ApplyJumpBoost();
        }
    }
    
    private void ApplyJumpBoost()
    {
        if (playerMovement != null)
        {
            playerMovement.JumpForce += boostAmount;
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(RemoveJumpBoost());
        }
    }

    private IEnumerator RemoveJumpBoost()
    {
        yield return new WaitForSeconds(duration);
        playerMovement.JumpForce -= boostAmount;
        Destroy(gameObject);
    }
}
