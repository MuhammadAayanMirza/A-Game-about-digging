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

    public int [] jetpackUpgradeCosts = {0, 10, 100, 300};

    public int [] batteryUpgradeCosts = {0, 30, 100, 300};

    public int [] inventoryUpgradeCosts = {0, 30, 100, 300};

    [Header("UI Text References")]
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI coinsText;
    [Header("Shovel UI References")]
    public TextMeshProUGUI shovelLevelText;
    public TextMeshProUGUI upgradeCostText;

    [Header("Jetpack UI References")]
    public TextMeshProUGUI jetpackLevelText;
    public TextMeshProUGUI jetpackCostText;

    [Header("Inventory UI References")]
    public TextMeshProUGUI inventoryLevelText;
    public TextMeshProUGUI inventoryCostText;

    [Header("Shovel Progress Bar")]

    public Image FirstProgress;
    public Image SecondProgress;

    public Image ThirdProgress;
    public Image FourthProgress;

    [Header("Jetpack Progress Bar")]

    public Image JetFirstProgress;
    public Image JetSecondProgress;

    public Image JetThirdProgress;
    public Image JetFourthProgress;

    [Header("Battery Progress Bar")]

    public Image BatFirstProgress;
    public Image BatSecondProgress;

    public Image BatThirdProgress;
    public Image BatFourthProgress;

    [Header("Inventory Progress Bar")]

    public Image InFirstProgress;
    public Image InSecondProgress;

    public Image InThirdProgress;
    public Image InFourthProgress;

    [Header("Battery UI References")]

    public TextMeshProUGUI batteryLevelText;
    public TextMeshProUGUI batteryCostText;

    

    private PlayerInventory playerInventory;
    private Digging playerDigging;
    private playerMovements playerMove;



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
            playerMove = player.GetComponent<playerMovements>();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerInventory == null || playerDigging == null || playerInventory == null) return;

        if (coalText != null) coalText.text = "Coal: " + playerInventory.coalCount;
        if (coinsText != null) coinsText.text = "Balance: " + playerInventory.coins + "M";

        if (shovelLevelText != null)
        {
            if (playerDigging.digSizeLevel >= 4)
            {
                shovelLevelText.text = "Max";
            }
            else
            {
                float nextDigSize = 1 + playerDigging.digSizeLevel;
                shovelLevelText.text = "  Digs  " + playerDigging.digSizeLevel + "  tiles" + "\n▼" + "\n" + "  Digs  " + nextDigSize + "  tiles";
            }
        }
        if (upgradeCostText != null)
        {
            if (playerDigging.digSizeLevel >= 4)
            {
                upgradeCostText.text = "Max";
            }
            else
            {
                int nextLevelCost = upgradeCosts[playerDigging.digSizeLevel];
                upgradeCostText.text = nextLevelCost + "M";
            }
        }

        if (jetpackLevelText != null)
        {
            float currentJetpackSpeed = playerMove.baseUpMoveSpeed;
            if (playerMove.jetpackLevel >= 4)
            {
                jetpackLevelText.text = "Max";
            }
            else
            {
                float nextJetpackSpeed = currentJetpackSpeed + playerMove.speedBonusPerLevel;
                jetpackLevelText.text = currentJetpackSpeed + "  m/s  " + "\n▼" + "\n" + nextJetpackSpeed + "  m/s ";
            }
        }

        if (jetpackCostText != null)
        {
            if (playerMove.jetpackLevel >= 4)
            {
                jetpackCostText.text = "Max";
            }
            else
            {
                int nextJetpackCost = jetpackUpgradeCosts[playerMove.jetpackLevel];
                jetpackCostText.text = nextJetpackCost + "M";
            }
        }

        if (batteryLevelText != null)
        {
            float currentMaxBattery = playerMove.GetMaxBattery();
            if (playerMove.batteryLevel >= 4)
            {
                batteryLevelText.text = currentMaxBattery + "Max";
            }
            else
            {
                float nextMaxBattery = currentMaxBattery + playerMove.batteryBonusPerLevel;
                batteryLevelText.text = currentMaxBattery + "  EV" + "\n▼" + "\n" + nextMaxBattery + "  EV";
            }
        }

        if (batteryCostText != null)
        {
            if (playerMove.batteryLevel >= 4)
            {
                batteryCostText.text = "Max";
            }
            else
            {
                int nextBatteryCost = batteryUpgradeCosts[playerMove.batteryLevel];
                batteryCostText.text = nextBatteryCost + "M";

            }
        }

        if (inventoryLevelText != null)
        {
            float currentInventorySize = playerInventory.GetMaxSpace();
            if (playerInventory.inventorylevel >= 4)
            {
                inventoryLevelText.text = currentInventorySize + "Max";
            }
            else
            {
                float nextInventorySize = currentInventorySize + playerInventory.IncreasePerLevel;
                inventoryLevelText.text = currentInventorySize + "  Items  " + "\n▼" + "\n" + nextInventorySize + "  Items ";
            }
        }

        if (inventoryCostText != null)
        {
            if (playerInventory.inventorylevel >= 4)
            {
                inventoryCostText.text = "Max";
            }
            else
            {
                int nextInventoryCost = inventoryUpgradeCosts[playerInventory.inventorylevel];
                inventoryCostText.text = nextInventoryCost + "M";
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

         if (playerMove.jetpackLevel >= 1)
        {
            JetFirstProgress.gameObject.SetActive(true);
        }
        if (playerMove.jetpackLevel >= 2)
        {
            JetSecondProgress.gameObject.SetActive(true);
        }
        if (playerMove.jetpackLevel >= 3)
        {
            JetThirdProgress.gameObject.SetActive(true);
        }
        if (playerMove.jetpackLevel >= 4)
        {
            JetFourthProgress.gameObject.SetActive(true);
        }

        if (playerMove.batteryLevel >= 1)
        {
            BatFirstProgress.gameObject.SetActive(true);
        }
        if (playerMove.batteryLevel >= 2)
        {
            BatSecondProgress.gameObject.SetActive(true);
        }
        if (playerMove.batteryLevel >= 3)
        {
            BatThirdProgress.gameObject.SetActive(true);
        }
        if (playerMove.batteryLevel >= 4)
        {
            BatFourthProgress.gameObject.SetActive(true);
        }

         if (playerInventory.inventorylevel >= 1)
        {
            InFirstProgress.gameObject.SetActive(true);
        }
        if (playerInventory.inventorylevel >= 2)
        {
            InSecondProgress.gameObject.SetActive(true);
        }
        if (playerInventory.inventorylevel >= 3)
        {
            InThirdProgress.gameObject.SetActive(true);
        }
        if (playerInventory.inventorylevel >= 4)
        {
            InFourthProgress.gameObject.SetActive(true);
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

    public void ShopBuyJetpackUpgrade()
    {
        if (playerInventory == null || playerMove == null) return;

        int currentLevel = playerMove.jetpackLevel;
        if (currentLevel >= 4) return;

        int costOfNextUpgrade = jetpackUpgradeCosts[currentLevel];

        if (playerInventory.SpendCoins(costOfNextUpgrade))
        {
            playerMove.UpgradeJetpack();
            UpdateUI();
        }

    }

    public void ShopBuyBatteryUpgrade()
    {
        if (playerInventory == null || playerMove == null) return;

        int currentLevel = playerMove.batteryLevel;
        if (currentLevel >= 4) return;

        int costOfNextUpgrade = batteryUpgradeCosts[currentLevel];

        if (playerInventory.SpendCoins(costOfNextUpgrade))
        {
            playerMove.UpgradeBattery();
            UpdateUI();
        }

    }

    public void ShopBuyInventoryUpgrade()
    {
        if (playerInventory == null) return;


        int currentlevel = playerInventory.inventorylevel;
        if (currentlevel >= 4) return;

        int costOfNextUpgrade = inventoryUpgradeCosts[currentlevel];

        if (playerInventory.SpendCoins(costOfNextUpgrade))
        {
            playerInventory.UpgradeInventory();
            UpdateUI();
        }

    }
    






}
