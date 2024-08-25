using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ICastable, ICooldownable, IManaConsumable
{
    public float mana { get; set; }

    public float coolDown { get; protected set; }
    public float manaCost { get; protected set; }

    private bool isOnCooldown;
    private float cooldownTimer;

    public void Cast()
    {
       if(HasEnoughMana())
        {
            UseSkill();
            UseMana();
            StartCooldown();
        }
    }

    protected abstract void UseSkill();

    public bool HasEnoughMana()
    {
        if(mana > manaCost)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    public bool IsOnCooldown() // on cool down tell show text on cooldown , and make a cooldown effect
    {
        throw new System.NotImplementedException();
    }

    public void StartCooldown() // cool down macanism
    {
        throw new System.NotImplementedException();
    }

    public void UseMana()
    {
        mana -= manaCost;
    }
}

