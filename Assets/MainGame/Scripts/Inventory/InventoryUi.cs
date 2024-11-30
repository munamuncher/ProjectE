using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    private List<InventorySlotsUI> slotsUI =  new List<InventorySlotsUI>();

    private void Start()
    {
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            var slotUI = Instantiate(slotPrefab,slotParent).GetComponent<InventorySlotsUI>();
            slotsUI.Add(slotUI);
        }
        inventory.onInventoryChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        inventory.onInventoryChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotsUI.Count; i++)
        {
            if (i < inventory.invenSlots.Count)
            {
                var slot = inventory.invenSlots[i];
                slotsUI[i].SetSlot(slot.item, slot.quantity);
            }
            else
            {
                slotsUI[i].ClearSlot();
            }
        }
    }
}
