using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Data")]
    public int coalCount = 0;
    public int coins = 0;

    [Header("Inventory Space")]

    public int baseSpace = 3;
    public int inventorylevel = 1;
    public int IncreasePerLevel = 2;

    [SerializeField] private GameObject inventoryFullPopup;

    public int GetMaxSpace()
    {
        return baseSpace + ((inventorylevel -1) * IncreasePerLevel);
    }


    public TextMeshProUGUI InventoryFull;

    public bool AddCoal(int amount)
    {
       if (coalCount + amount > GetMaxSpace())
       {
            ShowInventoryFullPopup();
            return false;
       }
       
       coalCount += amount;

       if (GameManager.Instance != null)
       {
        GameManager.Instance.UpdateUI();
       }
       return true;

    }


    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        return false;
    }

    public void ShowInventoryFullPopup()
    {
        if (inventoryFullPopup == null)
        return;

        inventoryFullPopup.SetActive(true);

        CancelInvoke(nameof(HideInventoryFullPopup));
        Invoke(nameof(HideInventoryFullPopup), 1.5f);
    }

    private void HideInventoryFullPopup()
    {
        if (inventoryFullPopup != null)
        inventoryFullPopup.SetActive(false);
    }

    public void UpgradeInventory()
    {
        if (inventorylevel >= 4)
        {
            Debug.Log("Inventroy Max");
            return;
        }

        inventorylevel++;

        Debug.Log("Inventory level:" + inventorylevel);
    }

}
