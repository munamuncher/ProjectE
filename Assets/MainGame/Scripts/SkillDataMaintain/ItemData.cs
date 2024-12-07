using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Material
}

[CreateAssetMenu(fileName = "NewItem",menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public List<ConsumableItem> ConsumableItems = new List<ConsumableItem>();
    public List<EquipmentItem> EquipmentItems = new List<EquipmentItem>();

    public Dictionary<int, ItemDataStructure> ItemDataDictionary;

    private void OnEnable()
    {
        InitializeItemDictionary();
    }

    private void InitializeItemDictionary()
    {
        ItemDataDictionary = new Dictionary<int, ItemDataStructure>();

        foreach (var item in ConsumableItems)
        {
            if(item == null)
            {
                continue;
            }

            if(!ItemDataDictionary.ContainsKey(item.ItemID))
            {
                ItemDataDictionary.Add(item.ItemID, item);
            }
            else
            {
                Debug.LogError($"Duplication Item ID Found: {item.itemName}. Skipping the Item");
            }
        }

        foreach (var item in EquipmentItems)
        {
            if (item == null)
            {
                continue;
            }

            if (!ItemDataDictionary.ContainsKey(item.ItemID))
            {
                ItemDataDictionary.Add(item.ItemID, item);
            }
            else
            {
                Debug.LogError($"Duplication Item ID Found: {item.itemName}. Skipping the Item");
            }
        }
    }

    public ItemDataStructure GetItem(int id, ItemType type)
    {
        switch (type)
        {
            case ItemType.Consumable:
                return ConsumableItems.Find(item => item.ItemID == id);
            case ItemType.Equipment:
                return EquipmentItems.Find(item => item.ItemID == id);
            default:
                Debug.LogWarning("Unknown item type");
                return null;
        }
    }


    [System.Serializable]
    public class ItemDataStructure
    {
        public int ItemID;
        public string itemName;
        public Sprite itemIcon;
        public bool isStackable;
        public int maxQuantity;
        public ItemType itemtype;

    }
    [System.Serializable]
    public class ConsumableItem : ItemData.ItemDataStructure
    {       
        public int effectOfPotion;
    }
    [System.Serializable]
    public class EquipmentItem : ItemData.ItemDataStructure
    {
        [Header("Weapon")]
        public int meleePowerIncrease;
        public int magicPowerIncrease;
        [Header("Amour")]
        public int adIncrease;
        public int mdIncerease;


    }
}
