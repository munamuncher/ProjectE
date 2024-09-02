using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    public GameObject target { get; set; }
}

public interface IMoveable
{
    void Move();
}

public interface IDamageable
{
    void Damage();
}

public interface ICastable
{
    void Cast();
}

public interface ICooldownable
{
    void StartCooldown();
    bool IsOnCooldown();
}

public interface IManaConsumable
{
    void UseMana();
    bool HasEnoughMana();
}

public interface IAnimations
{
    void PlayMoveAnim();
}



