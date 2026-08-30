using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Data")]
    public int coalCount = 0;
    public int coins = 0;

    public void AddCoal(int amount)
    {
        coalCount += amount;
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


}
