using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovements : MonoBehaviour
{
   public float moveSpeed = 5f;

   private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            movement.y += 1;
        
        if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            movement.y -= 1;

        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movement.x += 1;

        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movement.x -= 1;

        movement = movement.normalized;

        rb.linearVelocity = movement * moveSpeed;

    }


}
