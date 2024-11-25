using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> invenSlots = new List<InventorySlot>();
    public int maxSlots = 20;

    public bool AddItem(ItemData item, int quantity)
    {
        foreach (var slot in invenSlots)
        {
            if (slot.item == item && item.isStackable)
            {
                slot.AddItem(quantity);
                return true;
            }
        }

        if(invenSlots.Count < maxSlots)
        {
            invenSlots.Add(new InventorySlot(item, quantity));
            return true;
        }

        return false;
    }

    public void RemoveItem(ItemData item , int quantity)
    {
        foreach(var slot in invenSlots)
        {
            if(slot.item == item)
            {
                slot.RemoveItem(quantity);
                if(slot.quantity <= 0)
                {
                    invenSlots.Remove(slot);
                }
                break;
            }
        }
    }
}
