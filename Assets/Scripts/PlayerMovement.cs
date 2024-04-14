using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private Rigidbody2D rb; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = Input.GetAxis("Horizontal");
        transform.position = new Vector2(transform.position.x + moveInput * speed * Time.deltaTime, transform.position.y);
        
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        // Make the player be able to duck by pressing S key - when you duck, you can't move and the height
        // of the player is reduced to half
        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            speed = 0f;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            speed = 5f;
        }
    }
}
