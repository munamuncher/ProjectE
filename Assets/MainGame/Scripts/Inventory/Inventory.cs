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

    public void UseItem(int id, int quantity) //add amount in the resourse section , suggestion on if i should use switch function and instead of if?
    {
        ItemData.ItemDataStructure item = itemCache.GetData(id, itemData.itemDataList);

        if (item != null && item.itemtype == ItemType.Consumable)
        {
            PotionScript potion = new PotionScript();
            potion.UseItem(ipotions, item.effectOfPotion);
            RemoveItem(id, quantity);
        }
        else
        {
            Debug.LogWarning("Item is not a potion or could not be found.");
        }
    }

    public void RemoveItem(int id , int quantity)
    {
        ItemData.ItemDataStructure item = itemCache.GetData(id, itemData.itemDataList);

        if(item == null)
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
}
