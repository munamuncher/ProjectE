using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotsUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemQuantityText;

    public void SetSlot(ItemData.ItemDataStructure item , int quantity)
    {
        Debug.LogWarning($"Recived {item.itemName} and quantity of {quantity}");
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            itemQuantityText.text = item.isStackable ? quantity.ToString() : "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        itemQuantityText.text = null;
    }
}
