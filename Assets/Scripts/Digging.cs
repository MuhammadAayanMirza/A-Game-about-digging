using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Digging : MonoBehaviour
{
    public Tilemap groundTilemap;
    public float digRange = 2.5f;

    void Update()
    {
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

    if (distance <= digRange)
        {
            if(groundTilemap.HasTile(cellPosition))
            {
                groundTilemap.SetTile(cellPosition, null);
            }
        }
    }
}
