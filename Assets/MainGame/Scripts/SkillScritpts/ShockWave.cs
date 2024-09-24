using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : Skill
{
    public override void UseSkill()
    {
        StartCooldown();
        Debug.Log("Shockwave is happening");
    }
}
