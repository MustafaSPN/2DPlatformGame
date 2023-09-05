using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 deathKick = new Vector2(10f, 10f);
    private bool isAlive;

    private Rigidbody2D body;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

    private float jumpSpeed = 20f;
    private float moveSpeed = 7f;
    private float climbSpeed = 5f;
    private float gravityScaleAtStart;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gun;
    [SerializeField] private Tilemap background;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = body.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        Death();
        if (isAlive)
        {
         
            Run();
            FlipSprite();
            ClimbLadder();   
        }
    }

   void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }

   void Run()
   {
       Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, body.velocity.y);
       body.velocity = playerVelocity ;
       bool playerHasHorizontalSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;

       animator.SetBool("isRunning",playerHasHorizontalSpeed);
   }

   void FlipSprite()
   {
       bool playerHasHorizontalSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
       if (playerHasHorizontalSpeed)
       {
           transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);    
       }

       
   }

   void OnJump(InputValue value)
   {
       if (isAlive)
       {
           if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
           {
               return;
           }

           if (value.isPressed)
           {
               body.velocity += new Vector2(0f, jumpSpeed);
           }
       }
   }

   void ClimbLadder()
   {

       if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
       {
           body.gravityScale = gravityScaleAtStart;

           animator.SetBool("isClimbing" ,false); 
           return;
       }
       bool playerHasVerticalSpeed = Mathf.Abs(body.velocity.y) > Mathf.Epsilon;
       animator.SetBool("isClimbing" ,playerHasVerticalSpeed);
       Vector2 climbVelocity = new Vector2(body.velocity.x, moveInput.y * climbSpeed);
       body.velocity = climbVelocity;
       body.gravityScale = 0;
       
   }

   void OnFire(InputValue value)
   {
       if (!isAlive) { return; }
       GameObject bullet;
       bullet = Instantiate(bulletPrefab);
       bullet.transform.position = gun.transform.position;
   }

   void Death()
   {
       if (body.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) && isAlive)
       {
           isAlive = false;
           animator.SetTrigger("Dying");
           animator.SetBool("isAlive",false);
           body.velocity = deathKick;
           background.color = Color.black;
           
           FindObjectOfType<GameSession>().ProcessPlayerDeath();


       }
   }
}
