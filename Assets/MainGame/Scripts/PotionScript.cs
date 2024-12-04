using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : Consumable
{
    public override void UseItem(Ipotions ipotions , int affectofpotion)
    {
        ipotions.PotionEffect(affectofpotion);
    }
}
