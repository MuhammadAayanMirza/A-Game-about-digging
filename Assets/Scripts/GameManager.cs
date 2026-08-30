using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    [Header("Economy Settings")]
    public int coinValuePerCoal = 10;
    public int[] upgradeCosts = { 0, 10, 100, 300};

    [Header("UI Text References")]
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shovelLevelText;
    public TextMeshProUGUI upgradeCostText;

    [Header("Progress Bar")]

    public Image FirstProgress;
    public Image SecondProgress;

    public Image ThirdProgress;
    public Image FourthProgress;

    private PlayerInventory playerInventory;
    private Digging playerDigging;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy (gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
            playerDigging = player.GetComponent<Digging>();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerInventory == null || playerDigging == null) return;

        if (coalText != null) coalText.text = "Coal: " + playerInventory.coalCount;
        if (coinsText != null) coinsText.text = "Balance: " + playerInventory.coins + "M";

        if (shovelLevelText != null)
            shovelLevelText.text = "Shovel Area" + playerDigging.digSizeLevel + "x" + playerDigging.digSizeLevel;

        if (upgradeCostText != null)
        {
            if (playerDigging.digSizeLevel >= 4)
            {
                upgradeCostText.text = "MAX";
            }
            else
            {
                int nextLevelCost = upgradeCosts[playerDigging.digSizeLevel];
                upgradeCostText.text = nextLevelCost + "M";
            }
        }

        if (playerDigging.digSizeLevel >= 1)
        {
            FirstProgress.gameObject.SetActive(true);
        }
        if (playerDigging.digSizeLevel >= 2)
        {
            SecondProgress.gameObject.SetActive(true);
        }
        if (playerDigging.digSizeLevel >= 3)
        {
            ThirdProgress.gameObject.SetActive(true);
        }
        if (playerDigging.digSizeLevel >= 4)
        {
            FourthProgress.gameObject.SetActive(true);
        }
    }

    public void ShopSellCoal()
    {
        if (playerInventory.coalCount > 0)
        {
            int earnings = playerInventory.coalCount * coinValuePerCoal;
            playerInventory.coins += earnings;
            playerInventory.coalCount = 0;

            UpdateUI();
        }
    }

    public void ShopBuyUpgrade()
    {
        if (playerInventory == null || playerDigging == null) return;

        int currentLevel = playerDigging.digSizeLevel;

        if (currentLevel >= 4) return;

        int costOfNextUpgrade = upgradeCosts[currentLevel];

        if (playerInventory.SpendCoins(costOfNextUpgrade))
        {
            playerDigging.digSizeLevel++;
            UpdateUI();
        }
    }







}
