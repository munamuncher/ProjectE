using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{
    public Thunder(SkillData.SkillDataStructure skillData) : base(skillData) { }

    public override void UseSkill()
    {
        if (HasEnoughMana() && !IsOnCooldown())
        {
            Debug.Log($"{skillData.skillName} cast!");
            UseMana();
            StartCooldown();
            // Logic for creating and using the fireball
        }
        else
        {
            Debug.Log("Not enough mana or skill is on cooldown!");
        }
    }
}
