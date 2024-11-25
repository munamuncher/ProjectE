using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region _SingleTon_
    private static ItemManager instance;
    public static ItemManager ITInst => instance;
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
    }
    #endregion

    private void UseItem(ItemData item)
    {
        switch(item.itemtype)
        {
            case ItemType.Consumable:
                //show what item is used and make log where it show what it did or show increase of power and its functions
                break;
            case ItemType.Equipment:
                break;
            case ItemType.Material:
                break;
        }
    }
}


