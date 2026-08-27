using UnityEngine;
using UnityEngine.Tilemaps;

public class ShovelShow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ShovelItem;    
    [SerializeField] private Tilemap Ground;

    private void Start()
    {
        if (ShovelItem != null) ShovelItem.SetActive(false);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (ShovelItem != null) ShovelItem.SetActive(true);
        }
    }
    private void OnCollisionExit2D (Collision2D collision)
    {

        if (ShovelItem == null) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            ShovelItem.SetActive(false);
        }
    }
}
