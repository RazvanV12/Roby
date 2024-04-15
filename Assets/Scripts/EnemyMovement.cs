using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Vector2 _currentTarget;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _animator = GetComponent<Animator>();
        _currentTarget = pointA.position;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveToTarget();

        // Check if we've reached the current target
        if (Vector2.Distance(transform.position, _currentTarget) < 0.1f)
        {
            // Switch target
            _currentTarget = _currentTarget == (Vector2)pointA.position ? pointB.position : pointA.position;
        }
        _animator.SetFloat(XVelocity, _rb.velocity.x);
    }
    
    private void MoveToTarget()
    {
        var direction = ((Vector2)_currentTarget - (Vector2)transform.position).normalized;
        _rb.velocity = direction * speed;
    }
}
