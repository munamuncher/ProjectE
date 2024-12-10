using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ITarget
{
    GameObject target { get; set; }
    void UpdateEnemyList();
    void FindClosestTarget();

}

public interface IMoveable
{
    void Move(GameObject target);
}

public interface IDamageable
{
    void Damage(int DamageAmount);
}

public interface ICooldownable
{
    float coolDownTime { get; }
    bool IsOnCooldown();
    void StartCooldown();
    void UpdateCooldown();
}

public interface IManaConsumable
{
    int manaCost { get; }
    bool HasEnoughMana(int currentMana);
    void UseMana(int currentMana);
}

public interface IAnimations
{
    void PlayAnim(string animationName , bool loop);
}

public interface ISkillMove
{

    void ReciveDirection(int a);
    void SkillMove();
}

public interface ISKillBuff
{
    void SkillBuff();
}

public interface IChangeSkill
{
    void ReciveSprite(RuntimeAnimatorController animation);
    void ReciveDamageData(int daamage);
}

public interface IRecivePoolObjects
{
    void ReciveGameObject(GameObject Objects);
}

public interface Ipotions
{
    void PotionEffect(int amount);
}

public interface Iequpitable
{
    void ReciveEffectofEquiptment(int ADpower, int APpower, int ARpower, int MDpower);
}



