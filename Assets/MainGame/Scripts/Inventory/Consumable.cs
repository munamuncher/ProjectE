using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable
{
    protected void Consume()
    {
        //make dinking noise
        Debug.Log("item has been consumed");
        //animation aswell
        // use item
    }
    public abstract void UseItem(Ipotions ipotions , int amount);
}
