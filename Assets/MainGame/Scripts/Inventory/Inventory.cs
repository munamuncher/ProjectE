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
        else
        {
            ItemData.ItemDataStructure item = itemCache.GetData(id, itemData.itemDataList);

            if (item != null)
            {
                Debug.LogWarning($"Found the item {item.itemName} and {quantity} quantity");
                if (item.isStackable)
                {
                    foreach (var slot in invenSlots)
                    {
                        if (slot.item == item)
                        {
                            slot.AddItem(quantity);
                            onInventoryChanged?.Invoke();
                            return true;
                        }
                    }
                }
                if (invenSlots.Count < maxSlots)
                {
                    invenSlots.Add(new InventorySlot(item, quantity));
                    onInventoryChanged?.Invoke();
                    return true;
                }
                else
                {
                    Debug.LogWarning("No space in inventory to add the item.");
                    return false;
                }
            }
            else
            {
                Debug.LogWarning($"Item with ID {id} not found in the cache.");
            }
        }

        return false;
    }

    public void RemoveItem(ItemData.ItemDataStructure item, int quantity)
    {
        List<InventorySlot> slotsToRemove = new List<InventorySlot>();
        foreach (var slot in invenSlots)
        {
            if (slot.item == item)
            {
                slot.RemoveItem(quantity);
                if (slot.quantity <= 0)
                {
                    slotsToRemove.Add(slot);
                }
                onInventoryChanged?.Invoke();
                break;
            }
        }
        foreach (var slot in slotsToRemove)
        {
            invenSlots.Remove(slot);
        }
    }
}
