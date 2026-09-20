using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialStage
    {
        Movement,
        Dig,
        Coal,
        Jetpack,
        Shop,
        SellCoal,
        ShovelUpgrade,
        Complete

    }

    private IEnumerator AdvanceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextStage();
    }

    [Header("Tutorial")]
    public TutorialStage currentStage = TutorialStage.Movement;

    [Header("UI")]
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialTitle;
    public TextMeshProUGUI tutorialText;

    [Header("Player")]
    public PlayerInventory playerInventory;

    private bool hasMoved = false;

    private void Start()
    {
        ShowMovementTutorial();
    }

    private void Update()
    {
        switch (currentStage)
        {
            case TutorialStage.Movement:
                CheckMovement();
                break;
            
            case TutorialStage.Dig:
                break;

            case TutorialStage.Coal:
                CheckCoal();
                break;

            case TutorialStage.Jetpack:
                break;
            
            case TutorialStage.Shop:
                break;
            
            case TutorialStage.SellCoal:
                break;

            case TutorialStage.ShovelUpgrade:
                break;

            case TutorialStage.Complete:
                break;
        }

    }

    private void CheckMovement()
    {
        if(Keyboard.current.sKey.isPressed 
        || Keyboard.current.downArrowKey.isPressed
        || Keyboard.current.wKey.isPressed
        || Keyboard.current.upArrowKey.isPressed
        || Keyboard.current.aKey.isPressed
        || Keyboard.current.leftArrowKey.isPressed
        || Keyboard.current.dKey.isPressed
        || Keyboard.current.rightArrowKey.isPressed)

        {
            hasMoved = true;
        }

        if(hasMoved)
        {
            NextStage();
        }
    }

    private void ShowMovementTutorial()
    {
       ShowTutorial(
        "Welcome!",
        "Use Arrow Keys or W/A/S/D to move."

       );
    }

    private void CheckCoal()
    {
        if (playerInventory == null)
        return;

        if (playerInventory.coalCount > 0)
        {
            NextStage();
        }
    }

    private void NextStage()
    {
        currentStage++;

        switch(currentStage)
        {
            case TutorialStage.Dig:
                ShowTutorial(
                    "Let's Dig",
                    "Move to the bottom of the cobble and left click grass tile to dig."
                );
                break;

            case TutorialStage.Coal:
                ShowTutorial(
                    "Nice, Now Find Some Coal",
                    "Collect coal to sell for Coins."
                );
                break;

            case TutorialStage.Jetpack:
                ShowTutorial(
                    "Head Back Up!",
                    "Hold Space to use your jetpack and go to the house."
                );
                break;

            case TutorialStage.Shop:
                ShowTutorial(
                    "Welcome to the Shop",
                    "Come here to sell minerals and buy upgrades."
                );

                StartCoroutine(AdvanceAfterDelay(4f));
                break;

            case TutorialStage.SellCoal:
                ShowTutorial(
                    "Sell Your Coal",
                    "Sell the coal you found for Coins."
                );
                break;

            case TutorialStage.ShovelUpgrade:
                ShowTutorial(
                    "Upgrade Your Shovel",
                    "Use your Coins to buy your first shovel upgrade."
                );
                break;

            case TutorialStage.Complete:
                StartCoroutine(CompleteTutorial());
                break;
        }
    }

    public void OnTileDug()
    {
       if(currentStage == TutorialStage.Dig)
       {
            NextStage();
       }
    }


    public void NearHouse()
    {
        if(currentStage == TutorialStage.Jetpack)
        {
            NextStage();
            
        }
    }

    public void OnCoalSold()
    {
        if(currentStage == TutorialStage.SellCoal)
        {
            NextStage();
        }
    }

    public void OnShovelUpgraded()
    {
        if(currentStage == TutorialStage.ShovelUpgrade)
        {
            NextStage();
        }
    }

    private void ShowTutorial(string title, string message)
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
        
        if (tutorialTitle != null)
            tutorialTitle.text = title;

        if (tutorialText != null)
            tutorialText.text = message;
    }

    private IEnumerator CompleteTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
        
        if (tutorialTitle != null)
            tutorialTitle.text = "Tutorial Complete!";

        if (tutorialText != null)
            tutorialText.text = "You've got the basics down! Dig more to reach the bottom and upgrade along the way!";

        yield return new  WaitForSeconds(4f);

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

    }








}
