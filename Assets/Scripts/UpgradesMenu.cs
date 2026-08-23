using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Canvas.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            Canvas.SetActive(false);
        }
    }







}