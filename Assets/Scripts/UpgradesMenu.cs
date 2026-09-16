using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    private TutorialManager tutorialManager;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }   
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
                audioManager.PlaySFX(audioManager.PopUp);
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