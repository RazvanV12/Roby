using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private bool _isGrounded = true;
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private float _horizontalInput;
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
            speed = 0f;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            speed = 5f;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _isGrounded = false;
            _animator.SetBool(IsJumping, !_isGrounded);
        }
        _rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
        _animator.SetFloat(XVelocity, Math.Abs(_rb.velocity.x));
        _animator.SetFloat(YVelocity, Math.Abs(_rb.velocity.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
        _animator.SetBool(IsJumping, !_isGrounded);
    }
}
