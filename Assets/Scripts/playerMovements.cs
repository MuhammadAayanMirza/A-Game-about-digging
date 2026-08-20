using UnityEngine;

public class playerMovements : MonoBehaviour
{
   public float moveSpeed = 5f;

   private Rigidbody2D rb;
   private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }
}
