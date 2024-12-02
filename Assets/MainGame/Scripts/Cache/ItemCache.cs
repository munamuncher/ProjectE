using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCache : DataCache<ItemData.ItemDataStructure>
{
    protected override int GetId(ItemData.ItemDataStructure data)
    {
        return data.ItemID;
    }
}
