using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{

    public override void UseSkill()
    {
        if (HasEnoughMana() && !IsOnCooldown())
        {
            Debug.Log("Casting thunder!");
            UseMana();
            StartCooldown();

        }
        else
        {
            Debug.Log("Cannot cast thunder: not enough mana or skill is on cooldown.");
        }
    }
}
