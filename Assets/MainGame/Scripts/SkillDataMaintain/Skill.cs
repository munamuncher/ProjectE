using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : IManaConsumable, ICooldownable
{
    public int mana { get; set; }
    private float currentCooldownTime;
    private bool isOnCooldown;


    public int manaCost { get; set; }
    public float coolDownTime { get; set; }

    public bool HasEnoughMana() => mana >= manaCost;

    public void UseMana()
    {
        if (HasEnoughMana())
        {
            mana -= manaCost;
            Debug.Log(mana);
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
            Debug.Log(currentCooldownTime);
            if (currentCooldownTime <= 0)
            {
                isOnCooldown = false;
            }
        }
    }

    public abstract void UseSkill();
}


//// This abstract class represents a skill that can be used by a character.
//// It implements interfaces for managing mana consumption and cooldowns.
//public abstract class Skill : IManaConsumable, ICooldownable
//{
//    // A reference to the skill data that contains properties for the skill.
//    protected SkillData.SkillDataStructure skillData;

//    // Property to represent the current mana of the skill user (e.g., player).
//    public int Mana { get; set; }

//    // Variable to track the current cooldown time for the skill.
//    private float currentCooldownTime;

//    // Boolean to check if the skill is currently on cooldown.
//    private bool isOnCooldown;

//    // Constructor that takes a SkillDataStructure object to initialize the skill.
//    protected Skill(SkillData.SkillDataStructure data)
//    {
//        skillData = data; // Store the passed skill data for later use.
//    }

//    // Property to get the mana cost for using this skill from the skill data.
//    public int ManaCost => skillData.mana;

//    // Property to get the cooldown time for this skill from the skill data.
//    public float CooldownTime => skillData.coolDownTime;

//    // Method to check if there is enough mana to use the skill.
//    public bool HasEnoughMana() => Mana >= ManaCost;

//    // Method to deduct mana when the skill is used.
//    public void UseMana()
//    {
//        if (HasEnoughMana()) // Check if there is enough mana.
//        {
//            Mana -= ManaCost; // Deduct the mana cost from the current mana.
//        }
//    }

//    // Method to check if the skill is currently on cooldown.
//    public bool IsOnCooldown() => isOnCooldown;

//    // Method to start the cooldown for this skill.
//    public void StartCooldown()
//    {
//        if (!isOnCooldown) // Only start cooldown if not already on cooldown.
//        {
//            isOnCooldown = true; // Set cooldown status to true.
//            currentCooldownTime = CooldownTime; // Set the current cooldown timer.
//        }
//    }

//    // Method to update the cooldown status every frame.
//    public void UpdateCooldown()
//    {
//        if (isOnCooldown) // Check if the skill is on cooldown.
//        {
//            currentCooldownTime -= Time.deltaTime; // Reduce the cooldown timer based on the time passed since the last frame.
//            if (currentCooldownTime <= 0) // If cooldown time is up,
//            {
//                isOnCooldown = false; // Reset the cooldown status to false.
//            }
//        }
//    }

//    // Abstract method that must be implemented in derived classes to define specific skill behavior.
//    public abstract void UseSkill();
//}