using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotsUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemQuantityText;

    public void SetSlot(ItemData item , int quantity)
    {
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
        icon.enabled = false;
        itemQuantityText.text = null;
    }
}
