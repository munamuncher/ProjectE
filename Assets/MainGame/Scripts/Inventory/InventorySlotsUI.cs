using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotsUI : MonoBehaviour , IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI itemQuantityText;
    public Sprite defultImage;
    private int currentNumb = 0;
    [SerializeField]
    private int ID = 0;
    public void SetSlot(ItemData.ItemDataStructure item , int quantity)
    {
        Debug.LogWarning($"Recived {item.itemName} and quantity of {quantity}");
        if (item != null)
        {
            if (icon.sprite != item.itemIcon || itemQuantityText.text != quantity.ToString())
            {
                ID = item.ItemID;
                icon.sprite = item.itemIcon;
                icon.enabled = true;
                currentNumb = item.isStackable ? quantity : 1;
                itemQuantityText.text = currentNumb.ToString();
            }
        }
        else
        {
            ClearSlot();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogWarning("being clicked");
        Inventory.InvenInst.UseItem(ID, 1);
    }
    public void ClearSlot()
    {
        icon.sprite = defultImage;
        itemQuantityText.text = "";
    }
}
