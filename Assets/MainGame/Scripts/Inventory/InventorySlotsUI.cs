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
    private int currentNumb = 0;
    public void SetSlot(ItemData.ItemDataStructure item , int quantity)
    {
        Debug.LogWarning($"Recived {item.itemName} and quantity of {quantity}");
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            currentNumb = item.isStackable ? quantity : 1;
            itemQuantityText.text = currentNumb.ToString();
            if(currentNumb >= 0)
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
        itemQuantityText.text = "";
    }
}
