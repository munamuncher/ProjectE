using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotsUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemQuantityText;
    public Sprite defultImage;
    public void SetSlot(ItemData.ItemDataStructure item , int quantity)
    {
        Debug.LogWarning($"Recived {item.itemName} and quantity of {quantity}");
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            itemQuantityText.text = item.isStackable ? quantity.ToString() : "";
            if(itemQuantityText.text == "0")
            {
                ClearSlot();
            }
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        icon.sprite = defultImage;
        itemQuantityText.text = null;
    }
}
