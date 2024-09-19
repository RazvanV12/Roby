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
    [SerializeField] private int maxJumpCount = 1; 
    
    [SerializeField, ReadOnly] private bool _isGrounded = true;
    [SerializeField] private bool isDucked = false;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool isOnIce;
    [SerializeField] private bool invertedControls;
    [SerializeField] private bool invertedVerticals;

    private Animator _animator;
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private int numberOfCollisionsWithIce;
    [SerializeField] private int numberOfCollisionsWithFloor;

    [SerializeField] private GameObject footsteps;
    
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (gameManager.IsPaused() == false)
        {
            _verticalInput = Input.GetAxisRaw("Vertical");
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            if (_horizontalInput != 0 && _isGrounded && audioManager.SfxEnabled)
            {
                footsteps.SetActive(true);
            }
            else
            {
                footsteps.SetActive(false);
            }

            if (invertedControls)
            {
                _verticalInput *= -1;
                _horizontalInput *= -1;
            }

            if (Mathf.Approximately(_verticalInput, -1) && _isGrounded) //&& !isDucked)
            {
                transform.localScale = new Vector3(1, 0.5f, 1);
                moveForce = 0f;
                //isDucked = true;
            }
            else
            {
                transform.localScale = new Vector3(1, 1f, 1);
                moveForce = 100f;
                //isDucked = false;
            }

            if (numberOfCollisionsWithFloor == 0)
            {
                jumpCount++;
            }
            // Check for jump input and increment jump count
            if ((invertedControls ? Input.GetKeyDown(KeyCode.S) : Input.GetKeyDown(KeyCode.W)) &&
                jumpCount < maxJumpCount)
            {
                _isJumping = true;
                jumpCount++;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (gameManager.IsPaused() == false)
        {
            if (!_isGrounded && transform.position.y < -1.5f)
            {
                moveForce = 0f;
            }

            if (!_isGrounded && transform.position.y > 11)
            {
                moveForce = 0f;
                PlayerInteractions.OnPlayerDeath?.Invoke();
            }

            //_rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
            if (Mathf.Abs(_horizontalInput) > 0)
            {
                Rigidbody.AddForce(new Vector2(_horizontalInput * moveForce, 0), ForceMode2D.Force);
            }
            else if (!isOnIce)
            {
                // Stop the player if not on ice and no input is detected
                Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            }

            if (Mathf.Abs(Rigidbody.velocity.x) > maxSpeed)
            {
                Rigidbody.velocity = new Vector2(Mathf.Sign(Rigidbody.velocity.x) * maxSpeed, Rigidbody.velocity.y);
            }

            if (_isJumping)
            {
                if (invertedVerticals)
                    Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, -jumpForce);
                else
                {
                    Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
                }

                audioManager.PlaySfx(audioManager.JumpClip);
                _isJumping = false;
                _isGrounded = false;
                _animator.SetBool(IsJumping, true);
            }

            _animator.SetFloat(XVelocity, Math.Abs(Rigidbody.velocity.x));
            _animator.SetFloat(YVelocity, Math.Abs(Rigidbody.velocity.y));
        }
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
            Rigidbody.drag = lowDrag;
            Rigidbody.angularDrag = lowAngularDrag;
        }
        if((other.CompareTag("Floor") || other.CompareTag("Ice Ground") || other.CompareTag("Cloud")) && numberOfCollisionsWithFloor == 1)
        {
            audioManager.PlaySfx(audioManager.LandingClip);
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
                Rigidbody.drag = normalDrag;
                Rigidbody.angularDrag = normalAngularDrag;
            }
        }

        if (other.CompareTag("Floor") || other.CompareTag("Cloud")) //&& !Mathf.Approximately(_verticalInput, -1))
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
    public Rigidbody2D Rigidbody { get; set; }

    public bool InvertedVerticals
    {
        get => invertedVerticals;
        set => invertedVerticals = value;
    }
}
