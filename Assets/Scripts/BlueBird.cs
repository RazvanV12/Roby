using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.3f;
    [SerializeField] private float speed = 1.4f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float distance = 10f;

    [SerializeField] private Transform rayCastPosition;

    [SerializeField] private GameObject flappingWings;

    private Renderer _renderer;
    
    private bool isFlapping = false;
    //private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(new Vector2(rayCastPosition.position.x, rayCastPosition.position.y), transform.up, distance);
        if (hit.collider is not null && hit.collider.CompareTag("Player") && !isFlapping)
        {
            StartCoroutine(StartFlapping());
            isFlapping = true;
        }

        if (_renderer.isVisible)
        {
            if (isFlapping)
                flappingWings.SetActive(true);
            else
            {
                flappingWings.SetActive(false);
            }
        }
        else
        {
            flappingWings.SetActive(false);
        }
    }
    
    private IEnumerator StartFlapping()
    {
        var startTime = Time.time;
        while (true)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, Mathf.Sin(Time.time) * amplitude * Time.deltaTime, 0);
            if(Time.time - startTime > lifeTime)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayCastPosition.position, rayCastPosition.position + transform.up * distance);
    }
    // private void FlapWings()
    // {
    //     // Apply both vertical and horizontal force
    //     var force = new Vector2(horizontalForce, flapForce);
    //     rb.AddForce(force, ForceMode2D.Impulse);
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteractions.OnPlayerDeath?.Invoke();
        }
    }
}
