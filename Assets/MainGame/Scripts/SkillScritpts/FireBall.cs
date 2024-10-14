using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
{

    public override void UseSkill()
    {
            StartCooldown();
            Debug.Log("FireBall has Been Used");
    }

}
