using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{

    public override void UseSkill()
    {
        StartCooldown();
        Debug.Log("Thunder has been Used");
    }
}
