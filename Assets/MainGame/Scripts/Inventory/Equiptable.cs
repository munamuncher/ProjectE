using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equiptable
{
    protected void Equip()
    {
        Debug.Log("item has been Equipted");
    }

    public abstract void EquipItem(Iequipable iequipable,int ADpower, int APpower, int ARpower, int MDpower);
}
