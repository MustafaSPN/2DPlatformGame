using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    private float bulletSpeed;
    private float xSpeed;
    private PlayerMovement player;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bulletSpeed = 20f;
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(xSpeed * bulletSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
