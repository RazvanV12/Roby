using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Vector2 _currentTarget;
    private bool _isMoving = true;
    [SerializeField] private float waitingTime;
    
    [SerializeField] private PlayerInteractions playerInteractions;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
        _currentTarget = pointB.position;
        StartCoroutine(MoveBetweenPoints());
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetFloat(XVelocity, _rb.velocity.x);
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true) // Loop to move continuously
        {
            while (_isMoving)
            {
                MoveToTarget();
                // Check if we've reached the current target
                if (Vector2.Distance(transform.position, _currentTarget) < 0.1f)
                {
                    // Switch target
                    _currentTarget = _currentTarget == (Vector2)pointA.position ? pointB.position : pointA.position;
                    _isMoving = false;
                }
                yield return null;
            }

            _rb.velocity = Vector2.zero; // Stop the enemy's movement
            yield return new WaitForSeconds(waitingTime); // Wait for 5 seconds
            _isMoving = true; // Resume moving after the pause
        }
    }

    private void MoveToTarget()
    {
        var direction = ((Vector2)_currentTarget - (Vector2)transform.position).normalized;
        _rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }
    }
}
