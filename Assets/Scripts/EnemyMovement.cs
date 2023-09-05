using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private float moveSpeed;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(body.velocity.x)), 1f);
    }
}
