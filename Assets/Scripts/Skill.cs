using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ICastable, ICooldownable, IManaConsumable
{
    public SkillData skillData;
    public float mana { get; set; }
    private float manaCost;
    private float cooldownTimer;
    private bool isOnCooldown;

    public void ReciveData(int id)
    {
        manaCost = skillData.skillDataDictionary[id].mana;
        cooldownTimer = skillData.skillDataDictionary[id].coolDown;
        Debug.Log(manaCost);
        Debug.Log(cooldownTimer);
        if (HasEnoughMana())
        {
            UseSkill();
            UseMana();
            StartCooldown();
            Debug.Log("casting");
        }
    }

    public abstract void UseSkill();

    public bool HasEnoughMana()
    {
        if(mana >= manaCost)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    public bool IsOnCooldown() 
    {
        return true;
    }

    public void StartCooldown() // cool down macanism
    {
        if(!isOnCooldown)
        {
            isOnCooldown = true;
            cooldownTimer =- Time.deltaTime;
            
            if(cooldownTimer <= 0)
            {
                isOnCooldown = false;
            }
        }
    }

    public void UseMana()
    {
        mana -= manaCost;
    }
}

