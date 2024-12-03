using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData.ItemDataStructure item;
    public int quantity;

    public InventorySlot(ItemData.ItemDataStructure item, int quantity)
    {
        this.item = item;
        this.quantity = Mathf.Min(quantity , item.maxQuantity);
    }

    public int AddItem(int amount)
    {
        int remianingSpace = item.maxQuantity - quantity;
        int addedQuantity = Mathf.Min(remianingSpace, amount);
        quantity += addedQuantity;
        return amount - addedQuantity;
    }

    public void RemoveItem(int amount)
    {  
        quantity -= amount;
        if (quantity < 0)
        {
            ClearSlot();
        }    
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
}
