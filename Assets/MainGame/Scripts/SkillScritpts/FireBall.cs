using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
{

    public override void UseSkill()
    {
        if (HasEnoughMana() && !IsOnCooldown())
        {
            Debug.Log("Casting Fireball!");
            UseMana();
            StartCooldown();
            // todo 스킬 능력 효과? 입력
        }
        else
        {
            Debug.Log("Cannot cast Fireball: not enough mana or skill is on cooldown.");
        }
    }
}
