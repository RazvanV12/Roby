using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float normalDrag;
    [SerializeField] private float lowDrag = 0.1f;
    [SerializeField] private float normalAngularDrag = 0.05f;
    [SerializeField] private float lowAngularDrag = 0.01f;
    [SerializeField] private float moveForce = 100f;
    private float _horizontalInput;
    
    [SerializeField, ReadOnly] private bool _isGrounded = true;
    private bool _isJumping;
    [SerializeField] private bool isOnIce;
     
    private Rigidbody2D _rb;
    
    private Animator _animator;
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.S) && _isGrounded)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            //moveForce = 0f;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            //moveForce = 0.5f;
        }

        if (!_isJumping) _isJumping = Input.GetKeyDown(KeyCode.W);
        if (_isJumping && Input.GetKeyUp(KeyCode.W)) _isJumping = false;
    }

    private void FixedUpdate()
    {
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
        if (_isJumping && _isGrounded)
        {
            _isJumping = false;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _isGrounded = false;
            _animator.SetBool(IsJumping, !_isGrounded);
        }
        _animator.SetFloat(XVelocity, Math.Abs(_rb.velocity.x));
        _animator.SetFloat(YVelocity, Math.Abs(_rb.velocity.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Floor") || other.CompareTag("Ice Ground"))
        {
            _isGrounded = true;
            _animator.SetBool(IsJumping, !_isGrounded);
        }
        if (other.CompareTag("Ice Ground"))
        {
            isOnIce = true;
            _rb.drag = lowDrag;
            _rb.angularDrag = lowAngularDrag;
        }

        if (other.CompareTag("Floor"))
        {
            isOnIce = false;
            _rb.drag = normalDrag;
            _rb.angularDrag = normalAngularDrag;
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Ice Ground"))
    //     {
    //         isOnIce = false;
    //         _rb.drag = normalDrag;
    //         _rb.angularDrag = normalAngularDrag;
    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ice Ground"))
    //     {
    //         isOnIce = true;
    //         _rb.drag = lowDrag;
    //         _rb.angularDrag = lowAngularDrag;
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ice Ground"))
    //     {
    //         isOnIce = false;
    //         _rb.drag = normalDrag;
    //         _rb.angularDrag = normalAngularDrag;
    //     }
    // }

}
