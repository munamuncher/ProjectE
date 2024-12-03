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
        itemCache = new ItemCache();
    }
    #endregion

    public event Action onInventoryChanged;
    [SerializeField]
    private ItemData itemData;
    private ItemCache itemCache;
    public List<InventorySlot> invenSlots = new List<InventorySlot>();
    public int maxSlots = 120;

    public bool AddItem(int id, int quantity)
    {
        if (invenSlots == null)
        {
            Debug.LogWarning("InvenSlots is missing");
        }

        ItemData.ItemDataStructure item = itemCache.GetData(id, itemData.itemDataList);

        if (item == null)
        {
            Debug.LogWarning($"Item with ID {id} not found in the cache.");
            return false;
        }

        Debug.LogWarning($"Found the item {item.itemName} and {quantity} quantity");
        if (item.isStackable)
        {
            foreach (var slot in invenSlots)
            {
                if (slot.item == item) 
                {
                    quantity = slot.AddItem(quantity);

                    if (quantity <= 0)
                    {
                        onInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
        }

        while (quantity > 0 && invenSlots.Count < maxSlots)
        {
            int amountToAdd = Mathf.Min(quantity, item.maxQuantity);
            invenSlots.Add(new InventorySlot(item, amountToAdd));
            quantity -= amountToAdd;
        }
        if (quantity > 0)
        {
            Debug.LogWarning("No space in inventory to add the remaining items.");
            return false;
        }
        onInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(int id , int quantity)
    {
        ItemData.ItemDataStructure item = itemCache.GetData(id, itemData.itemDataList);
        List<InventorySlot> slotsToRemove = new List<InventorySlot>();
        for(int i = invenSlots.Count -1; i >= 0;i--)
        {
            var slots = invenSlots[i];
            if(slots.item == item)
            {
                slots.RemoveItem(quantity); 
                if (slots.quantity <= 0)
                {
                    slotsToRemove.Add(slots);
                }
                onInventoryChanged.Invoke();
            }
        }
        foreach (var slot in slotsToRemove)
        {
            invenSlots.Remove(slot);
        }
    }
}
