using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : IManaConsumable, ICooldownable
{
    private float currentCooldownTime;
    private bool isOnCooldown;


    public int manaCost { get; set; }
    public float coolDownTime { get; set; }

    public bool HasEnoughMana(int currentMana)
    {
        return currentMana >= manaCost;
    }

    public void UseMana(int currentMana)
    {
        if (HasEnoughMana(currentMana))
        {
            currentMana -= manaCost;
        }
    }

    public bool IsOnCooldown() => isOnCooldown;

    public void StartCooldown()
    {
        if (!isOnCooldown)
        {
            isOnCooldown = true;
            currentCooldownTime = coolDownTime;
        }
    }

    public void UpdateCooldown()
    {
        if (isOnCooldown)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 0)
            {
                isOnCooldown = false;
            }
        }
    }

    public abstract void UseSkill();
}
