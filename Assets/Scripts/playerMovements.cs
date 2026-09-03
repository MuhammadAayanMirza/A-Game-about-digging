using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Collections;

public class playerMovements : MonoBehaviour
{
   public float moveSpeed = 6f;

    [Header("Jetpack Settings")]
   public float baseUpMoveSpeed = 6f;
   public float speedBonusPerLevel = 2f;
   public int jetpackLevel = 1;

   [Header ("Battery Settings")]
   public float maxBattery = 10f;
   public float batteryDrainRate = 1f;
   public int batteryLevel = 1;
   public float batteryBonusPerLevel = 5f;
   public float currentBattery;

   private Rigidbody2D rb;
   private float originalGravity;
   [SerializeField] private float jetpackGravity = 0f;
   [SerializeField] private float cobbleGravity = 0f;
   [SerializeField] private float triggerGravity = 20f;
   [SerializeField] private Animator _animator;

   [SerializeField] private GameObject DeathScreen;

   private bool isInJetpackZone = false;
   private bool isInCobbleZone = false;
   private bool isHoldingUpKey = false;
   private Vector2 inputMovement = Vector2.zero;
   private Vector3 startingPositon;

   private bool isDead = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;

        startingPositon = transform.position;

         currentBattery = GetMaxBattery();
    }

    public float GetMaxBattery()
    {
        return maxBattery + ((batteryLevel -1) * batteryBonusPerLevel);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger")
        {
            isInJetpackZone = true;
        }

        if (collision.gameObject.name == "Cobblestone")
        {
            isInCobbleZone = true;
            
        }

    }



      void Update()
    {
        inputMovement = Vector2.zero;


        isHoldingUpKey = Keyboard.current.wKey.isPressed || 
                              Keyboard.current.upArrowKey.isPressed || 
                              Keyboard.current.spaceKey.isPressed;
        
        if (isHoldingUpKey)
            inputMovement.y += 1;
        
        if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            inputMovement.y -= 1;

        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            inputMovement.x += 1;

        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            inputMovement.x -= 1;

        inputMovement = inputMovement.normalized;
    }

    void FixedUpdate()
    {
        
    
    float currentXVelocity = inputMovement.x * moveSpeed;
        float currentYVelocity = inputMovement.y * moveSpeed;

        bool jetpackIsActive = isInJetpackZone && isHoldingUpKey; 
        if (jetpackIsActive)
    
        {
            rb.gravityScale = jetpackGravity;

            float upgradedUpSpeed = baseUpMoveSpeed + ((jetpackLevel -1 ) * speedBonusPerLevel);

            currentYVelocity = upgradedUpSpeed;

            currentBattery -= batteryDrainRate * Time.fixedDeltaTime;

        if(currentBattery <= 0)
            {
                currentBattery = 0;

            if (!isDead)
            {
                isDead = true;
                Die();
            }
            return;
            }

        }
        else if (isInJetpackZone)
        {
            rb.gravityScale = triggerGravity;
        }

        else if (isInCobbleZone)
        {
            rb.gravityScale = cobbleGravity;
        }
            else
            {
                rb.gravityScale = originalGravity;
            }

        _animator.SetBool("Jetpack", jetpackIsActive);

        rb.linearVelocity = new Vector2(currentXVelocity, currentYVelocity);

        isInJetpackZone = false;
        isInCobbleZone = false;

       

        if (inputMovement.x != 0)
        {
            _animator.SetBool("Walking", true);

            float facingDirection = Mathf.Sign(inputMovement.x) * 3.66f ;
            transform.localScale = new Vector3(facingDirection, 3.66f, 1f);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
      }

      public void UpgradeJetpack()
    {
        if (jetpackLevel >= 4)
        {
            Debug.Log(" Jetpack Max ");
            return;
        }


        jetpackLevel++;
        Debug.Log("Jetpack Upgraded to Level: " + jetpackLevel);
    }

    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        Debug.Log("Jetpack battery peww, dieeee");

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        

        PlayerInventory inventory = GetComponent<PlayerInventory>();

        if(inventory != null)
        {
            inventory.coalCount = 0;

            if(GameManager.Instance != null)
            {
            GameManager.Instance.UpdateUI();
            }
        }

        _animator.SetBool("Jetpack", false);
        _animator.SetBool("Walking", false);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        DeathScreen.SetActive(true);

        yield return new WaitForSeconds(4f);

        rb.position = startingPositon;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;

        currentBattery = GetMaxBattery();

        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        DeathScreen.SetActive(false);

        isDead = false;

        Debug.Log("You are back! Don't Die Now Oloo");

    }
}