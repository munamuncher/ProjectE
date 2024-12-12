using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageCalc : ISkillDamageCalc
{
    public int CalculateSkillDmg(int baseDamage, int characterBonus)
    {
        Debug.LogWarning($"adding{baseDamage} + {characterBonus}");
        return baseDamage + characterBonus;
    }
}
