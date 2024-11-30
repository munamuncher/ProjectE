using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region _SingleTon_
    private static Inventory instance;
    public static Inventory InvenInst => instance;
    private void Awake()
    {
        if (instance != this && instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public event Action onInventoryChanged;

    public List<InventorySlot> invenSlots = new List<InventorySlot>();
    public int maxSlots = 120;

    public bool AddItem(ItemData item, int quantity)
    {       
        if(invenSlots == null)
        {
            Debug.LogWarning("InvenSlots is missing");
        }
        else
        {
            Debug.LogWarning($"found the InvenSlots adding the items {item.itemName} and {quantity}");
        }
        foreach (var slot in invenSlots)
        {
            if (slot.item == item && item.isStackable)
            {
                slot.AddItem(quantity);
                onInventoryChanged?.Invoke();
                return true;
            }
        }

        if(invenSlots.Count < maxSlots)
        {
            invenSlots.Add(new InventorySlot(item, quantity));
            onInventoryChanged?.Invoke();
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
                onInventoryChanged?.Invoke();
                break;
            }
        }
    }
}
