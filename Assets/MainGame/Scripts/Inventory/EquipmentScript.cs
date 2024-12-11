using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentScript : Equiptable
{
    public override void EquipItem(Iequipable iequipable, int ADpower, int APpower, int ARpower, int MDpower)
    {
        iequipable.ReciveEffectofEquipment( ADpower, APpower, ARpower, MDpower);
    }
}
