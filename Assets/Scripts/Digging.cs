using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Digging : MonoBehaviour
{
    public Tilemap groundTilemap;
    public float digRange = 2.5f;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    [Header ("Shovel Toggle") ]
    public GameObject ShovelItem;

    [Header ("Upgrade Settings")]
    [Tooltip("1 = 1x1, 2 = 2x2, 3 = 3x3, 4 = 4x4 cube")]
    [Range(1,4)] public int digSizeLevel = 1;
    public bool IsDigging { get; private set; }

    private Vector3 startingPos;

    void Start()
    {
        startingPos = transform.localScale;
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

    void Dig()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

    mouseWorldPosition.z = 0;

    Vector3Int cellPosition = groundTilemap.WorldToCell(mouseWorldPosition);
    Vector3 tileCenter = groundTilemap.GetCellCenterWorld(cellPosition);
    float distance = Vector2.Distance(transform.position, tileCenter);

    if (distance <= digRange && groundTilemap.HasTile(cellPosition))
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
            groundTilemap.SetTile(centerCell, null);
            return;
        }

        int minOffset = 0;
        int maxOffset = 0;

        switch (digSizeLevel)
        {
        case 2:

            minOffset = 0;
            maxOffset = 1;
            break;
        
        case 3:

            minOffset = -1;
            maxOffset = 1;
            break;

        case 4:

            minOffset = -1;
            maxOffset = 2;
            break;
        }
    
    for (int x = minOffset; x <= maxOffset; x++)
        {
          for (int y = minOffset; y <= maxOffset; y++)
            {
                Vector3Int currentCell = new Vector3Int(centerCell.x + x, centerCell.y + y, centerCell.z);

                if (groundTilemap.HasTile(currentCell))
                {
                    groundTilemap.SetTile(currentCell, null);
                }
            } 
        }
    }

    public void UpgradeDigSize()
    {
        digSizeLevel++;

        if (digSizeLevel > 4)
        {
            digSizeLevel = 1;
        }

        Debug.Log("Shovel Power Upgraded! Current Size Level: " + digSizeLevel);
    }
}
