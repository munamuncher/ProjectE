using System.Collections;
using System.Collections.Generic;
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
    public List<ItemDataStructure> itemDataList = new List<ItemDataStructure>();

    public Dictionary<int, ItemDataStructure> ItemDataDictionary;

    private void OnEnable()
    {
        InitializeItemDictionary();
    }

    private void InitializeItemDictionary()
    {
        ItemDataDictionary = new Dictionary<int, ItemDataStructure>();

        foreach (var item in itemDataList)
        {
            if(item == null)
            {
                Debug.LogWarning("Currently there is no item in the List");
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
    }


    [System.Serializable]
    public class ItemDataStructure
    {
        public int ItemID;
        public string itemName;
        public Sprite itemIcon;
        public bool isStackable;
        public ItemType itemtype;
    }

    public ItemDataStructure GetItem(int id)
    {
        if (ItemDataDictionary.TryGetValue(id, out var item))
        {
            return item;
        }
        Debug.LogWarning($"Item not found {id}");
        return null;
    }

}
