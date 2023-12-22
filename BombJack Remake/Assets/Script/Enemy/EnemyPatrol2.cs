using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol2 : MonoBehaviour
{
    [HideInInspector]
    public bool patrolMode;
    private bool mustFlip;

    public float mSpeed;
    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public LayerMask groundLayer;

    void Start()
    {
        patrolMode = true;
    }


    void Update()
    {
        if (patrolMode)
        {
            Patrol();
        }
    }
    private void FixedUpdate()
    {
        if (patrolMode)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustFlip)
        {
            Flip();
        }
        rb.velocity = new Vector2(mSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        patrolMode =  false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        mSpeed *= -1;
        patrolMode = true;
    }
}
