using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int quantity;

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddItem(int amount)
    {
        quantity += amount;
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
