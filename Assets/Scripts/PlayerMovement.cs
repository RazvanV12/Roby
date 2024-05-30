using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float normalDrag;
    [SerializeField] private float lowDrag = 0.1f;
    [SerializeField] private float normalAngularDrag = 0.05f;
    [SerializeField] private float lowAngularDrag = 0.01f;
    [SerializeField] private float moveForce = 100f;
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int maxJumpCount = 1; // Allow for double jump when power-up is active
    
    [SerializeField, ReadOnly] private bool _isGrounded = true;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool isOnIce;
    [SerializeField] private bool invertedControls;
    [SerializeField] private bool invertedVerticals;
    // To do: Logic for when the player is upside-down; 
     
    private Rigidbody2D _rb;
    
    private Animator _animator;
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private int numberOfCollisionsWithIce;
    private int numberOfCollisionsWithFloor;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
    }

    // // Update is called once per frame
    // private void Update()
    // {
    //     _verticalInput = Input.GetAxisRaw("Vertical");
    //     _horizontalInput = Input.GetAxisRaw("Horizontal");
    //     if (Input.GetKey(KeyCode.S) && _isGrounded)
    //     {
    //         transform.localScale = new Vector3(1, 0.5f, 1);
    //         moveForce = 0f;
    //     }
    //     else
    //     {
    //         transform.localScale = new Vector3(1, 1f, 1);
    //         moveForce = 100f;
    //     }
    //
    //     if (!_isJumping) _isJumping = Input.GetKeyDown(KeyCode.W);
    //     if (_isJumping && Input.GetKeyUp(KeyCode.W)) _isJumping = false;
    // }
    //
    // private void FixedUpdate()
    // {
    //     if(!_isGrounded && transform.position.y < -1.5f)
    //     {
    //         moveForce = 0f;
    //     }
    //     //_rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
    //     if (Mathf.Abs(_horizontalInput) > 0)
    //     {
    //         _rb.AddForce(new Vector2(_horizontalInput * moveForce, 0), ForceMode2D.Force);
    //     }
    //     else if (!isOnIce)
    //     {
    //         // Stop the player if not on ice and no input is detected
    //         _rb.velocity = new Vector2(0, _rb.velocity.y);
    //     }
    //     if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
    //     {
    //         _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxSpeed, _rb.velocity.y);
    //     }
    //     if (_isJumping && _isGrounded)
    //     {
    //         _isJumping = false;
    //         _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    //         _isGrounded = false;
    //         _animator.SetBool(IsJumping, !_isGrounded);
    //     }
    //     _animator.SetFloat(XVelocity, Math.Abs(_rb.velocity.x));
    //     _animator.SetFloat(YVelocity, Math.Abs(_rb.velocity.y));
    // }
    
    // Update is called once per frame
    private void Update()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (invertedControls)
        {
            _verticalInput *= -1;
            _horizontalInput *= -1;
        }
        if (Mathf.Approximately(_verticalInput, -1) && _isGrounded)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            moveForce = 0f;
        }
        else
        {
            transform.localScale = new Vector3(1, 1f, 1);
            moveForce = 100f;
        }

        // Check for jump input and increment jump count
        if ((invertedControls ? Input.GetKeyDown(KeyCode.S) : Input.GetKeyDown(KeyCode.W)) && jumpCount < maxJumpCount)
        {
            _isJumping = true;
            jumpCount++;
        }
    }
    
    private void FixedUpdate()
    {
        if(!_isGrounded && transform.position.y < -1.5f)
        {
            moveForce = 0f;
        }
        if(!_isGrounded && transform.position.y > 11)
        {
            moveForce = 0f;
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }
        //_rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
        if (Mathf.Abs(_horizontalInput) > 0)
        {
            _rb.AddForce(new Vector2(_horizontalInput * moveForce, 0), ForceMode2D.Force);
        }
        else if (!isOnIce)
        {
            // Stop the player if not on ice and no input is detected
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxSpeed, _rb.velocity.y);
        }
        if (_isJumping)
        {
            if(invertedVerticals)
                _rb.velocity = new Vector2(_rb.velocity.x, -jumpForce);
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            }
            _isJumping = false;
            _isGrounded = false;
            _animator.SetBool(IsJumping, true);
        }
        _animator.SetFloat(XVelocity, Math.Abs(_rb.velocity.x));
        _animator.SetFloat(YVelocity, Math.Abs(_rb.velocity.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Floor") || other.CompareTag("Ice Ground") || other.CompareTag("Cloud"))
        {
            numberOfCollisionsWithFloor++;
            _isGrounded = true;
            jumpCount = 0; // Reset jump count when grounded
            _animator.SetBool(IsJumping, false);
        }
        if (other.CompareTag("Ice Ground"))
        {
            numberOfCollisionsWithIce++;
            isOnIce = true;
            _rb.drag = lowDrag;
            _rb.angularDrag = lowAngularDrag;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ice Ground") && !Mathf.Approximately(_verticalInput, -1))
        {
            numberOfCollisionsWithIce--;
            if (numberOfCollisionsWithIce == 0)
            {
                isOnIce = false;
                _rb.drag = normalDrag;
                _rb.angularDrag = normalAngularDrag;
            }
        }

        if ((other.CompareTag("Floor") || other.CompareTag("Cloud")) && !Mathf.Approximately(_verticalInput, -1))
        {
            numberOfCollisionsWithFloor--;
            if (numberOfCollisionsWithFloor == 0)
            {
                _isGrounded = false;
                _animator.SetBool(IsJumping, true);
            }
        }
    }
    
    // Getter setter for maxSpeed   
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }
    
    // getter setter for jump force
    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = value;
    }
    
    // getter setter for inverted controls
    public bool InvertedControls
    {
        get => invertedControls;
        set => invertedControls = value;
    }

    // getter setter for maxJumpCount
    public int MaxJumpCount
    {
        get => maxJumpCount;
        set => maxJumpCount = value;
    }
    
    //getter setter for rb
    public Rigidbody2D Rigidbody
    {
        get => _rb;
        set => _rb = value;
    }

    public bool InvertedVerticals
    {
        get => invertedVerticals;
        set => invertedVerticals = value;
    }
}
