using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    GameObject target { get; set; }
}

public interface IMoveable
{
    void Move();
}

public interface IDamageable
{
    void Damage(int DamageAmount);
}

public interface ISendData
{ 
    void SendData(int val);
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
    int mana { get; set; }
    int manaCost { get; }
    bool HasEnoughMana();
    void UseMana();
}

public interface IAnimations
{
    void PlayMoveAnim(string animationName);
}



