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

        ipotions = player.GetComponent<Ipotions>();
        if(ipotions == null)
        {
            Debug.LogWarning("Ipotion 참조 실패 - Inventory.cs - Awake()");
        }
    }
    #endregion

    public event Action onInventoryChanged;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private ItemData itemData;
    private ItemCache itemCache;
    private Ipotions ipotions;
    public List<InventorySlot> invenSlots = new List<InventorySlot>();
    public int maxSlots = 120;

    public bool AddItem(int id, int quantity)
    {
        var item = GetItemFromData(id);
        if (item == null) return false;

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

    public void UseItem(int id, int quantity) //add amount in the resourse section , suggestion on if i should use switch function and instead of if?
    {
        var item = GetItemFromData(id);
        if (item == null)
        {
            Debug.LogWarning($"Item with ID {id} not found.");
            return;
        }

        switch (item.itemtype)
        {
            case ItemType.Consumable:
                UseConsumable(item, quantity);
                break;

            case ItemType.Equipment:
                Debug.Log("Equipment items cannot be used directly.");
                break;

            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }

    private void UseConsumable(ItemData.ItemDataStructure item, int quantity)
    {
        if (item is ItemData.ConsumableItem consumableItem)
        {
            PotionScript potion = new PotionScript();
            potion.UseItem(ipotions, consumableItem.effectOfPotion);
            RemoveItem(item.ItemID, quantity);
        }
        else
        {
            Debug.LogWarning("Item is not a valid consumable.");
        }
    }

    public void RemoveItem(int id , int quantity)
    {
        var item = GetItemFromData(id);
        if (item == null)
        {
            Debug.LogWarning($"item with {id} id can not be found");
            return;
        }

        for(int i =invenSlots.Count -1; i >=0 && quantity > 0;i--)
        {
            var slot = invenSlots[i];
            if (slot.item == item)
            {
                if(slot.quantity >= quantity)
                {
                    slot.RemoveItem(quantity);
                    quantity = 0;
                }
                else
                {
                    quantity -= slot.quantity;
                    slot.RemoveItem(quantity);
                }

                if(slot.quantity <= 0)
                {
                    invenSlots.RemoveAt(i);
                }
            }
        }

        if (quantity > 0)
        {
            Debug.LogWarning($"Not enough items to remove. {quantity} remaining.");
        }
        onInventoryChanged?.Invoke();
    }

    private ItemData.ItemDataStructure GetItemFromData(int id)
    {
        if(itemData ==  null || itemData.ItemDataDictionary ==  null)
        {
            Debug.LogWarning("ItemData or ItemDictionary is not initialized correctly");
            return null;
        }

        if(itemData.ItemDataDictionary.TryGetValue(id , out var item))
        {
            return item;
        }

        Debug.LogWarning($"item with Id{id} not found int ItemDataDictionary");
        return null;
    }
}
