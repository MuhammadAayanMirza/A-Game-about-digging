using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Runtime.InteropServices;
public class Digging : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap coalTilemap;
    public float digRange = 2.5f;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    private PlayerInventory playerInventory;

    [Header ("Shovel Toggle") ]
    public GameObject ShovelItem;

    [Header ("Upgrade Settings")]
    [Tooltip("1 = 1 tile, 2 = 2 tiles, 3 = 3 tiles, 4 = 4 tiles")]
    [Range(1,4)] public int digSizeLevel = 1;
    public bool IsDigging { get; private set; }

    private Vector3 startingPos;

    void Start()
    {
        startingPos = transform.localScale;

        playerInventory = GetComponent<PlayerInventory>();
    }

    void FaceDirection(Vector3 tileCenter)
    {
        bool blockIsLeft = tileCenter.x < transform.position.x;

        float x = Mathf.Abs(startingPos.x);

        if(blockIsLeft)
            x *= -1;

        transform.localScale = new Vector3(
            x,
            startingPos.y,
            startingPos.z
        );
    }
    void Update()
    {
        if (IsDigging) return;

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Dig();
        }
    }

    bool IsValidTile(Vector3Int cell)
    {
        bool hasGround = (groundTilemap != null && groundTilemap.HasTile(cell));
        bool hasCoal = (coalTilemap != null && coalTilemap.HasTile(cell));
        return hasGround || hasCoal;

    }

    void Dig()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

    mouseWorldPosition.z = 0;

    Vector3Int cellPosition = groundTilemap.WorldToCell(mouseWorldPosition);
    Vector3 tileCenter = groundTilemap.GetCellCenterWorld(cellPosition);
    float distance = Vector2.Distance(transform.position, tileCenter);

    if (distance <= digRange && IsValidTile(cellPosition))
        {

            IsDigging = true;

            FaceDirection(tileCenter);


            if(ShovelItem != null)
             ShovelItem.SetActive(false);


            if (animator != null)
             animator.SetBool("IsDigging", true);

            StartCoroutine(ResetDigState(cellPosition));

                
            }
        }

    private System.Collections.IEnumerator ResetDigState(Vector3Int cellPosition)
    {
        IsDigging = true;
        
        yield return new WaitForSeconds(0.8f);

        ExecuteAreaDig(cellPosition);

        IsDigging = false;

        if (animator != null)
        animator.SetBool("IsDigging", false);

        if (ShovelItem != null) 
            ShovelItem.SetActive(true);
    }

    private void ExecuteAreaDig(Vector3Int centerCell)
    {
        if (groundTilemap == null)return;

        if (digSizeLevel == 1)
        {
           BreakSingleTile(centerCell);
           if (GameManager.Instance != null) GameManager.Instance.UpdateUI();
           return;
        }

        List<Vector3Int> validNearbyCells = new List<Vector3Int>();

        for (int x = -2; x <= 2; x++)
        {
            for(int y = -2; y <= 2; y++)
            {
                Vector3Int scanCell = new Vector3Int(centerCell.x + x, centerCell.y + y, centerCell.z);
                if (IsValidTile(scanCell))
                {
                    validNearbyCells.Add(scanCell);
                    
                }
            }
        }
            validNearbyCells.Sort((a,b) =>
            Vector3Int.Distance(a, centerCell).CompareTo(Vector3Int.Distance(b,centerCell))
            );

            int tilesToBreakCount = Mathf.Min(digSizeLevel, validNearbyCells.Count);

            for(int i = 0; i < tilesToBreakCount; i++)
        {
            BreakSingleTile(validNearbyCells[i]);
        }

        if (GameManager.Instance != null) GameManager.Instance.UpdateUI();
     }

    private void BreakSingleTile(Vector3Int cell)
    {
        if (coalTilemap != null && coalTilemap.HasTile(cell))
        {
            coalTilemap.SetTile(cell, null);
            if (playerInventory != null) playerInventory.AddCoal(1);
        }
        if (groundTilemap != null && groundTilemap.HasTile(cell))
        {
            groundTilemap.SetTile(cell, null);
        }
    }

}
