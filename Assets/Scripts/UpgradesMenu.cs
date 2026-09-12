using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    private TutorialManager tutorialManager;

    private void Start()
    {
        tutorialManager = FindAnyObjectByType<TutorialManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Canvas.SetActive(true);

            if (tutorialManager != null)
            {
                tutorialManager.NearHouse();
            }
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