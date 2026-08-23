using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class playerMovements : MonoBehaviour
{
   public float moveSpeed = 6f;
   public float upMoveSpeed = 6f;

   private Rigidbody2D rb;

   private float originalGravity;
   [SerializeField] private float modifiedGravity = 0f;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger")
        {
            rb.gravityScale = modifiedGravity;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger")
        {
            rb.gravityScale = originalGravity;
        }
    }


    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed || Keyboard.current.spaceKey.isPressed)
            movement.y += 1;
        
        if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            movement.y -= 1;

        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movement.x += 1;

        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movement.x -= 1;

        movement = movement.normalized;

        float finalXSpeed = movement.x * moveSpeed;
        float finalYSpeed = movement.y > 0 ? movement.y * upMoveSpeed : movement.y * moveSpeed;

        rb.linearVelocity = new Vector2(finalXSpeed, finalYSpeed);

       

    }
    }