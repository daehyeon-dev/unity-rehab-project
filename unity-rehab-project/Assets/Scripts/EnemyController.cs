using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 10;

    private Vector2 startPosition;
    private int direction = 1;
    private Rigidbody2D rb;
    private float lastAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        TryAttack();
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if(transform.position.x >= startPosition.x + moveRange)
        {
            direction = -1;
            Flip();
        }
        else if(transform.position.x < startPosition.x - moveRange)
        {
            direction = 1;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    private void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        Collider2D hitPlayer = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRange,
            playerLayer
        );

        if (hitPlayer == null)
            return;

        lastAttackTime = Time.time;
        
        Health targetHealth = hitPlayer.GetComponent<Health>();

        if(targetHealth != null)
        {
            targetHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 center;
        if (Application.isPlaying)
        {
            center = new Vector3(startPosition.x, transform.position.y, transform.position.z);
        }
        else
        {
            center = transform.position;
        }

        Vector3 left = center + Vector3.left * moveRange;
        Vector3 right = center + Vector3.right * moveRange;

        Gizmos.DrawLine(left, right);
        Gizmos.DrawSphere(left, 0.1f);
        Gizmos.DrawSphere(right, 0.1f);

        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
